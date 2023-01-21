using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Yu3zx.DapperExtend;

namespace Yu3zx.DapperExtend
{
    public static class EntityResolve
    {
        private static object objLock = new object();

        /// <summary>
        /// 用于缓存对象转换实体
        /// </summary>
        private static Dictionary<string, ModelDes> _ModelDesCache = new Dictionary<string, ModelDes>();

        /// <summary>
        /// 确定是否已经存在缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static ModelDes ExistModelDesCache(string key)
        {
            ModelDes value;
            _ModelDesCache.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="des"></param>
        private static void Add(string key, ModelDes des)
        {
            lock (objLock)
            {
                if ((!_ModelDesCache.ContainsKey(key)) && des != null)
                {
                    _ModelDesCache.Add(key, des);
                }
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static ModelDes GetModelDesCache(string key)
        {
            ModelDes value;
            _ModelDesCache.TryGetValue(key, out value);
            if (value != null)
                return value;
            throw new Exception("缓存中没存在此数据");
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private static ModelDes UpdateModelDesCache<TEntity>()
        {
            var type = typeof(TEntity);
            var cacheValue = ExistModelDesCache(type.FullName);
            if (cacheValue == null)
            {
                var model = new ModelDes();
                #region 表描述
                model.ClassType = type;
                model.ClassName = type.Name;
                var tbAttrObj = type.GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault();
                if (tbAttrObj != null)
                {
                    var tbAttr = tbAttrObj as TableAttribute;
                    if (!string.IsNullOrEmpty(tbAttr.Name))
                        model.TableName = tbAttr.Name;
                    else
                        model.TableName = model.ClassName;
                }
                else
                    model.TableName = model.ClassName;
                #endregion
                #region 属性描述
                foreach (var propertyInfo in type.GetProperties())
                {
                    var pty = new PropertyDes();
                    pty.Field = propertyInfo.Name;
                    pty.Column = propertyInfo.Name;
                    var attributesList = propertyInfo.GetCustomAttributes(typeof(BaseAttribute), true);

                    foreach (var attributes in attributesList)
                    {
                        if (attributes is IgnoreAttribute)
                        {
                            break;
                        }
                        if (attributes is KeyAttribute)
                        {
                            pty.CusAttribute = attributes as KeyAttribute;
                            break;
                        }
                        if (attributes is ColumnAttribute)
                        {
                            pty.CusAttribute = attributes as ColumnAttribute;
                            if (!string.IsNullOrWhiteSpace(((ColumnAttribute)pty.CusAttribute).Name))
                            {
                                pty.Column = ((ColumnAttribute)pty.CusAttribute).Name;
                            }
                        }
                    }
                    model.Properties.Add(pty);
                }
                #endregion
                Add(type.FullName, model);
                cacheValue = model;
            }
            return cacheValue;
        }

        /// <summary>
        /// 获取转换实体对象描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static ModelDes GetModelDes<TEntity>()
        {
            return UpdateModelDesCache<TEntity>();
        }

        /// <summary>
        /// 获取要执行SQL时的列,添加和修改数据时
        /// </summary>
        /// <param name="des"></param>
        /// <param name="add">是否是添加</param>
        /// <returns></returns>
        internal static IList<ParamColumnModel> GetExecColumns(ModelDes des, GetColumnType getColumnType)
        {
            var columns = new List<ParamColumnModel>();

            if (des == null || des.Properties == null) return columns;

            foreach (var item in des.Properties)
            {
                if (getColumnType != GetColumnType.Select)
                {
                    if (item.CusAttribute is KeyAttribute)
                    {
                        continue;
                    }
                }

                columns.Add(new ParamColumnModel() { ColumnName = item.Column ?? item.Field, FieldName = item.Field });
            }
            return columns;
        }

        /// <summary>
        /// 获取对象的主键标识列和属性
        /// </summary>
        /// <param name="des"></param>
        /// <returns></returns>
        internal static PropertyDes GetPrimary(ModelDes des)
        {
            var p = des.Properties.FirstOrDefault(m => m.CusAttribute is KeyAttribute);
            if (p == null)
            {
                throw new Exception("没有任何列标记为主键特性");
            }
            return p as PropertyDes;
        }
    }
}
