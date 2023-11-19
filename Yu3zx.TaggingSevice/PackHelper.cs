using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.TaggingSevice
{
    public class PackHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lBQuality"></param>
        /// <returns></returns>
        public static byte[] BuildBTypeValue(List<int> lBQuality)
        {
            int iMin = Math.Min(16, lBQuality.Count);
            byte[] fByte = new byte[2] { 0x00, 0x00 };
            for (int i = 0; i < iMin; i++)
            {
                if (lBQuality[i] > 16)
                {
                    continue;
                }
                else if (lBQuality[i] > -1 && lBQuality[i] < 8)
                {
                    fByte[1] = SetBitValue(fByte[1], lBQuality[i]);
                }
                else if (lBQuality[i] > 7 && lBQuality[i] < 16)
                {
                    fByte[0] = SetBitValue(fByte[0], lBQuality[i] % 8);
                }
            }
            return fByte;
        }

        private static byte SetBitValue(byte data, int index)
        {
            if (index > 7 || index < 0)
            {
                return data;
            }
            int vBit = 1 << index;
            vBit = vBit & 0xFF;
            data = (byte)(data | vBit);
            return data;
        }

        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
    }
}
