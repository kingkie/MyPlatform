using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yu3zx.Util;
using Yu3zx.DataBases;
using System.IO;
using Yu3zx.Moonlit.bean;

namespace Yu3zx.Moonlit
{
    public partial class frmUserLogin : Form
    {
        public frmUserLogin()
        {
            InitializeComponent();
        }
        VerifyCode vCode = new VerifyCode(5, 0);
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }
        
private void txtUserPwd_KeyDown(object sender, KeyEventArgs e)
      {  
          if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
          {  
              this.btnOK_Click(sender, e);//触发button事件  
          }  
      }

private void btnOK_Click(object sender, EventArgs e)
        {
            MainManager.GetInstance().DbFactory = new MysqlFactory(ProgramManager.GetInstance().DbConnString);
            if (!MainManager.GetInstance().DbFactory.ConnectionTest())
            {
                MessageBox.Show("数据库链接错误！");
                return;
                //Environment.Exit(0);
            }
            if (!"1".Equals(LoadLicense.GetLicenseModel.Valid))
            { //不是有效的
                MessageBox.Show("软件授权到期，系统不能使用！");
                return;
            }
            if (string.IsNullOrEmpty(txtUserId.Text))
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(txtUserPwd.Text))
            {
                MessageBox.Show("请输入用户密码！");
                return;
            }
            //if (txtVerificate.Text.ToUpper() != vCode.VerifyText)
            //{
            //    MessageBox.Show("验证码错误！");
            //    //picCode_Click(sender,e);
            //    //txtVerificate.Text = "";
            //    return;
            //}

            string pwdmsg = EncryptUtil.Md5(txtUserPwd.Text);
            string sqlStr = "select * from users where userid = '" + txtUserId.Text + "' and userpwd = '" + pwdmsg + "'";
            DataSet dsUser = MainManager.GetInstance().DbFactory.ExecuteDataset(sqlStr, "users");
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                //登录成功后记录用户
                UserUtil.setUser(dsUser.Tables[0].Rows[0]["userid"].ToString(), dsUser.Tables[0].Rows[0]["username"].ToString());
                //MainManager.GetInstance().SoftUser = new Users.User();
                //MainManager.GetInstance().SoftUser.UserId = dsUser.Tables[0].Rows[0]["userid"].ToString();
                //MainManager.GetInstance().SoftUser.UserName = dsUser.Tables[0].Rows[0]["username"].ToString();
                //MainManager.GetInstance().SoftUser.Authority = (UserLevel)int.Parse(dsUser.Tables[0].Rows[0]["authority"].ToString());
                this.Hide();
                (new mainFrm()).Show();
                //string strlog = "insert into uselogs(userid,operationlog,addtime) values('" + MainManager.GetInstance().SoftUser.UserId +
                //                "','" + MainManager.GetInstance().SoftUser.UserName + "，登录。" + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //MainManager.GetInstance().DbFactory.ExecuteNonQuery(strlog);
            }
            else
            {
                MessageBox.Show("用户名或者密码错误！");
                //string sqlins = "insert into users(userid,userpwd,authority,username) values ('admin2018','" + pwdmsg + "'," + ((int)UserLevel.Admin).ToString() + ",'南京波轩')";
                //MainManager.GetInstance().DbFactory.ExecuteNonQuery(sqlins);
                picCode_Click(sender, e);
                //txtVerificate.Text = "";
            }
        }

        private void frmUserLogin_Load(object sender, EventArgs e)
        {
            //1、读取授权文件 并再标题显示，当授权到期后，无法登录系统
            bool bool1 = LoadLicense.isLicense();
            txtDbLink.Text = ProgramManager.GetInstance().DbConnString;
            //picCode.Image = vCode.VerifyImage;
            if (!bool1)
            { //当授权到期后直接提示然后不让登录
                this.Text = this.Text + "未授权版"+ LoadLicense.GetLicenseModel.Time;
                MessageBox.Show("您的软件授权已到期，系统将无法登入！");
            }
            else {
                string company= LoadLicense.GetLicenseModel.Company;
                string type= LoadLicense.GetLicenseModel.Type;
                string titleText = this.Text;
                if ("1".Equals(type)) { //正式版
                    titleText += " 正式版";
                }
                else {
                    titleText += " 试用版" + LoadLicense.GetLicenseModel.Time;
                }
                this.Text = titleText;
            }
            btnDbSet_Click(null, null);

            this.txtUserPwd.KeyDown+= this.txtUserPwd_KeyDown;// new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtUserPwd_KeyDown);
        }

        private void picCode_Click(object sender, EventArgs e)
        {
            vCode.NextVerify();
            //picCode.Image = vCode.VerifyImage;
        }

        private void btnDbSet_Click(object sender, EventArgs e)
        {
            if(pnlDbSet.Visible == false)
            {
                this.Height = this.Height + 60;
                pnlDbSet.Visible = true;
            }
            else
            {
                this.Height = this.Height - 60;
                pnlDbSet.Visible = false;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty( txtDbLink.Text))
            {
                MessageBox.Show("数据库连接不能为空！");
                return;
            }
            ProgramManager.GetInstance().DbConnString = txtDbLink.Text.Trim();
            ProgramManager.GetInstance().Save();
        }
    }
}
