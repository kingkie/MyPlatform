using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.DapperExtend
{
    /// <summary>
    /// SQLite组装查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SQLiteQuery<TEntity> : Query<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// 查询语句
        /// </summary>
        protected override string SelectSql
        {
            get
            {
                var sqlStr = base.SelectSql;
                if (TopNum > 0)
                {
                    //sqlStr = string.Format("SELECT {0} {1} FROM {2} {3} ", TopNum, base.SelectColumn, this._ModelDes.TableName, this.WhereSql);
                    sqlStr = string.Format("SELECT {0} FROM {1} {2} {3} LIMIT {4}", base.SelectColumn, this._ModelDes.TableName, this.WhereSql, this.OrderSql, this.TopNum);
                }
                return sqlStr;
            }
        }

        public override string SqlString
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
                    sql = string.Format(@"SELECT {0} FROM {1} WHERE Id >=(SELECT Id FROM {2} ORDER BY Id asc LIMIT {3},1 ) LIMIT {4}", base.SelectColumn, this._ModelDes.TableName, this._ModelDes.TableName, _PageModel.RowBegin - 1, _PageModel.PageSize);
                }
                return sql;
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
    }
}
