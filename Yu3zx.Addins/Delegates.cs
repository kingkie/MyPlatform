using System;
using System.Net.Sockets;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Yu3zx.Addins
{
    //事件定义使用如下泛型委托，可以通过EventHelper.SpringEventSafely简化事件的安全触发。
    public delegate void CbGeneric();
    public delegate void CbGeneric<T>(T obj);
    public delegate void CbGeneric<T1, T2>(T1 obj1, T2 obj2);
    public delegate void CbGeneric<T1, T2, T3>(T1 obj1, T2 obj2, T3 obj3);
    public delegate void CbGeneric<T1, T2, T3, T4>(T1 obj1, T2 obj2, T3 obj3, T4 obj4);
    public delegate void CbGeneric<T1, T2, T3, T4 ,T5>(T1 obj1, T2 obj2, T3 obj3, T4 obj4, T5 obj5);
    public delegate void CbGeneric<T1, T2, T3, T4, T5 ,T6>(T1 obj1, T2 obj2, T3 obj3, T4 obj4, T5 obj5 ,T6 obj6);
    public delegate void CbGeneric<T1, T2, T3, T4, T5, T6, T7>(T1 obj1, T2 obj2, T3 obj3, T4 obj4, T5 obj5, T6 obj6, T7 obj7);
    public delegate void CbGeneric<T1, T2, T3, T4, T5, T6, T7 ,T8>(T1 obj1, T2 obj2, T3 obj3, T4 obj4, T5 obj5, T6 obj6, T7 obj7 ,T8 obj8);
    public delegate void CbGeneric<T1, T2, T3, T4, T5, T6, T7, T8 ,T9>(T1 obj1, T2 obj2, T3 obj3, T4 obj4, T5 obj5, T6 obj6, T7 obj7, T8 obj8 ,T9 obj9);
    public delegate void Action<T1,T2>(T1 t1,T2 t2) ;
    public delegate void Action<T1, T2 ,T3>(T1 t1, T2 t2 ,T3 t3);
    public delegate TResult Func<T, TResult>(T source); 
        
    //以下delegate 定义，即将过期
    public delegate void CbSimple();    
    public delegate void CbSimpleInt(int val);
    public delegate void CbSimpleBool(bool val);
    public delegate void CbSimpleStr(string str);  
    public delegate void CbSimpleObj(object obj);
    public delegate void CbStream(byte[] stream);
    public delegate void CbDataRow(DataRow row);
    public delegate void CbDateTime(DateTime dt) ;
    public delegate void CbException(Exception ex);   
    public delegate void CbNetworkStream(NetworkStream stream) ;
    public delegate void CbSimpleStrInt(string str, int val);
    public delegate void CbProgress(int val, int total);

    public interface IExceptionHandler
    {
        void HanleException(Exception ee);
    }

    public class Parameter<T1, T2>
    {
        #region Ctor
        public Parameter() { }
        public Parameter(T1 t1, T2 t2)
        {
            this.arg1 = t1;
            this.arg2 = t2;
        } 
        #endregion

        #region Arg1
        private T1 arg1;
        public T1 Arg1
        {
            get { return arg1; }
            set { arg1 = value; }
        } 
        #endregion

        #region Arg2
        private T2 arg2;
        public T2 Arg2
        {
            get { return arg2; }
            set { arg2 = value; }
        } 
        #endregion
    }

    public class Parameter<T1, T2, T3>
    {
        #region Ctor
        public Parameter() { }
        public Parameter(T1 t1, T2 t2 ,T3 t3)
        {
            this.arg1 = t1;
            this.arg2 = t2;
            this.arg3 = t3;
        }
        #endregion

        #region Arg1
        private T1 arg1;
        public T1 Arg1
        {
            get { return arg1; }
            set { arg1 = value; }
        }
        #endregion

        #region Arg2
        private T2 arg2;
        public T2 Arg2
        {
            get { return arg2; }
            set { arg2 = value; }
        }
        #endregion

        #region Arg3
        private T3 arg3;
        public T3 Arg3
        {
            get { return arg3; }
            set { arg3 = value; }
        }
        #endregion
    }
}
