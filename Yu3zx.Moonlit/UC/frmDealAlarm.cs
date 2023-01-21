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
    public partial class frmDealAlarm : Form
    {
        public frmDealAlarm()
        {
            InitializeComponent();
        }

        private void frmDealAlarm_Load(object sender, EventArgs e)
        {
            //dgvShow.DataSource = AlarmManager.GetInstance().AlarmList;
            InitDgv();
        }

        private void InitDgv()
        {
            int iRow = 0;
            dgvShow.Rows.Clear();
            for (int i = 0; i < AlarmManager.GetInstance().AlarmList.Count; i++)
            {
                if (AlarmManager.GetInstance().AlarmList[i].IsKeep)
                {
                    iRow = dgvShow.Rows.Add();
                    dgvShow.Rows[iRow].Cells["AlarmSource"].Value =
                        AlarmManager.GetInstance().AlarmList[i].AlarmSource;
                    dgvShow.Rows[iRow].Cells["AlarmName"].Value =
                        AlarmManager.GetInstance().AlarmList[i].AlarmName;
                    dgvShow.Rows[iRow].Cells["AlarmMsg"].Value =
                        AlarmManager.GetInstance().AlarmList[i].AlarmMsg;
                    dgvShow.Rows[iRow].Cells["AlarmAddTime"].Value =
                        AlarmManager.GetInstance().AlarmList[i].AlarmAddTime;
                    dgvShow.Rows[iRow].Cells["DevId"].Value =
                        AlarmManager.GetInstance().AlarmList[i].DevId;
                    dgvShow.Rows[iRow].Cells["AlarmClear"].Value = "报警消除";
                    dgvShow.Rows[iRow].Tag = AlarmManager.GetInstance().AlarmList[i];
                }
            }
        }

        private void dgvShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvShow.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > -1)
            {
                string strCellValue = dgvShow.Rows[e.RowIndex].Cells["AlarmClear"].Value.ToString();
                if (strCellValue == "报警消除")
                {
                    Alarm sAla = dgvShow.Rows[e.RowIndex].Tag as Alarm;
                    sAla.AlarmRemoveTime = DateTime.Now;
                    sAla.IsKeep = false;
                    RefreshMarker(sAla);

                    InitDgv();
                }
            }
        }

        private void RefreshMarker(Alarm alaShow)
        {
        }
    }
}
