using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.Protobuf
{
    [ProtoContract(Name = @"FlyData")]
    public class FlyData
    {
        [ProtoMember(1,IsRequired =true,Name = @"lng",DataFormat = DataFormat.Default)]
        [global::System.ComponentModel.DefaultValue(0)]
        public double lng
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public double lat { get; set; }// = 2;//纬度
        [ProtoMember(3)]
        public float altitude { get; set; }// = 3;//海拔高度(椭球高)(单位:m)
        [ProtoMember(4)]
        public float ultrasonic { get; set; }// = 4;//相对高度
        [ProtoMember(5)]
        public float pitch { get; set; }// = 5;//俯仰角
        [ProtoMember(6)]
        public float roll { get; set; }// = 6;//横滚角
        [ProtoMember(7)]
        public float yaw { get; set; }// = 7;//偏航角
    }
    /// <summary>
    /// 测试方法
    /// </summary>
    public class ClassTest
    {
        public static FlyData GetFlyData(byte[] bFly)
        {
            FlyData fly = ProtobufHelper.DeSerialize<FlyData>(bFly);
            if(fly != null)
            {
                return fly;
            }
            else
            {
                return new FlyData() { altitude = 0, lat = 123.433, lng = 32.5 };
            }
        }

        public static string GetFlyStrData(FlyData flyData)
        {
            string flyStr = ProtobufHelper.Serialize(flyData);
            if(string.IsNullOrEmpty(flyStr))
            {
                flyStr = "";
            }
            return flyStr;
        }

        [STAThread]
        static void Main()
        {
            FlyData flyData = new FlyData();
            flyData.altitude = 1.0f;
            flyData.lat = 123.58f;
            flyData.lng = 36.21f;
            flyData.pitch = 1.0f;
            flyData.roll = 2.01f;
            flyData.ultrasonic = 3.0f;
            flyData.yaw = 1;

            byte[] sss = ProtobufHelper.Serialize2Bytes<FlyData>(flyData);
            Console.WriteLine("Data:" + string.Join(" ", sss.Select(x=>x.ToString("X2"))));

            byte[] bData = sss;
            var vLfy = GetFlyData(bData);
            if(vLfy != null)
            {
                Console.WriteLine("Obj:" + vLfy.lat);
                Console.WriteLine("Obj:" + vLfy.lng);
                Console.WriteLine("Obj:" + vLfy.ultrasonic);
            }
        }
    }
}
