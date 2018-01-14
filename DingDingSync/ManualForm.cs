using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Operation;
using Conf;
using DBTools;

namespace DingDingSync
{
    public partial class ManualForm : Form
    {
        public ManualForm()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Configuration.Load();
            DateTime beginDate = this.dateTimePickerBegin.Value.Date;
            DateTime endDate = this.dateTimePickerEnd.Value.Date;

            int index = this.comboBox_Type.SelectedIndex;
            if (index == 0)
            {
                DataCollection.ProcessResultData(beginDate, endDate);
            }
            else if (index == 1)
            {
                DataCollection.ProcessScheudleListData(beginDate, endDate);
            }
            else if (index == 2)
            {
                DataCollection.ProcessInstanceData(beginDate, endDate);
            }
            else
            {
                MessageBox.Show("不合法的采集类型！");
                return;
            }

            foreach (string proc in Configuration.Procedures)
            {
                if (!string.IsNullOrEmpty(proc))
                {
                    DBUtility db = new DBUtility();
                    db.ExecuteProc(proc);
                }
            }

            MessageBox.Show("采集完毕！");
        }
    }
}