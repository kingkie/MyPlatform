using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yu3zx.ClothServer
{
    public partial class mainFrm : Form
    {
        Thread thData = null;
        public mainFrm()
        {
            InitializeComponent();
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            thData = new Thread(DataAction);
            thData.IsBackground = true;
            thData.Name = "Data";


        }
        /// <summary>
        /// 数据处理线程
        /// </summary>
        private void DataAction()
        {
            try
            {

            }
            catch
            {
            }
        }
    }
}
