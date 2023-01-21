using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Yu3zx.DapperExtend.Resolve;

namespace Yu3zx.DapperExtend
{
    public class Query<TEntity> where TEntity : IEntity
    {
        protected Query()
            : base()
        {
            IsAdd = GetColumnType.Select;
            this._ModelDes = EntityResolve.GetModelDes<TEntity>();
            this.ExcColums = new List<string>();
        }

        #region 字段
        private static object objLock = new object();

        protected StringBuilder _Sql; //组装的SQL WHERE部分
        protected string ParamPrefix = "@"; //参数前缀
        internal ModelDes _ModelDes;//处理的实体对象描述
        protected int _PageIndex;
        protected int _PageSize;
        protected DBType DbType;
        protected SqlType _sqlType;
        protected IList<string> ExcColums;
        protected Dictionary<string, string> OrderByColums = new Dictionary<string, string>();
        protected PageModel _PageModel { get; set; }

        /// <summary>
        /// 记录参数名和值
        /// </summary>
        protected Dictionary<string, object> ParamValues = new Dictionary<string, object>();

        /// <summary>
        /// 所有字段集合
        /// </summary>
        private List<ParamColumnModel> ParamColumnModels
        {
            get
            {
                var colums = EntityResolve.GetExecColumns(this._ModelDes, IsAdd).ToList();
                if (this.ExcColums != null && this.ExcColums.Count > 0)
                {
                    colums = colums.Where(m => this.ExcColums.Contains(m.ColumnName)).ToList();
                }
                return colums;
            }
        }

        /// <summary>
        /// 查询字段集合
        /// </summary>
        private List<ParamColumnModel> SelectColumns
        {
            get
            {
                var colums = ParamColumnModels;
                if (this.ExcColums != null && this.ExcColums.Count > 0)
                {
                    colums = colums.Where(m => this.ExcColums.Contains(m.ColumnName)).ToList();
                }
                return colums;
            }
        }

        /// <summary>
        /// 实体对象属性集合
        /// </summary>
        private IEnumerable<string> FieldNames
        {
            get
            {
                var cAll = ParamColumnModels.Select(c => c.FieldName).ToList();
                return cAll;
            }
        }

        /// <summary>
        /// 数据库字段集合
        /// </summary>
        private IEnumerable<string> ColumnNames
        {
            get
            {
                var fAll = ParamColumnModels.Select(c => c.ColumnName).ToList();
                return fAll;
            }
        }

        /// <summary>
        /// 是否是添加操作
        /// </summary>
        internal GetColumnType IsAdd { get; set; }

        /// <summary>
        /// 查询记录数，MSSQL的Top数，MySQL的LIMIT数
        /// </summary>
        public int TopNum { get; private set; }

        public DynamicParameters Parameters
        {
            get
            {
                DynamicParameters dp = new DynamicParameters();
                foreach (var paramValue in ParamValues)
                {
                    dp.Add(paramValue.Key, paramValue.Value);
                }
                return dp;
            }
        }

        #endregion

        #region SQL

        /// <summary>
        /// SQL语句
        /// </summary>
        public virtual string SqlString
        {
            get
            {
                var sql = "";
                switch (_sqlType)
                {
                    case SqlType.Select:
                        sql = SelectSql + OrderSql;
                        break;
                    case SqlType.Insert:
                        sql = InsertSql;
                        break;
                    case SqlType.Delete:
                        sql = DeleteSql;
                        break;
                    case SqlType.Where:
                        sql = WhereSql;
                        break;
                    case SqlType.Update:
                        sql = UpdateSql;
                        break;
                    case SqlType.Count:
                        sql = CountSql;
                        break;
                    case SqlType.Page:
                        sql = PageSql;
                        break;
                }
                return sql;
            }
        }

        /// <summary>
        /// SQL字符串,只表示包括Where部分
        /// </summary>
        protected string WhereSql = "WHERE 1=1 ";

        /// <summary>
        /// 查询SQL
        /// </summary>
        protected virtual string SelectSql
        {
            get
            {
                var sqlStr = string.Format("SELECT {0} FROM {1} {2}", SelectColumn, this._ModelDes.TableName, this.WhereSql);
                /*if (_TopNum > 0)
            {
                if (this.DbType == DBType.SqlServer || this.DbType == DBType.SqlServerCE)
                    sqlStr = string.Format("SELECT TOP {0} {1} FROM {2} {3} {4}", this._TopNum, columss, this._ModelDes.TableName, this.WhereSql, this.OrderSql);
                else if (this.DbType == DBType.Oracle)
                {
                    var strWhere = "";
                    if (string.IsNullOrEmpty(this.WhereSql))
                    {
                        strWhere = string.Format(" WHERE  ROWNUM <= {0} ", this._TopNum);
                    }
                    else
                    {
                        strWhere = string.Format(" {0} AND ROWNUM <= {1} ", this.WhereSql, this._TopNum);
                    }
                    sqlStr = string.Format("SELECT * FROM {2} {3} {4}", this._ModelDes.TableName, strWhere, this.OrderSql);
                }
                else
                {
                    sqlStr = string.Format("SELECT {0} FROM {1} {2} {3} LIMIT {4}", columss, this._ModelDes.TableName, this.WhereSql, this.OrderSql, this._TopNum);
                }
            }
            else
            {
                sqlStr = string.Format("SELECT {0} FROM {1} {2} {3}", columss, this._ModelDes.TableName, this.WhereSql, this.OrderSql);
            }*/
                return sqlStr;
            }
        }

