using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yu3zx.Moonlit.Modols;

namespace Yu3zx.Moonlit
{
    public partial class frmAlarm : Form
    {
        #region 变量属性
        /// <summary>
        /// 控制窗口动画效果
        /// </summary>
        private Timer Timer = null;
        private int _keeptime = 5;//保持5秒

        private Alarm _alarm;

        /// <summary>
        /// 计算时间
        /// </summary>
        private int Interval = 0;

        /// <summary>
        /// 窗体保持时间
        /// </summary>
        public int KeepTime
        {
            set
            {
                _keeptime = value;
            }
            get{ return _keeptime;}
        }
        #endregion



        public frmAlarm()
        {
            this.SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.Selectable |
            ControlStyles.ContainerControl |
            ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.UpdateStyles();
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
        }

        public frmAlarm(Alarm _alarmin)
        {
            this.SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.Selectable |
            ControlStyles.ContainerControl,true );//|
            //ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.UpdateStyles();
            //this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
            _alarm = _alarmin;

            if (_alarm != null)
            {
                txtAddTime.Text = _alarm.AlarmAddTime.ToString("yyyy-MM-dd HH:mm:ss");
                txtAlarmName.Text = _alarm.AlarmName;
                txtAlarmSource.Text = _alarm.AlarmSource;
                txtAlarmDesc.Text = _alarm.AlarmMsg;
            }
        }

        /// <summary>
        /// 在窗口加载时，初始化部分数据
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                int xPos = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
                int yPos = Screen.PrimaryScreen.WorkingArea.Height;
                this.Location = new Point(xPos, yPos);
                this.TopMost = true;
                this.TopLevel = true;
                this.ShowIcon = false;
                base.BackColor = Color.FromArgb(0, 122, 204);
            }
        }

        /// <summary>
        /// 将以动画的形式显示，默认存在时间为 5 秒
        /// </summary>
        /// <param name="caption">窗口标题</param>
        /// <param name="text">窗口内容</param>
        public void AnimalShow()
        {
            try
            {
                if (Timer == null)
                {
                    Timer = new System.Windows.Forms.Timer();
                    Timer.Interval = 100;
                    Timer.Tick += new EventHandler(Timer_Tick);
                    Timer.Start();
                }
                else
                {
                    Timer.Interval = 100;
                    Timer.Tick += new EventHandler(Timer_Tick);
                    Timer.Enabled = true;
                }
                if (this != null)
                {
                    this.Show();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        /// <summary>
        /// 执行窗口动画操作
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                // 动态改变窗口位置
                int pos = this.Height / 100;
                int top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
                if (this.Top > top + pos * 10)
                {
                    for (int i = 0; i < 35; i++)
                    {
                        this.Top -= pos;
                    }
                }
                else
                {
                    this.Top = top;
                    if (this._keeptime > this.Interval)
                    {
                        Timer.Interval = 1000;
                        Interval++;
                    }
                    else
                    {
                        Timer.Interval = 100;
                        if (this.Opacity > 0)  // 动画降低窗口透明度
                        {
                            this.Opacity -= 0.1;
                        }
                        else                                // 释放窗口资源
                        {
                            this.Interval = 0;
                            this.Timer.Enabled = false;
                            this.Timer.Dispose();
                            this.Timer = null;
                            this.Dispose();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    /// <summary>
    /// 鼠标状态
    /// </summary>
    public enum EMouseState
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal,
        /// <summary>
        /// 鼠标划过
        /// </summary>
        Move,
        /// <summary>
        /// 鼠标按下
        /// </summary>
        Down,
        /// <summary>
        /// 鼠标释放
        /// </summary>
        Up,
        /// <summary>
        /// 鼠标离开
        /// </summary>
        Leave,
    }

    /// <summary>
    /// 系统控制按钮
    /// </summary>
    public enum ESysButton
    {
        /// <summary>
        /// 默认-最小化，最大化，关闭
        /// </summary>
        Normal,
        /// <summary>
        /// 关闭按钮
        /// </summary>
        Close,
        /// <summary>
        /// 关闭按钮，最小化
        /// </summary>
        Close_Mini,
    }
}
