using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Linq.Expressions;

namespace Yu3zx.DapperExtend
{
    /// <summary>
    /// DapperContext的扩展方法；扩展的类为静态类；扩展的方法是静态方法
    /// .NET中，此方法的参数中必须要有被扩展类作为第一个参数，此参数前面用this关键字修饰。此方法在客户端作为一个指定类型的实例调用。
    /// 扩展方法必须在同一命名空间使用，你需要使用using声明导入该类的命名空间。
    /// 针对包含扩展方法的扩展类，你可以定义任何名称。类必须是静态的。
    /// 如果你想针对一个类型添加新的方法，你不需要有该类型的源码，就可以使用和执行该类型的扩展方法。
    /// 如果扩展方法与该类型中定义的方法具有相同的签名，则扩展方法永远不会被调用。
    /// </summary>
    public static class DapperContextExtension
    {
        #region 插入数据

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="sql">sql语句</param>
        /// <param name="entity">实体参数对象</param>
        /// <param name="transaction">事务</param>
        /// <returns>是否成功</returns>
        public static bool Insert<TEntity>(this DapperContext db, string sql, object entity, IDbTransaction transaction = null) where TEntity : IEntity
        {
            return db.DbConnecttion.Execute(sql, entity, transaction) > 0;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">实体对象</param>
        /// <param name="transaction">事务</param>
        /// <returns>是否成功</returns>
        public static bool Insert<TEntity>(this DapperContext db, TEntity entity, IDbTransaction transaction = null) where TEntity : IEntity
        {
            var sql = Query<TEntity>.Builder(db, SqlType.Insert).SqlString;
            return db.DbConnecttion.Execute(sql, entity, transaction) > 0;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">实体对象</param>
        /// <param name="transaction">事务</param>
        /// <returns>是否成功</returns>
        public static int InsertRow<TEntity>(this DapperContext db, TEntity entity, IDbTransaction transaction = null) where TEntity : IEntity
        {
            var sql = Query<TEntity>.Builder(db, SqlType.Insert).SqlString;
            return db.DbConnecttion.Execute(sql, entity, transaction);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="entities">实体对象集合</param>
        /// <param name="transaction">事务</param>
        /// <returns>是否成功</returns>
        public static bool Insert<TEntity>(this DapperContext db, List<TEntity> entities, IDbTransaction transaction = null) where TEntity : IEntity
        {
            var sql = Query<TEntity>.Builder(db, SqlType.Insert).SqlString;
            return db.DbConnecttion.Execute(sql, entities, transaction) > 0;
        }
        #endregion

        #region 删除数据

        /// <summary>
        /// 根据SQL删除
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="sql">sql语句</param>
        /// <param name="entity">实体参数对象</param>
        /// <param name="transaction">事务</param>
        /// <returns>是否成功</returns>
        public static bool Delete<TEntity>(this DapperContext db, string sql, object entity, IDbTransaction transaction = null) where TEntity : IEntity
        {
            return db.DbConnecttion.Execute(sql, entity, transaction) > 0;
        }

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="expression">条件表达式</param>
        /// <param name="transaction">事务</param>
        /// <returns>是否成功</returns>
        public static bool Delete<TEntity>(this DapperContext db, Expression<Func<TEntity, bool>> expression, IDbTransaction transaction = null) where TEntity : IEntity
        {
            if (expression == null)
            {
                throw new ArgumentNullException("条件不能为空");
            }
            var query = Query<TEntity>.Builder(db, SqlType.Delete).Where(expression);
            return db.DbConnecttion.Execute(query.SqlString, query.Parameters, transaction) > 0;
        }
        #endregion

        #region 更新数据
        /// <summary>
        /// 根据SQL修改数据
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="transaction">事务</param>
        /// <param name="sql">sql语句</param>
        /// <param name="entity">实体参数对象</param>
        /// <returns>是否成功</returns>
        public static bool Update(this DapperContext db, string sql, object entity, IDbTransaction transaction = null)
        {
            return db.DbConnecttion.Execute(sql, entity, transaction) > 0;
        }

        /// <summary>
        ///默认根据主键修改数据
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">实体对象</param>
        /// <param name="transaction">事务</param>
        /// <param name="selectorExpression">更新列表达式</param>
        /// <returns>是否成功</returns>
        public static bool Update<TEntity>(this DapperContext db, Expression<Func<TEntity, object>> selectorExpression,
            TEntity entity, IDbTransaction transaction = null) where TEntity : IEntity
        {
            var query = Query<TEntity>.Builder(db, SqlType.Update).UpdateSelect(selectorExpression);
            return db.DbConnecttion.Execute(query.SqlString, entity, transaction) > 0;
        }
        #endregion

        #region 查询数据

        /// <summary>
        /// 根据SQL语句获取数据
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">实体参数对象</param>
        /// <returns></returns>
        public static List<TEntity> Select<TEntity>(this DapperContext db, string sql, object parameters) where TEntity : IEntity
        {
            var result = db.DbConnecttion.Query<TEntity>(sql, parameters);
            return result.ToList();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="query">查询对象</param>
        /// <param name="whereExpression">参数对象</param>
        /// <param name="selectorExpression">查询字段</param>
        /// <returns></returns>
        public static List<TEntity> Select<TEntity>(this DapperContext db, Expression<Func<TEntity, bool>> whereExpression = null,
            Expression<Func<TEntity, object>> selectorExpression = null,
            Query<TEntity> query = null) where TEntity : IEntity
        {
            if (query == null)
            {
                query = Query<TEntity>.Builder(db).Where(whereExpression).Select(selectorExpression);
            }
            var result = db.DbConnecttion.Query<TEntity>(query.SqlString, query.Parameters);
            return result.ToList();
        }

        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">实体参数对象</param>
        /// <returns></returns>
        public static int Count<TEntity>(this DapperContext db, string sql, object parameters) where TEntity : IEntity
        {
            var result = db.DbConnecttion.Query<int>(sql, parameters).SingleOrDefault();

            return result;
        }

        /// <summary>
        /// 根据SQL获取记录数
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="query">查询对象</param>
        /// <param name="whereExpression">参数对象</param>
        /// <returns></returns>
        public static int Count<TEntity>(this DapperContext db, Expression<Func<TEntity, bool>> whereExpression = null
            , Query<TEntity> query = null) where TEntity : IEntity
        {
            if (query == null)
            {
                query = Query<TEntity>.Builder(db, SqlType.Count).Where(whereExpression);
            }
            var result = db.DbConnecttion.Query<int>(query.SqlString, query.Parameters).SingleOrDefault();
            return result;
        }

        /// <summary>
        /// 分页SQL
        /// </summary>
        /// <typeparam name="TEntity">实体对象类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="whereExpression">查询条件</param>
        /// <param name="selectorExpression">选择字段</param>
        /// <param name="orderExpression">升序表达式</param>
        /// <param name="orderdescExpression">降序表达式</param>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public static List<TEntity> Page<TEntity>(this DapperContext db, int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereExpression = null,
            Expression<Func<TEntity, object>> selectorExpression = null,
            Expression<Func<TEntity, object>> orderExpression = null,
            Expression<Func<TEntity, object>> orderdescExpression = null,
            Query<TEntity> query = null) where TEntity : IEntity
        {

            if (query == null)
            {
                query = Query<TEntity>.Builder(db, SqlType.Page).Where(whereExpression)
                    .Select(selectorExpression)
                    .Page(pageIndex, pageSize)
                    .OrderBy(orderExpression)
                    .OrderByDesc(orderdescExpression);
            }

            var result = db.DbConnecttion.Query<TEntity>(query.SqlString, query.Parameters);

            return result.ToList();
        }

        #endregion
    }
}