        /// <summary>
        /// 排序部分SQL
        /// </summary>
        private string OrderSql
        {
            get
            {
                if (OrderByColums.Count <= 0) return "";
                const string ordersql = " ORDER BY ";
                return ordersql + string.Join(",", OrderByColums.Select(orderByColum =>
                    orderByColum.Key + (!string.IsNullOrWhiteSpace(orderByColum.Value) ? " " + orderByColum.Value : "")).ToArray());
            }
        }

        /// <summary>
        /// 数据总数SQL
        /// </summary>
        private string CountSql
        {
            get
            {
                return string.Format("SELECT COUNT(1) FROM {0} {1}", this._ModelDes.TableName, this.WhereSql);
            }
        }

        /// <summary>
        /// 分页SQL
        /// </summary>
        private string PageSql
        {
            get
            {
                var sql = SelectSql;
                if (_PageModel != null)
                {
                    sql = string.Format(@"WITH temp_list
                                             AS ({0})
                                        SELECT *
                                        FROM   (SELECT t.*,
                                                       Row_number()
                                                         OVER({3}) rn
                                                FROM   temp_list t) t
                                        WHERE  t.rn BETWEEN {1} AND {2}"
                        , SelectSql, _PageModel.RowBegin, _PageModel.RowEnd, OrderSql);
                }
                return sql;
            }
        }

        #endregion

        #region 扩展方法

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns></returns>
        public static Query<TEntity> Builder(DapperContext db, SqlType sqlType = SqlType.Select)
        {
            switch (db.DbType)
            {
                case DBType.SqlServer:
                    return new SqlServerQuery<TEntity> { ParamPrefix = db.ParamPrefix, DbType = db.DbType, _sqlType = sqlType };
                case DBType.Oracle:
                    return new OracleQuery<TEntity> { ParamPrefix = db.ParamPrefix, DbType = db.DbType, _sqlType = sqlType };
                case DBType.MySql:
                     return new  MySqlQuery<TEntity> { ParamPrefix = db.ParamPrefix, DbType = db.DbType, _sqlType = sqlType };
                
            }
            return new Query<TEntity> { ParamPrefix = db.ParamPrefix, DbType = db.DbType, _sqlType = sqlType };
        }

        /// <summary>
        /// 要查询的列
        /// </summary>
        /// <param name="propertySelector">表达式树</param>
        /// <returns></returns>
        public Query<TEntity> Select(Expression<Func<TEntity, object>> propertySelector)
        {
            var propertyList = GetPropertyByExpress(propertySelector);
            var propertyDeses = propertyList as PropertyDes[] ?? propertyList.ToArray();
            if (propertyList == null || !propertyDeses.Any()) return this;
            foreach (var result in propertyDeses.Select(p => p.Column).Where(result => result != null && (!this.ExcColums.Contains(result))))
            {
                this.ExcColums.Add(result);
            }
            return this;
        }

        public Query<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            string result = FormatExpression(expression);
            if (!string.IsNullOrWhiteSpace(result))
            {
                WhereSql += " AND " + result;
            }
            return this;
        }

        public Query<TEntity> Page(int pageIndex, int pageSize)
        {
            _PageModel = new PageModel() { PageIndex = pageIndex, PageSize = pageSize };
            return this;
        }

        /// <summary>
        /// 更新选择字段
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <returns></returns>
        public Query<TEntity> UpdateSelect(Expression<Func<TEntity, object>> propertySelector)
        {
            var propertyList = GetPropertyByExpress(propertySelector);
            var propertyDeses = propertyList as PropertyDes[] ?? propertyList.ToArray();
            if (propertyList == null || !propertyDeses.Any()) return this;
            foreach (var result in propertyDeses.Select(p => p.Column).Where(result => result != null && (!this.ExcColums.Contains(result))))
            {
                this.ExcColums.Add(result);
            }
            return this;
        }

        /// <summary>
        /// TOP
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Query<TEntity> Top(int num)
        {
            TopNum = num;
            return this;
        }

