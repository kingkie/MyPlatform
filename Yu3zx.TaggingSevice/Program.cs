using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yu3zx.TaggingSevice
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mutex = new System.Threading.Mutex(true, "TaggingSevice");
            if (mutex.WaitOne(0, false))
            {
                Application.Run(new mainFrm());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
