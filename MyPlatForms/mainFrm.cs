using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yu3zx.SecurityUtil;
using System.IO.Ports;
using Yu3zx.Devices.CommonDevices;
using Yu3zx.Devices.Buses;
using Yu3zx.Devices;
using Yu3zx.Devices.Common;
using Yu3zx.DapperExtend;

namespace MyPlatForms
{
    public partial class mainFrm : Form
    {
        public mainFrm()
        {
            InitializeComponent();
        }
        VerifyCode vcode = new VerifyCode(5,0);
        string strCode ;
        private void btnVerificate_Click(object sender, EventArgs e)
        {
            picVerificate.Image = vcode.VerifyImage;
            txtShow.Text = vcode.VerifyText;
        }

        private void picVerificate_Click(object sender, EventArgs e)
        {
            //vcode.NextVerify();
            //picVerificate.Image = vcode.VerifyImage;
            //txtShow.Text = vcode.VerifyText;
            
            picVerificate.Image = VerifyCodeImageCreator.NextCode(out strCode);
            txtShow.Text = strCode;
        }

        SiemensPLC smPlc;
        WinCommBus comm;

        private void mainFrm_Load(object sender, EventArgs e)
        {
            //初始化下拉串口名称列表框
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            comboPortName.Items.AddRange(ports);
            comboPortName.SelectedIndex = comboPortName.Items.Count > 0 ? 0 : -1;
            comboBaudrate.SelectedIndex = comboBaudrate.Items.IndexOf("9600");

        }

        private bool Listening = false;     //是否没有执行完invoke相关操作
        private bool Closing = false;       //是否正在关闭串口，执行Application.DoEvents，并阻止再次invoke
        private void buttonOpenClose_Click(object sender, EventArgs e)
        {
            smPlc.DevOpen();
            return;
            if (comm.Connected)
            {
                Closing = true;
                while (Listening) Application.DoEvents();
                //打开时点击，则关闭串口
                comm.Close();
            }
            else
            {
                try
                {
                    comm.Open();
                }
                catch (Exception ex)
                {
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                    comm = new WinCommBus();
                    //现实异常信息给客户。
                    MessageBox.Show(ex.Message);
                }
            }
            //设置按钮的状态
            buttonOpenClose.Text = comm.Connected ? "Close" : "Open";
        }
        TcpClientBus TcpBus;
        UdpClientBus UdpBus;
        TcpServerSingleBus tssb;
        private void button1_Click(object sender, EventArgs e)
        {
            smPlc = new SiemensPLC();

            //TcpBus = new TcpClientBus();
            //TcpBus.ServerIP = "127.0.0.1";
            //TcpBus.ServerPort = 2756;

            //UdpBus = new UdpClientBus();
            //UdpBus.ServerIP = "127.0.0.1";
            //UdpBus.ServerPort = 6000;

            //关闭时点击，则设置好端口，波特率后打开
            comm = new WinCommBus();
            comm.WinPortName = comboPortName.Text;
            comm.WinBaudRate = int.Parse(comboBaudrate.Text);
            comm.WinDataBits = int.Parse(txt_DataBits.Text);//8;
            comm.WinStopBits = (StopBits)Enum.Parse(typeof(StopBits), cob_StopBits.Text);
            comm.WinParity = (Parity)Enum.Parse(typeof(Parity), cob_Parity.Text);
            comm.Init();
            //tssb = new TcpServerSingleBus();
            //tssb.ServerIP = "127.0.0.1";
            //tssb.ServerPort = 6000;
            
            //smPlc.BusConnector = comm;
            Pipe pipe = new Pipe();
            pipe.BusConnector = comm;
            //pipe.DevicesId.Add(smPlc.DeviceId);
            pipe.AddDevice(smPlc.DeviceId);
            smPlc.PipeId = pipe.PipeId;
            PipesManager.GetInstance().Pipes.Add(pipe);
            DevicesManager.GetInstance().Devices.Add(smPlc);
            string ss = "";
            ProgramManager.GetInstance().Devices.Clear();
            ProgramManager.GetInstance().Devices.Add(smPlc);
            ProgramManager.GetInstance().Save();
            PipesManager.GetInstance().Save();
            DevicesManager.GetInstance().Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ProgramManager.GetInstance().Devices.Count.ToString());

