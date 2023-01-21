using System;
using System.Collections;
using System.IO;

namespace Yu3zx.Json
{
    /// <summary>
    /// 用于JSON对象增加ID和Name字段,对象继承id name，可以方便查找对象
    /// [JsonIgnore] 为设置不需要序列化的字段
    /// </summary>
    public class IdNameObject: IComparer
    {
        //编号，key
        public string id;
        //名称
        public string name;
        //操作对象锁
        private static object syncObj = new object();

        public IdNameObject()
        {
            id = "id_" + DateTime.Now.ToString("fff");
            name = "defaultname";
        }

        public IdNameObject(string id, string name)
        {
            this.id = id;

            if(!string.IsNullOrEmpty(name))
               this.name = name;
            else
               name = "defaultname";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is string)
                return id.Equals((string)obj);
            if (obj is IdNameObject)
                return id.Equals(((IdNameObject)obj).id);
            return false;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        #region IComparer 成员

        public int Compare(object x, object y)
        {
            // TODO:  添加 IdNameNode.Compare 实现

            if ((x is IdNameObject) && (y is IdNameObject))
            {
                IdNameObject a = (IdNameObject)x;
                IdNameObject b = (IdNameObject)y;

                return a.id.CompareTo(b.id);
            }
            return 0;
        }

        #endregion


        /// <summary>
        /// 保存对象到文件中
        /// </summary>
        /// <param name="filepathstring"></param>
        /// <returns></returns>
        public bool Save(string filepathstring)
        {
            lock (syncObj)
            {
                try
                {
                    //为了断电等原因导致xml文件保存出错，文件损坏，采用先写副本再替换的方式。
                    using (TextWriter textWriter = File.CreateText(filepathstring))
                    {
                        string jsonsavestr = JSONUtil.SerializeJSON(this);
                        textWriter.Write(jsonsavestr);
                        textWriter.Flush();
                    }
                    if (File.Exists(filepathstring))
                    {
                        FileInfo fi = new FileInfo(filepathstring);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        {
                            fi.Attributes = FileAttributes.Normal;
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