        /// <summary>
        /// 升序
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="expression">要排序的表达式</param>
        /// <returns></returns>
        public Query<TEntity> OrderBy(Expression<Func<TEntity, object>> expression)
        {
            if (expression == null) return this;
            var propertyList = GetPropertyByExpress(expression);
            var propertyDeses = propertyList as PropertyDes[] ?? propertyList.ToArray();
            if (propertyList == null || !propertyDeses.Any()) return this;
            foreach (var result in propertyDeses)
            {
                if (result != null && !OrderByColums.ContainsKey(result.Column))
                {
                    this.OrderByColums.Add(result.Column, "");
                }
            }
            return this;
        }

        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="expression">要排序的表达式</param>
        /// <returns></returns>
        public Query<TEntity> OrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            if (expression == null) return this;
            var propertyList = GetPropertyByExpress(expression);
            var propertyDeses = propertyList as PropertyDes[] ?? propertyList.ToArray();
            if (propertyList == null || !propertyDeses.Any()) return this;
            foreach (var result in propertyDeses)
            {
                if (result != null && !OrderByColums.ContainsKey(result.Column))
                {
                    this.OrderByColums.Add(result.Column, "DESC");
                }
            }
            return this;
        }

        /// <summary>
        /// 插入语句SQL
        /// </summary>
        private string InsertSql
        {
            get
            {
                IsAdd = GetColumnType.Add;
                StringBuilder sql = new StringBuilder();
                sql.Append(string.Format("INSERT INTO {0}(", this._ModelDes.TableName));
                sql.Append(string.Join(",", ColumnNames));
                sql.Append(")");
                sql.Append(" VALUES(");
                sql.Append(ParamPrefix);
                sql.Append(string.Join("," + ParamPrefix, FieldNames));
                sql.Append(") ");
                return sql.ToString();
            }
        }

        /// <summary>
        /// 删除SQL
        /// </summary>
        private string DeleteSql
        {
            get
            {
                return string.Format("DELETE FROM {0} {1}", this._ModelDes.TableName, this.WhereSql);
            }
        }
        /// <summary>
        /// 修改SQL
        /// </summary>
        private string UpdateSql
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(string.Format("UPDATE {0} SET", this._ModelDes.TableName));
                IsAdd = GetColumnType.Update;
                for (int i = 0; i < ParamColumnModels.Count(); i++)
                {
                    var paramColumnModel = ParamColumnModels[i];
                    if (i != 0) sql.Append(",");
                    sql.Append(" ");
                    sql.Append(paramColumnModel.ColumnName);
                    sql.Append(" ");
                    sql.Append("=");
                    sql.Append(" ");
                    sql.Append(ParamPrefix + paramColumnModel.FieldName);
                }
                sql.Append(string.Format(" {0}", WhereSql));
                if (ParamValues.Count > 0) return sql.ToString();
                var p = EntityResolve.GetPrimary(this._ModelDes);
                sql.AppendFormat(" AND {0}={1}", p.Column, ParamPrefix + p.Field);
                return sql.ToString();
            }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 查询列
        /// </summary>
        protected string SelectColumn
        {
            get
            {
                var sqlColumn = new StringBuilder();
                for (int i = 0; i < SelectColumns.Count(); i++)
                {
                    var paramColumnModel = SelectColumns[i];
                    if (i != 0) sqlColumn.Append(",");
                    sqlColumn.Append(paramColumnModel.ColumnName);
                    if (paramColumnModel.ColumnName == paramColumnModel.FieldName) continue;
                    sqlColumn.Append(" AS ");
                    sqlColumn.Append(paramColumnModel.FieldName);
                }
                return sqlColumn.ToString();
            }
        }

        /// <summary>
        /// 将表达式解析成SQL参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string FormatExpression(Expression<Func<TEntity, bool>> expression)
        {
            ExpressionResolve visitor = new ExpressionResolve { Des = _ModelDes, ParamPrefix = ParamPrefix, ParamValues = ParamValues };
            string result;
            if (expression == null)
            {
                result = "";
            }
            else
            {
                string condition;
                if (expression.Body is BinaryExpression)
                {
                    BinaryExpression be = (BinaryExpression)expression.Body;
                    condition = visitor.BinaryExpressionHandler(be.Left, be.Right, be.NodeType);
                }
                else
                {
                    condition = visitor.RouteExpressionHandler(expression.Body, false);
                }
                result = "(" + condition + ")";
            }
            return result;
        }

        /// <summary>
        /// 解析表达式的字段并返回数据库列名
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns></returns>
        private IEnumerable<PropertyDes> GetPropertyByExpress(Expression<Func<TEntity, object>> expression)
        {
            List<PropertyDes> propertyList = new List<PropertyDes>();
            if (expression == null) return propertyList;
            if ((expression != null) && (!(expression.Body is NewExpression) || ((expression.Body as NewExpression).Members.Count == 0)))
            {
                throw new ArgumentNullException("expression 至少包含一个要查询的列");
            }
            foreach (MemberInfo info in (expression.Body as NewExpression).Members)
            {
                propertyList.Add(this._ModelDes.Properties.FirstOrDefault(m => m.Field == info.Name));
            }
            return propertyList;
        }

        #endregion
    }
}