            if (true)//ProgramManager.GetInstance().Devices[0].Init
            {
                ProgramManager.GetInstance().Devices[0].DevWrite(System.Text.ASCIIEncoding.ASCII.GetBytes(txtShow.Text));
            }
            //smPlc.DevWrite(System.Text.ASCIIEncoding.ASCII.GetBytes(txtShow.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] rBytes = new byte[1024];
            int nHave = 0;
            smPlc.DevRead(rBytes, 1024, ref nHave);
            txtShow.Text += System.Text.ASCIIEncoding.ASCII.GetString(rBytes);
            MessageBox.Show(nHave.ToString());
        }

        private void btnDapper_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new UserEntity()
                {
                    Id = 1002,
                    UserName = "郏春燕",
                    Pwd = "5526655222",
                    Age = 29
                };

                using (var db = new DapperContext("SQLiteDbConnection"))
                {
                    
                    var result = db.Insert(model);
                    if (result)
                        Console.WriteLine("添加成功");
                    else
                        Console.WriteLine("添加失败");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void btnMuiltiIn_Click(object sender, EventArgs e)
        {
            var list = new List<UserEntity>();
            for (int i = 10; i < 21; i++)
            {
                var model = new UserEntity()
                {
                    UserName = "春燕" + i,
                    Pwd = "123456" + i,
                    Age = 15 + i
                };
                list.Add(model);
            }
            using (var db = new DapperContext())
            {
                var result = db.Insert(list);
                if (result)
                    Console.WriteLine("添加成功");
                else
                    Console.WriteLine("添加失败");
            }
        }

        private void btnDapperDel_Click(object sender, EventArgs e)
        {
            using (var db = new DapperContext())
            {
                var result = db.Delete<UserEntity>(u => u.Id == 10035);
                if (result)
                    Console.WriteLine("添加成功");
                else
                    Console.WriteLine("添加失败");
            }
        }

        private void btnUpdateDa_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new DapperContext())
                {
                    var entity = new UserEntity
                    {
                        Id = 10034,
                        Age = 30
                    };

                    var result = db.Update(u => new { u.Age, u.ModifiedTime }, entity);

                    var result1 = db.Update("update tuser set age=@age where Id=@Id", new { Id = 10033, Age = 23 });
                    if (result)
                        Console.WriteLine("添加成功");
                    else
                        Console.WriteLine("添加失败");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new DapperContext())
                {
                    var result = db.Select<UserEntity>(u => u.Valid == 1);

                    var query =
                        Query<UserEntity>.Builder(db)
                            .Select(u => new { u.Age, u.UserName })
                            .Where(u => u.Age > 10 && u.Age < 30)
                            .Top(3)
                            .OrderBy(u => new { u.Age, u.Id })
                            .OrderByDesc(u => new { u.AddTime, u.Pwd });

                    var result1 = db.Select(query: query);

                    var result4 = db.Select<UserEntity>("select * from tuser where valid=@valid", new { valid = 1 });

                    var result2 = db.Count<UserEntity>(u => u.Age > 30);

                    var result3 = db.Count<UserEntity>("select count(1) from tuser where valid=@valid", new { valid = 1 });

                    //Console.WriteLine(query.Sql);

                    Console.WriteLine("查询出数据条数:");
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void btnPages_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new DapperContext())
                {
                    var result = db.Page<UserEntity>(1, 3, u => u.Valid == 1, orderExpression: u => new { u.Id });

                    Console.WriteLine("查询出数据条数:");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnTran_Click(object sender, EventArgs e)
        {
            var model = new UserEntity()
            {
                UserName = "张三A",
                Pwd = "123456",
                Age = 15
            };

            using (var db = new DapperContext())
            {
                var tran = db.DbTransaction;
                try
                {
                    var result = db.Insert(model, tran);
                    var result1 = db.Delete<UserEntity>(u => u.Id == 10040, tran);
                    var result2 = db.Update<UserEntity>(u => new { u.Age }, new UserEntity() { Id = 10041, Age = 50 }, tran);
                    if (result && result1 && result2)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
