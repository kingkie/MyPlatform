using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace Yu3zx.Util
{
    public static class UtilityExtention
    {
        /// <summary>
        /// 将IEnumerable转换为ObservableCollection.
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sender">源集合</param>
        /// <returns>目标集合</returns>
        public static ObservableCollection<T> ConvertTo<T>(this IEnumerable<T> sender, bool isConvertAll = false)
        {
            if (sender == null)
            {
                return null;
            }
            ObservableCollection<T> collection = new ObservableCollection<T>();
            IEnumerator<T> ie = sender.GetEnumerator();
            if (isConvertAll)
            {
                while (ie.MoveNext())
                {
                    collection.Add(ie.Current);
                }
            }
            else
            {
                while (ie.MoveNext())
                {
                    collection.Add(ie.Current);
                }
            }

            return collection;
        }

        /// <summary>
        /// 获取依赖属性的静态只读字段信息.
        /// </summary>
        /// <param name="type">所属类</param>
        /// <param name="name">依赖属性名称</param>
        /// <returns>依赖属性的字段信息, 不存在则返回<c>null</c>.</returns>
        public static FieldInfo GetDependencyPropertyField(this Type type, string name)
        {
            FieldInfo fi = type.GetField(name: name, bindingAttr: BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
            if (fi != null && fi.FieldType == typeof(DependencyProperty))
            {
                return fi;
            }

            if (type.BaseType != null)
            {
                return type.BaseType.GetDependencyPropertyField(name);
            }

            return null;
        }


    }
}
