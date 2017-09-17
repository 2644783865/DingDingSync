namespace DingDingSync
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnToken = new System.Windows.Forms.Button();
            this.btnSaveDingDing = new System.Windows.Forms.Button();
            this.tbToken = new System.Windows.Forms.TextBox();
            this.lbToken = new System.Windows.Forms.Label();
            this.tbCorpSecret = new System.Windows.Forms.TextBox();
            this.tbCorpID = new System.Windows.Forms.TextBox();
            this.lbCorpSecret = new System.Windows.Forms.Label();
            this.lbCorpID = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSaveDBSet = new System.Windows.Forms.Button();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbDataBase = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.lbDB = new System.Windows.Forms.Label();
            this.lbPwd = new System.Windows.Forms.Label();
            this.lbUser = new System.Windows.Forms.Label();
            this.lbServer = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbProcessRange = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTimeRange = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetSchedule = new System.Windows.Forms.Button();
            this.lbMinute = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tbInterval = new System.Windows.Forms.TextBox();
            this.lbInterval = new System.Windows.Forms.Label();
            this.lbStartTime = new System.Windows.Forms.Label();
            this.chbWFResult = new System.Windows.Forms.CheckBox();
            this.chbKqSign = new System.Windows.Forms.CheckBox();
            this.chbKqSource = new System.Windows.Forms.CheckBox();
            this.chbKqPanBan = new System.Windows.Forms.CheckBox();
            this.notifyIcon_Service = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_Svcsetting = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRunSvc = new System.Windows.Forms.Button();
            this.btnStopSvc = new System.Windows.Forms.Button();
            this.lbRunInfo = new System.Windows.Forms.Label();
            this.btnInstallService = new System.Windows.Forms.Button();
            this.btnUninstallService = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPaiban = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.contextMenuStrip_Svcsetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnToken);
            this.groupBox1.Controls.Add(this.btnSaveDingDing);
            this.groupBox1.Controls.Add(this.tbToken);
            this.groupBox1.Controls.Add(this.lbToken);
            this.groupBox1.Controls.Add(this.tbCorpSecret);
            this.groupBox1.Controls.Add(this.tbCorpID);
            this.groupBox1.Controls.Add(this.lbCorpSecret);
            this.groupBox1.Controls.Add(this.lbCorpID);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "钉钉参数设置";
            // 
            // btnToken
            // 
            this.btnToken.Location = new System.Drawing.Point(44, 120);
            this.btnToken.Name = "btnToken";
            this.btnToken.Size = new System.Drawing.Size(75, 23);
            this.btnToken.TabIndex = 7;
            this.btnToken.Text = "获取Token";
            this.btnToken.UseVisualStyleBackColor = true;
            this.btnToken.Click += new System.EventHandler(this.btnToken_Click);
            // 
            // btnSaveDingDing
            // 
            this.btnSaveDingDing.Location = new System.Drawing.Point(141, 120);
            this.btnSaveDingDing.Name = "btnSaveDingDing";
            this.btnSaveDingDing.Size = new System.Drawing.Size(75, 23);
            this.btnSaveDingDing.TabIndex = 6;
            this.btnSaveDingDing.Text = "保存";
            this.btnSaveDingDing.UseVisualStyleBackColor = true;
            this.btnSaveDingDing.Click += new System.EventHandler(this.btnSaveDingDing_Click);
            // 
            // tbToken
            // 
            this.tbToken.Enabled = false;
            this.tbToken.Location = new System.Drawing.Point(84, 83);
            this.tbToken.Name = "tbToken";
            this.tbToken.PasswordChar = '*';
            this.tbToken.Size = new System.Drawing.Size(113, 20);
            this.tbToken.TabIndex = 5;
            this.tbToken.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // lbToken
            // 
            this.lbToken.AutoSize = true;
            this.lbToken.Location = new System.Drawing.Point(18, 86);
            this.lbToken.Name = "lbToken";
            this.lbToken.Size = new System.Drawing.Size(38, 13);
            this.lbToken.TabIndex = 4;
            this.lbToken.Text = "Token";
            // 
            // tbCorpSecret
            // 
            this.tbCorpSecret.Location = new System.Drawing.Point(84, 57);
            this.tbCorpSecret.Name = "tbCorpSecret";
            this.tbCorpSecret.PasswordChar = '*';
            this.tbCorpSecret.Size = new System.Drawing.Size(113, 20);
            this.tbCorpSecret.TabIndex = 3;
            // 
            // tbCorpID
            // 
            this.tbCorpID.Location = new System.Drawing.Point(84, 31);
            this.tbCorpID.Name = "tbCorpID";
            this.tbCorpID.PasswordChar = '*';
            this.tbCorpID.Size = new System.Drawing.Size(113, 20);
            this.tbCorpID.TabIndex = 2;
            // 
            // lbCorpSecret
            // 
            this.lbCorpSecret.AutoSize = true;
            this.lbCorpSecret.Location = new System.Drawing.Point(18, 57);
            this.lbCorpSecret.Name = "lbCorpSecret";
            this.lbCorpSecret.Size = new System.Drawing.Size(60, 13);
            this.lbCorpSecret.TabIndex = 1;
            this.lbCorpSecret.Text = "CorpSecret";
            // 
            // lbCorpID
            // 
            this.lbCorpID.AutoSize = true;
            this.lbCorpID.Location = new System.Drawing.Point(18, 31);
            this.lbCorpID.Name = "lbCorpID";
            this.lbCorpID.Size = new System.Drawing.Size(40, 13);
            this.lbCorpID.TabIndex = 0;
            this.lbCorpID.Text = "CorpID";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSaveDBSet);
            this.groupBox2.Controls.Add(this.btnTestConn);
            this.groupBox2.Controls.Add(this.tbUser);
            this.groupBox2.Controls.Add(this.tbPassword);
            this.groupBox2.Controls.Add(this.tbDataBase);
            this.groupBox2.Controls.Add(this.tbServer);
            this.groupBox2.Controls.Add(this.lbDB);
            this.groupBox2.Controls.Add(this.lbPwd);
            this.groupBox2.Controls.Add(this.lbUser);
            this.groupBox2.Controls.Add(this.lbServer);
            this.groupBox2.Location = new System.Drawing.Point(12, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(247, 270);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "本地连接设置";
            // 
            // btnSaveDBSet
            // 
            this.btnSaveDBSet.Location = new System.Drawing.Point(142, 167);
            this.btnSaveDBSet.Name = "btnSaveDBSet";
            this.btnSaveDBSet.Size = new System.Drawing.Size(75, 23);
            this.btnSaveDBSet.TabIndex = 9;
            this.btnSaveDBSet.Text = "保存";
            this.btnSaveDBSet.UseVisualStyleBackColor = true;
            this.btnSaveDBSet.Click += new System.EventHandler(this.btnSaveDBSet_Click);
            // 
            // btnTestConn
            // 
            this.btnTestConn.Location = new System.Drawing.Point(45, 167);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(75, 23);
            this.btnTestConn.TabIndex = 8;
            this.btnTestConn.Text = "连接测试";
            this.btnTestConn.UseVisualStyleBackColor = true;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(85, 60);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(113, 20);
            this.tbUser.TabIndex = 7;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(85, 87);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(113, 20);
            this.tbPassword.TabIndex = 6;
            // 
            // tbDataBase
            // 
            this.tbDataBase.Location = new System.Drawing.Point(85, 113);
            this.tbDataBase.Name = "tbDataBase";
            this.tbDataBase.Size = new System.Drawing.Size(113, 20);
            this.tbDataBase.TabIndex = 5;
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(85, 32);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(113, 20);
            this.tbServer.TabIndex = 4;
            // 
            // lbDB
            // 
            this.lbDB.AutoSize = true;
            this.lbDB.Location = new System.Drawing.Point(22, 116);
            this.lbDB.Name = "lbDB";
            this.lbDB.Size = new System.Drawing.Size(43, 13);
            this.lbDB.TabIndex = 3;
            this.lbDB.Text = "数据库";
            // 
            // lbPwd
            // 
            this.lbPwd.AutoSize = true;
            this.lbPwd.Location = new System.Drawing.Point(22, 90);
            this.lbPwd.Name = "lbPwd";
            this.lbPwd.Size = new System.Drawing.Size(31, 13);
            this.lbPwd.TabIndex = 2;
            this.lbPwd.Text = "密码";
            // 
            // lbUser
            // 
            this.lbUser.AutoSize = true;
            this.lbUser.Location = new System.Drawing.Point(22, 63);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(43, 13);
            this.lbUser.TabIndex = 1;
            this.lbUser.Text = "用户名";
            // 
            // lbServer
            // 
            this.lbServer.AutoSize = true;
            this.lbServer.Location = new System.Drawing.Point(22, 35);
            this.lbServer.Name = "lbServer";
            this.lbServer.Size = new System.Drawing.Size(43, 13);
            this.lbServer.TabIndex = 0;
            this.lbServer.Text = "服务器";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tbPaiban);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.tbProcessRange);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tbTimeRange);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btnSetSchedule);
            this.groupBox3.Controls.Add(this.lbMinute);
            this.groupBox3.Controls.Add(this.dateTimePicker1);
            this.groupBox3.Controls.Add(this.tbInterval);
            this.groupBox3.Controls.Add(this.lbInterval);
            this.groupBox3.Controls.Add(this.lbStartTime);
            this.groupBox3.Controls.Add(this.chbWFResult);
            this.groupBox3.Controls.Add(this.chbKqSign);
            this.groupBox3.Controls.Add(this.chbKqSource);
            this.groupBox3.Controls.Add(this.chbKqPanBan);
            this.groupBox3.Location = new System.Drawing.Point(296, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(322, 306);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "同步参数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(207, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "天";
            // 
            // tbProcessRange
            // 
            this.tbProcessRange.Location = new System.Drawing.Point(88, 149);
            this.tbProcessRange.Name = "tbProcessRange";
            this.tbProcessRange.Size = new System.Drawing.Size(113, 20);
            this.tbProcessRange.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "审批数据范围";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "小时";
            // 
            // tbTimeRange
            // 
            this.tbTimeRange.Location = new System.Drawing.Point(88, 114);
            this.tbTimeRange.Name = "tbTimeRange";
            this.tbTimeRange.Size = new System.Drawing.Size(113, 20);
            this.tbTimeRange.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "考勤数据范围";
            // 
            // btnSetSchedule
            // 
            this.btnSetSchedule.Location = new System.Drawing.Point(116, 240);
            this.btnSetSchedule.Name = "btnSetSchedule";
            this.btnSetSchedule.Size = new System.Drawing.Size(75, 23);
            this.btnSetSchedule.TabIndex = 11;
            this.btnSetSchedule.Text = "保存";
            this.btnSetSchedule.UseVisualStyleBackColor = true;
            this.btnSetSchedule.Click += new System.EventHandler(this.btnSetSchedule_Click);
            // 
            // lbMinute
            // 
            this.lbMinute.AutoSize = true;
            this.lbMinute.Location = new System.Drawing.Point(207, 80);
            this.lbMinute.Name = "lbMinute";
            this.lbMinute.Size = new System.Drawing.Size(31, 13);
            this.lbMinute.TabIndex = 10;
            this.lbMinute.Text = "分钟";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(88, 42);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(150, 20);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // tbInterval
            // 
            this.tbInterval.Location = new System.Drawing.Point(88, 77);
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Size = new System.Drawing.Size(113, 20);
            this.tbInterval.TabIndex = 8;
            // 
            // lbInterval
            // 
            this.lbInterval.AutoSize = true;
            this.lbInterval.Location = new System.Drawing.Point(3, 80);
            this.lbInterval.Name = "lbInterval";
            this.lbInterval.Size = new System.Drawing.Size(79, 13);
            this.lbInterval.TabIndex = 7;
            this.lbInterval.Text = "执行时间间隔";
            // 
            // lbStartTime
            // 
            this.lbStartTime.AutoSize = true;
            this.lbStartTime.Location = new System.Drawing.Point(3, 46);
            this.lbStartTime.Name = "lbStartTime";
            this.lbStartTime.Size = new System.Drawing.Size(79, 13);
            this.lbStartTime.TabIndex = 6;
            this.lbStartTime.Text = "开始执行时间";
            // 
            // chbWFResult
            // 
            this.chbWFResult.AutoSize = true;
            this.chbWFResult.Location = new System.Drawing.Point(98, 19);
            this.chbWFResult.Name = "chbWFResult";
            this.chbWFResult.Size = new System.Drawing.Size(74, 17);
            this.chbWFResult.TabIndex = 3;
            this.chbWFResult.Text = "审批记录";
            this.chbWFResult.UseVisualStyleBackColor = true;
            this.chbWFResult.Visible = false;
            // 
            // chbKqSign
            // 
            this.chbKqSign.AutoSize = true;
            this.chbKqSign.Location = new System.Drawing.Point(140, 19);
            this.chbKqSign.Name = "chbKqSign";
            this.chbKqSign.Size = new System.Drawing.Size(98, 17);
            this.chbKqSign.TabIndex = 2;
            this.chbKqSign.Text = "考勤签到记录";
            this.chbKqSign.UseVisualStyleBackColor = true;
            this.chbKqSign.Visible = false;
            this.chbKqSign.CheckedChanged += new System.EventHandler(this.chbKqSign_CheckedChanged);
            // 
            // chbKqSource
            // 
            this.chbKqSource.AutoSize = true;
            this.chbKqSource.Location = new System.Drawing.Point(45, 19);
            this.chbKqSource.Name = "chbKqSource";
            this.chbKqSource.Size = new System.Drawing.Size(98, 17);
            this.chbKqSource.TabIndex = 1;
            this.chbKqSource.Text = "考勤打卡记录";
            this.chbKqSource.UseVisualStyleBackColor = true;
            this.chbKqSource.Visible = false;
            // 
            // chbKqPanBan
            // 
            this.chbKqPanBan.AutoSize = true;
            this.chbKqPanBan.Location = new System.Drawing.Point(7, 19);
            this.chbKqPanBan.Name = "chbKqPanBan";
            this.chbKqPanBan.Size = new System.Drawing.Size(98, 17);
            this.chbKqPanBan.TabIndex = 0;
            this.chbKqPanBan.Text = "考勤排班记录";
            this.chbKqPanBan.UseVisualStyleBackColor = true;
            this.chbKqPanBan.Visible = false;
            this.chbKqPanBan.CheckedChanged += new System.EventHandler(this.chbKqPanBan_CheckedChanged);
            // 
            // notifyIcon_Service
            // 
            this.notifyIcon_Service.ContextMenuStrip = this.contextMenuStrip_Svcsetting;
            this.notifyIcon_Service.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_Service.Icon")));
            this.notifyIcon_Service.Text = "同鑫钉钉考勤服务";
            this.notifyIcon_Service.Visible = true;
            this.notifyIcon_Service.DoubleClick += new System.EventHandler(this.notifyIcon_Service_DoubleClick);
            // 
            // contextMenuStrip_Svcsetting
            // 
            this.contextMenuStrip_Svcsetting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数设置ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip_Svcsetting.Name = "contextMenuStrip_Svcsetting";
            this.contextMenuStrip_Svcsetting.Size = new System.Drawing.Size(125, 48);
            this.contextMenuStrip_Svcsetting.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // btnRunSvc
            // 
            this.btnRunSvc.Location = new System.Drawing.Point(481, 374);
            this.btnRunSvc.Name = "btnRunSvc";
            this.btnRunSvc.Size = new System.Drawing.Size(75, 23);
            this.btnRunSvc.TabIndex = 3;
            this.btnRunSvc.Text = "启动服务";
            this.btnRunSvc.UseVisualStyleBackColor = true;
            this.btnRunSvc.Click += new System.EventHandler(this.btnRunSvc_Click);
            // 
            // btnStopSvc
            // 
            this.btnStopSvc.Location = new System.Drawing.Point(481, 403);
            this.btnStopSvc.Name = "btnStopSvc";
            this.btnStopSvc.Size = new System.Drawing.Size(75, 23);
            this.btnStopSvc.TabIndex = 4;
            this.btnStopSvc.Text = "停止服务";
            this.btnStopSvc.UseVisualStyleBackColor = true;
            this.btnStopSvc.Click += new System.EventHandler(this.btnStopSvc_Click);
            // 
            // lbRunInfo
            // 
            this.lbRunInfo.AutoSize = true;
            this.lbRunInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRunInfo.ForeColor = System.Drawing.Color.Red;
            this.lbRunInfo.Location = new System.Drawing.Point(299, 447);
            this.lbRunInfo.Name = "lbRunInfo";
            this.lbRunInfo.Size = new System.Drawing.Size(0, 24);
            this.lbRunInfo.TabIndex = 6;
            // 
            // btnInstallService
            // 
            this.btnInstallService.Location = new System.Drawing.Point(355, 374);
            this.btnInstallService.Name = "btnInstallService";
            this.btnInstallService.Size = new System.Drawing.Size(75, 23);
            this.btnInstallService.TabIndex = 7;
            this.btnInstallService.Text = "安装服务";
            this.btnInstallService.UseVisualStyleBackColor = true;
            this.btnInstallService.Click += new System.EventHandler(this.btnInstallService_Click);
            // 
            // btnUninstallService
            // 
            this.btnUninstallService.Location = new System.Drawing.Point(355, 402);
            this.btnUninstallService.Name = "btnUninstallService";
            this.btnUninstallService.Size = new System.Drawing.Size(75, 23);
            this.btnUninstallService.TabIndex = 8;
            this.btnUninstallService.Text = "卸载服务";
            this.btnUninstallService.UseVisualStyleBackColor = true;
            this.btnUninstallService.Click += new System.EventHandler(this.btnUninstallService_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "天";
            // 
            // tbPaiban
            // 
            this.tbPaiban.Location = new System.Drawing.Point(88, 186);
            this.tbPaiban.Name = "tbPaiban";
            this.tbPaiban.Size = new System.Drawing.Size(113, 20);
            this.tbPaiban.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "排班数据范围";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 484);
            this.Controls.Add(this.btnUninstallService);
            this.Controls.Add(this.btnInstallService);
            this.Controls.Add(this.lbRunInfo);
            this.Controls.Add(this.btnStopSvc);
            this.Controls.Add(this.btnRunSvc);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "钉钉考勤同步";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UIForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.contextMenuStrip_Svcsetting.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSaveDingDing;
        private System.Windows.Forms.TextBox tbToken;
        private System.Windows.Forms.Label lbToken;
        private System.Windows.Forms.TextBox tbCorpSecret;
        private System.Windows.Forms.Label lbCorpSecret;
        private System.Windows.Forms.Label lbCorpID;
        private System.Windows.Forms.Label lbDB;
        private System.Windows.Forms.Label lbPwd;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label lbServer;
        private System.Windows.Forms.Button btnToken;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbDataBase;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.CheckBox chbKqSign;
        private System.Windows.Forms.CheckBox chbKqSource;
        private System.Windows.Forms.CheckBox chbKqPanBan;
        private System.Windows.Forms.NotifyIcon notifyIcon_Service;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Svcsetting;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.CheckBox chbWFResult;
        private System.Windows.Forms.Button btnSaveDBSet;
        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.Button btnRunSvc;
        private System.Windows.Forms.Button btnStopSvc;
        private System.Windows.Forms.Label lbRunInfo;
        private System.Windows.Forms.TextBox tbCorpID;
        private System.Windows.Forms.Label lbMinute;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox tbInterval;
        private System.Windows.Forms.Label lbInterval;
        private System.Windows.Forms.Label lbStartTime;
        private System.Windows.Forms.Button btnSetSchedule;
        private System.Windows.Forms.Button btnInstallService;
        private System.Windows.Forms.Button btnUninstallService;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTimeRange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbProcessRange;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPaiban;
        private System.Windows.Forms.Label label6;
    }
}

