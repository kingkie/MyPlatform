using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Yu3zx.DataBases
{
    public interface IDataAccess
    {
        bool Exists(string strSql);
        bool ConnLinkTest();
        DbParameter CreateParam(string paramName, SqlDbType dbType, object objValue, int size = 0, ParameterDirection direction = ParameterDirection.Input);
        DataRow ExecuteDataRowProcedure(string ProName, params DbParameter[] ParaName);
        DataRowView ExecuteDataRowViewProcedure(string ProName, params DbParameter[] ParaName);
        DataSet getQueryDataSet(string strSQL);
        DataSet getQueryDataSet(string[] strSQLs, string[] tableNames);
        DataSet getQueryDataSet(string strSQL, string tableName);
        DataSet getProcedureDataSet(string storeProceName, params DbParameter[] ParaName);
        DataSet getProcedureDataSet(string storeProceName, ref int returnValue, params DbParameter[] ParaName);
        DataTable getQueryDataTable(string strSQL);
        DataTable getProcedureDataTable(string storeProceName, params DbParameter[] ParaName);
        DataTable getProcedureDataTable(string storeProceName, ref int returnValue, DbParameter[] ParaName);
        int ExecuteNonQuery(string[] strSQLs);
        int ExecuteNonQuery(string strSQL);
        int ExecuteNonQuery(string[] strSQLs, object[][] Paras);
        DbDataReader ExecuteProcedureReader(string strSQL, params DbParameter[] ParaName);
        DbDataReader ExecuteReader(string strSQL);
        object ExecuteScalar(string strSQL);
        bool ExecuteStoredProcedure(string ProName);
        int ExecuteStoredProcedure(string ProName, params DbParameter[] ParaName);
        void FillDataSet(ref DataSet ds, string SQL, string TableName);
    }
}
