using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.DapperExtend
{
    public class DapperUseHelp
    {
        /// <summary>
        /// 插入单个对象
        /// var model = new UserEntity(){UserName = "杨艳",Pwd="Yu3zx201906",Age=29}
        /// using (var db = new DapperContext("MySqlDbConnection"))
        /// {
        ///     bool result = db.Insert(model);
        /// }
        /// </summary>
        public void ModelInsert()
        {

        }

        /// <summary>
        /// 插入多个对象
        /// var list = new List<UserEntity>();
        /// using (var db = new DapperContext("MySqlDbConnection"))
        /// {
        ///     bool result = db.Insert(list);
        /// }
        /// </summary>
        public void MulitInsert()
        {

        }

        /// <summary>
        /// 使用事务插入单个对象
        /// var model = new UserEntity(){UserName = "杨艳",Pwd="Yu3zx201906",Age=29}
        /// using (var db = new DapperContext("MySqlDbConnection"))
        /// {
        ///     var tran = db.DbTransaction;
        ///     bool result = db.Insert(model);
        ///     if (result)
        ///         tran.Commit();
        ///     else
        ///         tran.Rollback();
        /// }
        /// </summary>
        public void TranInsert()
        {

        }

        /// <summary>
        /// 删除单个对象
        /// using (var db = new DapperContext("MySqlDbConnection"))
        /// {
        ///      bool result = db.Delete<UserEntity>(u => u.Id == 21);
        /// }
        /// </summary>
        public void DelModel()
        {

        }

        /// <summary>
        /// 更新模型方法一
        /// var entity = new UserEntity{Id=23,Age=28,Pwd="520ertr4632r"};
        /// using (var db = new DapperContext("MySqlDbConnection"))
        /// {
        ///     bool result = db.Update(u => new { u.Age, u.Pwd }, entity);
        ///     
        /// }
        /// 更新模型方法二
        /// using (var db = new DapperContext("MySqlDbConnection"))
        /// {
        ///     var result = db.Update("update tuser set age=@age where age>@agenew", new { Id = 27, Age = 48, agenew = 44 });
        /// }
        /// agenew为新增加的参数
        /// </summary>
        public void UpdateModel()
        {
            //更新模型方法三
            //using (var db = new DapperContext("MySqlDbConnection"))
            //{
            //    var result = db.Update("update tuser set age=@age where Id=@Id", new { Id = 10033, Age = 50 });
            //}
        }

        /// <summary>
        /// 查询方法一
        /// using (var db = new DapperContext("MySqlDbConnection"))
        /// {
        ///     var result = db.Select<UserEntity>(u => u.Valid == 1);
        /// }
        /// </summary>
        public void QueryModel()
        {
            #region  查询方式二
            //using (var db = new DapperContext("MySqlDbConnection"))
            //{
            //    var query =
            //        Query<UserEntity>.Builder(db)
            //            .Select(u => new { u.Age, u.UserName })
            //            .Where(u => u.Age > 10 && u.Age < 30)
            //            .Top(3)
            //            .OrderBy(u => new { u.Age, u.Id })
            //            .OrderByDesc(u => new { u.AddTime, u.Pwd });

            //    var result = db.Select(query: query);
            //}
            #endregion End

            #region 查询方式三
            //using (var db = new DapperContext("MySqlDbConnection"))
            //{
            //var result = db.Select<UserEntity>("select * from tuser where valid=@valid", new { valid = 1 });
            //}
            #endregion End

            #region 查询方式四
            //using (var db = new DapperContext("MySqlDbConnection"))
            //{
            //var result = db.Select<UserEntity>("select * from tuser where valid=@valid", new { valid = 1 });
            //}
            #endregion End
        }

        /// <summary>
        /// 查询记录计数方法一
        /// using (var db = new DapperContext("MySqlDbConnection"))
        /// {
        ///     var result = db.Count<UserEntity>(u => u.Age > 30);
        /// }
        /// 查询记录计数方法二
        /// using (var db = new DapperContext("MySqlDbConnection"))
        /// {
        ///     var result = db.Count<UserEntity>("select count(1) from tuser where valid=@valid", new { valid = 1 });
        /// }
        /// </summary>
        public void QueryCount()
        {

        }

        /// <summary>
        /// 分页查询
        /// using (var db = new DapperContext("MySqlDbConnection"))
        /// {
        ///     //  u => u.Valid == 1是为Where语句，orderExpression: u => new { u.Id }为表达式
        ///     var result = db.Page<UserEntity>(1, 3, u => u.Valid == 1, orderExpression: u => new { u.Id });
        /// }
        /// </summary>
        public void PageQuery()
        {

        }

        #region 其它方法
        //更新多条数据
        //     var sql = @"UPDATE dbo.[user] SET UserName=@UserName WHERE UserId = @UserId ;";
        //     var users = new List<User> {
        //     new User { UserId=3013, UserName = "张三", },
        //     new User { UserId =3012, UserName = "李四",  },
        //};
        //     var res = Repository.Execute(sql, users);

        ///// <summary>
        /// 把多个用户的isvalid置为0
        /// </summary>
        //public void UpdateUsersByWhereInTest()
        //{
        //    var sql = @"UPDATE dbo.[user] SET isvalid=0 WHERE UserId IN @UserId ;";
        //    var userIdArr = new int[] { 3013, 3012, 3011, 3010 };
        //    var res = Repository.Execute(sql, new { UserId = userIdArr });
        //    Assert.True(res > 0);
        //}

        //var info = connection.Query<Users>("sp_GetUsers", new { id = 5 },
        //                           commandType: CommandType.StoredProcedure);


        #endregion End
    }



}
