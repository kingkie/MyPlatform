using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Data.SQLite;

namespace Yu3zx.DapperExtend
{
    /// <summary>
    /// Dapper下的数据上下文;默认使用MSSQL数据库
    /// </summary>
    public class DapperContext : IDisposable
    {
        public DapperContext(string connectionName = "DbConnection")
        {
            var providerName = "System.Data.SqlClient";
            var connection = ConfigurationManager.ConnectionStrings[connectionName];
            if (!string.IsNullOrEmpty(connection.ProviderName))
                providerName = connection.ProviderName;
            ProviderName = providerName;
            ConnString = connection.ConnectionString;
            this.Open();
        }

        private string paramPrefix = "@";
        private string providerName = "System.Data.SqlClient";
        private IDbConnection dbConn;
        private DbProviderFactory dbFactory;
        private DBType _dbType = DBType.SqlServer;

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public IDbConnection DbConnecttion
        {
            get
            {
                return dbConn;
            }
        }
        /// <summary>
        /// 执行事务对象
        /// </summary>
        public IDbTransaction DbTransaction
        {
            get
            {
                return dbConn.BeginTransaction();
            }
        }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DBType DbType
        {
            get
            {
                return _dbType;
            }
        }

        /// <summary>
        /// 参数前缀
        /// </summary>
        public string ParamPrefix
        {
            get
            {
                return paramPrefix;
            }
        }

        /// <summary>
        /// 提供者名称
        /// </summary>
        public string ProviderName { get; set; }
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public string ConnString { get; set; }
        /// <summary>
        /// 打开数据库
        /// </summary>
        public void Open()
        {
            SetParamPrefix();
            try
            {
                switch (_dbType)
                {
                    case DBType.SqlServer:
                    case DBType.Oracle:
                        dbFactory = DbProviderFactories.GetFactory(ProviderName);
                        dbConn = dbFactory.CreateConnection();
                        if (dbConn != null)
                        {
                            dbConn.ConnectionString = ConnString;
                            dbConn.Open();
                        }
                        break;
                    case DBType.MySql:
                        dbConn = new MySqlConnection();//  MySql.Data.MySqlClient()
                        dbConn.ConnectionString = ConnString;
                        dbConn.Open();
                        break;
                    case DBType.SQLite:
                        dbConn = new SQLiteConnection(ConnString);
                        //dbConn.ConnectionString = ConnString;
                        dbConn.Open();
                        break;
                    case DBType.SqlServerCE:
                    case DBType.PostgreSQL:
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SetParamPrefix()
        {
            string dbtype;
            if(dbFactory == null)
            {
                if(dbConn == null)
                {
                    dbtype = ProviderName;
                }
                else
                {
                    dbtype = dbConn.GetType().FullName;
                }
            }
            else
            {
                dbtype = dbFactory.GetType().FullName;
            }
            // 使用类型名判断
            if (dbtype.StartsWith("MySql")) _dbType = DBType.MySql;
            else if (dbtype.StartsWith("SqlCe")) _dbType = DBType.SqlServerCE;
            else if (dbtype.StartsWith("Npgsql")) _dbType = DBType.PostgreSQL;
            else if (dbtype.StartsWith("Oracle")) _dbType = DBType.Oracle;
            else if (dbtype.StartsWith("SQLite")) _dbType = DBType.SQLite;
            else if (dbtype.StartsWith("System.Data.SqlClient.")) _dbType = DBType.SqlServer;
            //
            else if (dbtype.IndexOf("MySql", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.MySql;
            else if (dbtype.IndexOf("SqlServerCe", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.SqlServerCE;
            else if (dbtype.IndexOf("Npgsql", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.PostgreSQL;
            else if (dbtype.IndexOf("Oracle", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.Oracle;
            else if (dbtype.IndexOf("SQLite", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.SQLite;
            // else try with provider name
            else if (providerName.IndexOf("MySql", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.MySql;
            else if (providerName.IndexOf("SqlServerCe", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.SqlServerCE;
            else if (providerName.IndexOf("Npgsql", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.PostgreSQL;
            else if (providerName.IndexOf("Oracle", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.Oracle;
            else if (providerName.IndexOf("SQLite", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.SQLite;

            if (_dbType == DBType.MySql && dbConn != null && dbConn.ConnectionString != null && dbConn.ConnectionString.IndexOf("Allow User Variables=true") >= 0)
                paramPrefix = "?";
            if (_dbType == DBType.Oracle)
                paramPrefix = ":";
        }
        public void Dispose()
        {
            if (dbConn != null)
            {
                try
                {
                    dbConn.Dispose();
                }
                catch 
                {

                }
            }
        }
    }
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DBType
    {
        SqlServer,
        MySql,
        SQLite,
        Oracle,
        SqlServerCE,
        PostgreSQL
    }
}
