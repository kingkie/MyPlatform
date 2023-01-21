using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace Yu3zx.MvcTools
{
    public class ModelTableHelper
    {
        /// 把datatable转换为modeList
        /// 调用此函数请保证一下几点：
        /// 1.model的内不包含子model。
        /// 2.model有无参构造函数
        /// 3.model的属性名和datatable的列名一致（不区分大小写）
        /// </summary>
        /// <typeparam name="T">model的类型</typeparam>
        /// <param name="dt">datatable</param>
        /// <returns></returns>
        public static List<T> SetValueFromDB<T>(DataTable dt) where T : new()
        {
            List<T> lstReturn = new List<T>();
            if (dt != null && dt.Rows.Count > 0)
            {

                DataColumnCollection dcc = dt.Columns;
                foreach (DataRow dr in dt.Rows)
                {
                    lstReturn.Add(ConvertDataRowToModel<T>(dr, dcc));
                }
                return lstReturn;
            }
            return lstReturn;
        }

        /// <summary>
        /// 把datarow转换为model
        /// 调用此函数请保证一下几点：
        /// 1.model的内不包含子model。
        /// 2.model有无参构造函数
        /// 3.model的属性名和datatable的列名一致（不区分大小写）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="dcc"></param>
        /// <returns></returns>
        public static T ConvertDataRowToModel<T>(DataRow dr, DataColumnCollection dcc) where T : new()
        {
            if (dr == null || dcc == null)
            {
                return default(T);
            }
            T t = new T();
            PropertyInfo[] pis = t.GetType().GetProperties();
            PropertyInfo pi = null;
            foreach (DataColumn dc in dcc)
            {
                pi = pis.FirstOrDefault(p => p.Name.ToLower().Equals(dc.ColumnName.ToLower()));
                if (pi != null
                    && dr[dc] != null
                    && dr[dc] != DBNull.Value
                    && pi.CanWrite)
                {
                    Type type = pi.PropertyType;
                    if (type.Name.ToLower().Contains("nullable"))
                    {
                        type = Nullable.GetUnderlyingType(type);
                    }
                    if (type.IsEnum)
                    {
                        if (dr[dc] != null && dr[dc] != DBNull.Value)
                        {
                            pi.SetValue(t, Enum.Parse(type, Convert.ToInt32(dr[dc]).ToString()), null);
                        }
                    }
                    else
                    {
                        pi.SetValue(t, Convert.ChangeType(dr[dc], type), null);
                    }
                }
            }
            return t;
        }


        /// <summary>
        /// 转换model
        /// 调用此函数请保证一下几点：
        /// 1.model的内不包含子model。
        /// 2.model有无参构造函数
        /// 3.两个model的属性名一致（不区分大小写）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="dcc"></param>
        /// <returns></returns>
        public static Y CopyTToY<T, Y>(T t) where Y : new()
        {
            Y y = new Y();
            PropertyInfo[] tpis = t.GetType().GetProperties();
            PropertyInfo[] ypis = y.GetType().GetProperties();
            foreach (PropertyInfo tpi in tpis)
            {
                var ypi = ypis.FirstOrDefault(p => p.Name.ToLower().Equals(tpi.Name.ToLower()));
                if (ypi != null
                    && tpi.GetValue(t, null) != null
                    && ypi.CanWrite)
                {
                    Type type = ypi.PropertyType;
                    if (type.Name.ToLower().Contains("nullable"))
                    {
                        type = Nullable.GetUnderlyingType(type);
                    }
                    if (type.IsEnum)
                    {
                        if (ypi != null && tpi.GetValue(t, null) != null)
                        {
                            ypi.SetValue(y, Enum.Parse(type, Convert.ToInt32(tpi.GetValue(t, null)).ToString()), null);
                        }
                    }
                    else
                    {
                        ypi.SetValue(y, Convert.ChangeType(tpi.GetValue(t, null), type), null);
                    }
                }
            }
            return y;
        }

        /// <summary>
        /// 把DataTable里的数据转换成JSON格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTable2Json(DataTable dt)
        {
            System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
            jsonBuilder.Append("{");
            jsonBuilder.AppendFormat("\"total\":{0}, ", dt.Rows.Count);
            jsonBuilder.Append("\"rows\":[ ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
    }
}
