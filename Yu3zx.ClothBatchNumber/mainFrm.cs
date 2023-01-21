using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yu3zx.ClothBatchNumber
{
    public partial class mainFrm : Form
    {
        public mainFrm()
        {
            InitializeComponent();
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var getStr = HttpRequest.GetStudent(1);
            Console.WriteLine(getStr);

            var getAll = HttpRequest.GetAllStudent();
            Console.WriteLine(getAll);

            var getPost = HttpRequest.GetStudentByPost(4);
            Console.WriteLine(getPost);

            var postStu = HttpRequest.StudentByPost(2);
            Console.WriteLine(postStu);
        }
    }
}
