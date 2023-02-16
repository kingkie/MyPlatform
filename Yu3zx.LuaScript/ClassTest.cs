using LuaInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.LuaScript
{
    internal class ClassTest
    {
         void testc()
         {
            Lua lua = new Lua(); //创建一个lua解释器

            lua["num"] = 66;               //用C#脚本在lua环境中，创建一个变量
            lua["str"] = "chinar";         //用C#脚本在lua环境中，创建一个字符串变量
            Console.WriteLine(lua["num"]); //输出
            Console.WriteLine(lua["str"]); //输出


            lua.DoString("num=666");                          //用C#脚本在lua环境中，创建一段lua脚本：num=666
            lua.DoString("str='chianr666'");                  //用C#脚本在lua环境中，创建一段lua脚本：str='chianr666'
            object[] values = lua.DoString("return num,str"); //用一个object数组 去接受返回值
            foreach (var value in values)                     //遍历 values 数组中的元素
            {
                Console.WriteLine(value); //输出
            }

            lua.DoFile("Chinar.lua"); //加载lua文件 —— lua.DoFile（文件名）

            ClassTest program = new ClassTest(); //声明一个对象
            //lua.RegisterFunction 注册方法（注册到Lua中以后方法的名称，程序对象，程序的类型program.GetType().（传入C#中的方法名：需要是公有方法））
            lua.RegisterFunction("LuaChinarTest", program, program.GetType().GetMethod("ChinarTest"));
            //然后用lua.DoString(Lua中方法名())调用测试
            lua.DoString("LuaChinarTest()");

            //lua.RegisterFunction 注册静态方法（注册到Lua中以后方法的名称，空，程序的类型 typeof(Program).（传入C#中的方法名：需要是公有方法））
            lua.RegisterFunction("LuaChinarStaticTest", null, typeof(ClassTest).GetMethod("ChinarStaticTest"));
            //然后用lua.DoString(Lua中静态方法名())调用测试
            lua.DoString("LuaChinarStaticTest()");

            lua.DoFile("ChinarClass.lua"); //Lua引用C#中的类

            Console.ReadKey(); //等待输入
        }

        void Test2()
        {
            //先初始化
            LuaScriptHelper.CreateInstance().LuaInit();

            //以下为执行调用
            string strInput = "fastColoredTextBox1.Text";//编辑器内容
            var objs = LuaScriptHelper.CreateInstance().RunLuaScript(strInput);
            if (objs != null && objs.Length > 0)
            {
                foreach (var obj in objs)
                {
                    Console.WriteLine(obj.ToString());
                }
            }
        }
    }
}
