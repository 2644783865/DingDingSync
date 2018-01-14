namespace ManualForm
{
    partial class ManulForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManulForm));
            this.labelType = new System.Windows.Forms.Label();
            this.labelDateBegin = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dtPickerBegin = new System.Windows.Forms.DateTimePicker();
            this.dtPickerEnd = new System.Windows.Forms.DateTimePicker();
            this.labelDateEnd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(13, 27);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(55, 13);
            this.labelType.TabIndex = 0;
            this.labelType.Text = "采集类型";
            // 
            // labelDateBegin
            // 
            this.labelDateBegin.AutoSize = true;
            this.labelDateBegin.Location = new System.Drawing.Point(12, 69);
            this.labelDateBegin.Name = "labelDateBegin";
            this.labelDateBegin.Size = new System.Drawing.Size(55, 13);
            this.labelDateBegin.TabIndex = 1;
            this.labelDateBegin.Text = "开始日期";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(163, 171);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "采集";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(90, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // dtPickerBegin
            // 
            this.dtPickerBegin.CustomFormat = "yyyy-mm-dd";
            this.dtPickerBegin.Location = new System.Drawing.Point(90, 69);
            this.dtPickerBegin.Name = "dtPickerBegin";
            this.dtPickerBegin.Size = new System.Drawing.Size(200, 20);
            this.dtPickerBegin.TabIndex = 4;
            this.dtPickerBegin.Value = new System.DateTime(2017, 11, 21, 15, 43, 31, 0);
            // 
            // dtPickerEnd
            // 
            this.dtPickerEnd.CustomFormat = "yyyy-mm-dd";
            this.dtPickerEnd.Location = new System.Drawing.Point(90, 112);
            this.dtPickerEnd.Name = "dtPickerEnd";
            this.dtPickerEnd.Size = new System.Drawing.Size(200, 20);
            this.dtPickerEnd.TabIndex = 5;
            // 
            // labelDateEnd
            // 
            this.labelDateEnd.AutoSize = true;
            this.labelDateEnd.Location = new System.Drawing.Point(12, 112);
            this.labelDateEnd.Name = "labelDateEnd";
            this.labelDateEnd.Size = new System.Drawing.Size(55, 13);
            this.labelDateEnd.TabIndex = 6;
            this.labelDateEnd.Text = "结束日期";
            // 
            // ManulForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 244);
            this.Controls.Add(this.labelDateEnd);
            this.Controls.Add(this.dtPickerEnd);
            this.Controls.Add(this.dtPickerBegin);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.labelDateBegin);
            this.Controls.Add(this.labelType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManulForm";
            this.Text = "手动补采";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label labelDateBegin;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dtPickerBegin;
        private System.Windows.Forms.DateTimePicker dtPickerEnd;
        private System.Windows.Forms.Label labelDateEnd;
    }
}

