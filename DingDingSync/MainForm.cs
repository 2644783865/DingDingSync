using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.ServiceProcess;
using System.Diagnostics;
using DesHelper;

using Newtonsoft.Json;

namespace DingDingSync
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public MainForm()
        {
            InitializeComponent();
            paramInit();

            //注册checkBox CheckedChanged事件
            this.chbKqPanBan.CheckedChanged += new System.EventHandler(this.chbKqPanBan_CheckedChanged);
            this.chbBanzhi.CheckedChanged += new System.EventHandler(this.chbBanzhi_CheckedChanged);
            this.chbKqSource.CheckedChanged += new System.EventHandler(this.chbKqSource_CheckedChanged);
            this.chbKqSign.CheckedChanged += new System.EventHandler(this.chbKqSign_CheckedChanged);
            this.chbWFResult.CheckedChanged += new System.EventHandler(this.chbWFResult_CheckedChanged);
        }

        private string _CorpID;
        private string _CorpSecret;

        private string _Server;
        private string _User;
        private string _Password;
        private string _DataBase;

        private string _XmlPath;
        private string _defaultInterval = "15";
        private string _tokenUrl = "https://oapi.dingtalk.com/gettoken?";
        private string _ServiceName = "TongXineSyncService";
        private ServiceAction _ServiceAction;

        #region DingTalk获取Token参数

        protected string CorpID
        {
            get
            {
                return this._CorpID;
            }
            set
            {
                this._CorpID = value;
            }
        }

        protected string CorpSecret
        {
            get
            {
                return this._CorpSecret;
            }
            set
            {
                this._CorpSecret = value;
            }
        }

        protected string XMLPath
        {
            get
            {
                return this._XmlPath;
            }
            set
            {
                this._XmlPath = value;
            }
        }

        #endregion DingTalk获取Token参数

        //本地数据库连接参数

        #region 数据库连接参数

        protected string Server
        {
            get
            {
                return this._Server;
            }
            set
            {
                this._Server = value;
            }
        }

        protected string User
        {
            get
            {
                return this._User;
            }
            set
            {
                this._User = value;
            }
        }

        protected string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }

        protected string DataBase
        {
            get
            {
                return this._DataBase;
            }
            set
            {
                this._DataBase = value;
            }
        }

        #endregion 数据库连接参数

        //从配置文件读取参数，初始化参数
        protected void paramInit()
        {
            try
            {
                XmlDocument xmlDoc = CreateXmlReader();

                this.tbCorpID.Text = DesHelper.DesHelper.DecryptString(GetSingleAttr(xmlDoc, "root/remote/corpid[1]"));
                this.tbCorpSecret.Text = DesHelper.DesHelper.DecryptString(GetSingleAttr(xmlDoc, "root/remote/corpsecret[1]"));
                this.tbToken.Text = DesHelper.DesHelper.DecryptString(GetSingleAttr(xmlDoc, "root/remote/token"));

                this.tbServer.Text = GetSingleAttr(xmlDoc, "root/localConnection/server[1]");
                this.tbUser.Text = GetSingleAttr(xmlDoc, "root/localConnection/user[1]");
                this.tbPassword.Text = DesHelper.DesHelper.DecryptString(GetSingleAttr(xmlDoc, "root/localConnection/password[1]"));
                this.tbDataBase.Text = GetSingleAttr(xmlDoc, "root/localConnection/database[1]");

                this.chbKqPanBan.Checked = GetSingleAttr(xmlDoc, "root/remote/apis/schedule[1]") == "1" ? true : false;
                this.chbBanzhi.Checked = GetSingleAttr(xmlDoc, "root/remote/apis/group[1]") == "1" ? true : false;
                this.chbKqSource.Checked = GetSingleAttr(xmlDoc, "root/remote/apis/attendenceSource[1]") == "1" ? true : false;
                this.chbKqSign.Checked = GetSingleAttr(xmlDoc, "root/remote/apis/attendenceSign[1]") == "1" ? true : false;
                this.chbWFResult.Checked = GetSingleAttr(xmlDoc, "root/remote/apis/auditResult[1]") == "1" ? true : false;

                this.dateTimePicker1.Value = DateTime.Parse(string.IsNullOrEmpty(GetSingleAttr(xmlDoc, "root/syncParam/startTime[1]")) ? DateTime.Now.ToString() : GetSingleAttr(xmlDoc, "root/syncParam/startTime[1]"));
                this.tbInterval.Text = GetSingleAttr(xmlDoc, "root/syncParam/interval[1]");
                this.tbTimeRange.Text = GetSingleAttr(xmlDoc, "root/syncParam/timeRange[1]");
                this.tbProcessRange.Text = GetSingleAttr(xmlDoc, "root/syncParam/processRange[1]");
                this.tbPaiban.Text = GetSingleAttr(xmlDoc, "root/syncParam/scheduleRange[1]");
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取配置文件ServiceSetting.xml失败，重新配置生成");
            }
        }

        public XmlDocument CreateXmlReader()
        {
            if (this.XMLPath == null)
            {
                this.XMLPath = @"./ServiceSetting.xml";
            }

            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings readSettings = new XmlReaderSettings()
            {
                IgnoreComments = true
            };
            XmlReader reader = null;
            try
            {
                reader = XmlReader.Create(this.XMLPath);
                xmlDoc.Load(reader);
            }
            catch (Exception e)
            {
                MessageBox.Show("配置文件ServiceSetting.xml读取失败！\r\n" + e.Message);
                return xmlDoc;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return xmlDoc;
        }

        //读取XML element属性值
        public string GetSingleAttr(XmlDocument xmlDoc, string nodeName, string attr = "value")
        {
            XmlNode node = xmlDoc.SelectSingleNode(nodeName);
            string retVal = ((XmlElement)node).GetAttribute(attr);
            return retVal;
        }

        //设置XML element属性值
        public void SetSingleAttr(XmlDocument xmlDoc, string nodeName, string attr, string attrVal)
        {
            XmlNode node = xmlDoc.SelectSingleNode(nodeName);
            ((XmlElement)node).SetAttribute(attr, attrVal);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;
                this.Hide();
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void notifyIcon_Service_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnRunSvc_Click(object sender, EventArgs e)
        {
            this.lbRunInfo.Text = "服务正在启动中...";
            if (_ServiceAction == null)
            {
                _ServiceAction = ServiceAction.CreateServiceAction(_ServiceName);
            }
            if (_ServiceAction.Start())
            {
                this.lbRunInfo.Text = "服务运行中...";
                this.btnRunSvc.Enabled = false;
                this.btnStopSvc.Enabled = true;
            }
            else
            {
                this.lbRunInfo.Text = "服务启动失败...";
            }
        }

        private void btnStopSvc_Click(object sender, EventArgs e)
        {
            if (_ServiceAction == null)
            {
                _ServiceAction = ServiceAction.CreateServiceAction(_ServiceName);
            }
            if (_ServiceAction.Stop())
            {
                this.lbRunInfo.Text = "服务已停止";
                this.btnRunSvc.Enabled = true;
                this.btnStopSvc.Enabled = false;
            }
            else
            {
                this.lbRunInfo.Text = "服务停止失败";
            }
        }

        //获取Token
        private void btnToken_Click(object sender, EventArgs e)
        {
            string retJson = string.Empty;

            string url = _tokenUrl + string.Format("corpid={0}&corpsecret={1}", this.tbCorpID.Text, this.tbCorpSecret.Text);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 5000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.101 Safari/537.36";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    retJson = sr.ReadToEnd();
                }
            }

            Token token = JsonConvert.DeserializeObject<Token>(retJson);
            if (token.errorcode == 0 && token.errmsg == "ok")
            {
                this.tbToken.Text = token.access_token;
                MessageBox.Show(string.Format("Token获取成功\r\n {0}{1}{2}", this.tbToken.Text.Substring(0, 5), "*********************", this.tbToken.Text.Substring(this.tbToken.Text.Length - 6)));
            }
            else
            {
                MessageBox.Show("Token获取失败");
            }
        }

        private void btnTestConn_Click(object sender, EventArgs e)
        {
            this.Server = this.tbServer.Text;
            this.User = this.tbUser.Text;
            this.Password = this.tbPassword.Text;
            this.DataBase = this.tbDataBase.Text;

            string connectionString = string.Format("Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                this.Password, this.User, this.DataBase, this.Server);

            SqlConnection sqlconn = new SqlConnection(connectionString);
            try
            {
                sqlconn.Open();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                return;
            }
            finally
            {
                sqlconn.Close();
            }
            MessageBox.Show("测试连接成功");
        }

        private void btnSaveDBSet_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = this.CreateXmlReader();

            string connectionString = string.Format("Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                this.Password, this.User, this.DataBase, this.Server);

            this.SetSingleAttr(xmlDoc, "root/localConnection/server[1]", "value", this.tbServer.Text);
            this.SetSingleAttr(xmlDoc, "root/localConnection/user[1]", "value", this.tbUser.Text);
            this.SetSingleAttr(xmlDoc, "root/localConnection/password[1]", "value", DesHelper.DesHelper.EncryptString(this.tbPassword.Text));
            this.SetSingleAttr(xmlDoc, "root/localConnection/database[1]", "value", this.tbDataBase.Text);
            this.SetSingleAttr(xmlDoc, "root/localConnection/connectionString[1]", "value", DesHelper.DesHelper.EncryptString(connectionString));

            xmlDoc.Save(this._XmlPath);
            xmlDoc = null;
        }

        private void btnSaveDingDing_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = CreateXmlReader();

            this.SetSingleAttr(xmlDoc, "root/remote/corpid[1]", "value", DesHelper.DesHelper.EncryptString(this.tbCorpID.Text));
            this.SetSingleAttr(xmlDoc, "root/remote/corpsecret[1]", "value", DesHelper.DesHelper.EncryptString(this.tbCorpSecret.Text));
            this.SetSingleAttr(xmlDoc, "root/remote/token[1]", "value", DesHelper.DesHelper.EncryptString(this.tbToken.Text));

            xmlDoc.Save(this.XMLPath);
            xmlDoc = null;
        }

        private void chbKqPanBan_CheckedChanged(object sender, EventArgs e)
        {
            string val = this.chbKqPanBan.CheckState == CheckState.Checked ? "1" : "0";
            XmlDocument xmlDoc = CreateXmlReader();
            this.SetSingleAttr(xmlDoc, "root/remote/apis/schedule", "value", val);
            xmlDoc.Save(this.XMLPath);
            xmlDoc = null;
        }

        private void chbKqSource_CheckedChanged(object sender, EventArgs e)
        {
            string val = this.chbKqSource.CheckState == CheckState.Checked ? "1" : "0";
            XmlDocument xmlDoc = CreateXmlReader();
            this.SetSingleAttr(xmlDoc, "root/remote/apis/attendenceSource", "value", val);
            xmlDoc.Save(this.XMLPath);
            xmlDoc = null;
        }

        private void chbWFResult_CheckedChanged(object sender, EventArgs e)
        {
            string val = this.chbWFResult.CheckState == CheckState.Checked ? "1" : "0";
            XmlDocument xmlDoc = CreateXmlReader();
            this.SetSingleAttr(xmlDoc, "root/remote/apis/auditResult", "value", val);
            xmlDoc.Save(this.XMLPath);
            xmlDoc = null;
        }

        private void chbKqSign_CheckedChanged(object sender, EventArgs e)
        {
            string val = this.chbKqSign.CheckState == CheckState.Checked ? "1" : "0";
            XmlDocument xmlDoc = CreateXmlReader();
            this.SetSingleAttr(xmlDoc, "root/remote/apis/attendenceSign", "value", val);
            xmlDoc.Save(this.XMLPath);
            xmlDoc = null;
        }

        private void chbBanzhi_CheckedChanged(object sender, EventArgs e)
        {
            string val = this.chbBanzhi.CheckState == CheckState.Checked ? "1" : "0";
            XmlDocument xmlDoc = CreateXmlReader();
            this.SetSingleAttr(xmlDoc, "root/remote/apis/group", "value", val);
            xmlDoc.Save(this.XMLPath);
            xmlDoc = null;
        }

        private void btnSetSchedule_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = CreateXmlReader();
            string startDate = this.dateTimePicker1.Value.ToString();
            string interval = this.tbInterval.Text;
            interval = string.IsNullOrEmpty(interval) ? this._defaultInterval : interval;
            string timeRange = this.tbTimeRange.Text;
            timeRange = string.IsNullOrEmpty(timeRange) ? "12" : timeRange;
            string processRange = this.tbProcessRange.Text;
            processRange = string.IsNullOrEmpty(processRange) ? "10" : processRange;
            string paibanRange = this.tbPaiban.Text;
            paibanRange = string.IsNullOrEmpty(paibanRange) ? "3" : paibanRange;

            this.SetSingleAttr(xmlDoc, "root/syncParam/startTime", "value", startDate);
            this.SetSingleAttr(xmlDoc, "root/syncParam/interval", "value", interval);
            this.SetSingleAttr(xmlDoc, "root/syncParam/timeRange", "value", timeRange);
            this.SetSingleAttr(xmlDoc, "root/syncParam/processRange", "value", processRange);
            this.SetSingleAttr(xmlDoc, "root/syncParam/scheduleRange", "value", paibanRange);
            xmlDoc.Save(this.XMLPath);
            xmlDoc = null;
        }

        private void btnInstallService_Click(object sender, EventArgs e)
        {
            ServiceController[] controllers = ServiceController.GetServices();
            foreach (ServiceController controller in controllers)
            {
                if (controller.ServiceName == "TongXineSyncService")
                {
                    MessageBox.Show("服务已存在，请勿重复安装或卸载后在进行安装");
                    return;
                }
            }

            Process process = new Process();
            process.StartInfo.FileName = "InstallUtil.exe";
            process.StartInfo.Arguments = "TongXineSyncService.exe";
            process.Start();
            process.WaitForExit();

            controllers = ServiceController.GetServices();
            foreach (ServiceController controller in controllers)
            {
                if (controller.ServiceName == "TongXineSyncService")
                {
                    MessageBox.Show("服务安装成功");
                    return;
                }
            }
            MessageBox.Show("服务安装失败");
        }

        private void btnUninstallService_Click(object sender, EventArgs e)
        {
            ServiceController[] controllers = ServiceController.GetServices();
            foreach (var controller in controllers)
            {
                if (controller.ServiceName == "TongXineSyncService" && controller.Status == ServiceControllerStatus.Stopped)
                {
                    try
                    {
                        Process process = new Process();
                        process.StartInfo.FileName = "InstallUtil.exe";
                        process.StartInfo.Arguments = "-u TongXineSyncService.exe";
                        //process.StartInfo.CreateNoWindow = true;
                        //process.StartInfo.UseShellExecute = false;
                        process.Start();
                        process.WaitForExit();
                        MessageBox.Show("服务卸载成功");
                        this.btnInstallService.Enabled = true;
                        return;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
            MessageBox.Show("服务不存在或服务尚在运行,请停止服务后在尝试卸载");
        }

        //手动补采
        private void btn_normal_Click(object sender, EventArgs e)
        {
            ManualForm manualForm = new ManualForm();
            manualForm.ShowDialog();
        }
    }

    internal class ServiceAction
    {
        private ServiceController _sc;
        private static ServiceAction _serviceAction;

        private ServiceAction(string serviceName)
        {
            _sc = new ServiceController(serviceName);
        }

        public static ServiceAction CreateServiceAction(string serviceName)
        {
            if (_serviceAction == null)
            {
                _serviceAction = new ServiceAction(serviceName);
            }
            return _serviceAction;
        }

        public bool Start()
        {
            bool ret = false;
            _sc.Refresh();
            try
            {
                if (_sc.Status == ServiceControllerStatus.Stopped)
                {
                    _sc.Start();
                    ret = true;
                }
                else if (_sc.Status == ServiceControllerStatus.Running)
                {
                    ret = true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                _serviceAction = null;
                ret = false;
            }
            return ret;
        }

        public bool Stop()
        {
            bool ret = false;
            _sc.Refresh();
            try
            {
                if (_sc.Status == ServiceControllerStatus.Running)
                {
                    _sc.Stop();
                    ret = true;
                }
                else if (_sc.Status == ServiceControllerStatus.Stopped)
                {
                    ret = true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                _serviceAction = null;
                ret = false;
            }
            return ret;
        }
    }

    internal class Token
    {
        private int _errcode;
        private string _errmsg;
        private string _access_token;

        public int errorcode
        {
            get
            {
                return this._errcode;
            }
            set
            {
                this._errcode = value;
            }
        }

        public string errmsg
        {
            get
            {
                return this._errmsg;
            }
            set
            {
                this._errmsg = value;
            }
        }

        public string access_token
        {
            get
            {
                return this._access_token;
            }
            set
            {
                this._access_token = value;
            }
        }
    }
}