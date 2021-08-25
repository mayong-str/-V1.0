namespace dispense
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_Open = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_Gas = new System.Windows.Forms.Button();
            this.button_Power = new System.Windows.Forms.Button();
            this.button_Water = new System.Windows.Forms.Button();
            this.button_Air = new System.Windows.Forms.Button();
            textBox_GetValue = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            textBox1_LocalID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            textBox_servername = new System.Windows.Forms.TextBox();
            textBox_localID = new System.Windows.Forms.TextBox();
            textBox_type = new System.Windows.Forms.TextBox();
            textBox_serialnumber = new System.Windows.Forms.TextBox();
            textBox_Date = new System.Windows.Forms.TextBox();
            this.button_All = new System.Windows.Forms.Button();
            this.textBox_Time = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            textBox_ID = new System.Windows.Forms.TextBox();
            this.button_Clear = new System.Windows.Forms.Button();
            this.textBox_Value = new System.Windows.Forms.TextBox();
            textBox_GetNull = new System.Windows.Forms.TextBox();
            textBox_fuwuqi = new System.Windows.Forms.TextBox();
            this.button_shoudongOut = new System.Windows.Forms.Button();
            this.button_DefaultFolder_Path = new System.Windows.Forms.Button();
            textBox_valueType = new System.Windows.Forms.TextBox();
            textBox1 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_DefaultFolder_Path = new System.Windows.Forms.Label();
            this.label_Folder_Path = new System.Windows.Forms.Label();
            this.label_Open = new System.Windows.Forms.Label();
            this.button_Folder_Path = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_valueType = new System.Windows.Forms.Label();
            this.label_endTime = new System.Windows.Forms.Label();
            this.label_startTime = new System.Windows.Forms.Label();
            this.button_summary = new System.Windows.Forms.Button();
            this.comboBox_valueType = new System.Windows.Forms.ComboBox();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Open
            // 
            this.button_Open.Location = new System.Drawing.Point(148, 32);
            this.button_Open.Name = "button_Open";
            this.button_Open.Size = new System.Drawing.Size(72, 27);
            this.button_Open.TabIndex = 0;
            this.button_Open.Text = "Browse";
            this.button_Open.UseVisualStyleBackColor = true;
            this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(309, 69);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1035, 668);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button_Gas
            // 
            this.button_Gas.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Gas.Location = new System.Drawing.Point(404, 36);
            this.button_Gas.Name = "button_Gas";
            this.button_Gas.Size = new System.Drawing.Size(87, 27);
            this.button_Gas.TabIndex = 3;
            this.button_Gas.Text = "GAS";
            this.button_Gas.UseVisualStyleBackColor = true;
            this.button_Gas.Click += new System.EventHandler(this.button_Gas_Click);
            // 
            // button_Power
            // 
            this.button_Power.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Power.Location = new System.Drawing.Point(498, 36);
            this.button_Power.Name = "button_Power";
            this.button_Power.Size = new System.Drawing.Size(87, 27);
            this.button_Power.TabIndex = 4;
            this.button_Power.Text = "POWER";
            this.button_Power.UseVisualStyleBackColor = true;
            this.button_Power.Click += new System.EventHandler(this.button_Power_Click);
            // 
            // button_Water
            // 
            this.button_Water.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Water.Location = new System.Drawing.Point(593, 36);
            this.button_Water.Name = "button_Water";
            this.button_Water.Size = new System.Drawing.Size(87, 27);
            this.button_Water.TabIndex = 5;
            this.button_Water.Text = "WATER";
            this.button_Water.UseVisualStyleBackColor = true;
            this.button_Water.Click += new System.EventHandler(this.button_Water_Click);
            // 
            // button_Air
            // 
            this.button_Air.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Air.Location = new System.Drawing.Point(309, 36);
            this.button_Air.Name = "button_Air";
            this.button_Air.Size = new System.Drawing.Size(87, 27);
            this.button_Air.TabIndex = 6;
            this.button_Air.Text = "AIR";
            this.button_Air.UseVisualStyleBackColor = true;
            this.button_Air.Click += new System.EventHandler(this.button_Air_Click);
            // 
            // textBox_GetValue
            // 
            textBox_GetValue.Location = new System.Drawing.Point(1429, 104);
            textBox_GetValue.Multiline = true;
            textBox_GetValue.Name = "textBox_GetValue";
            textBox_GetValue.Size = new System.Drawing.Size(138, 23);
            textBox_GetValue.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3600000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // textBox1_LocalID
            // 
            textBox1_LocalID.Location = new System.Drawing.Point(1471, 55);
            textBox1_LocalID.Multiline = true;
            textBox1_LocalID.Name = "textBox1_LocalID";
            textBox1_LocalID.Size = new System.Drawing.Size(94, 21);
            textBox1_LocalID.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1469, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 11;
            this.label1.Text = "配置的localID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1427, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 14);
            this.label2.TabIndex = 12;
            this.label2.Text = "配置localID对应的值";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBox_servername
            // 
            textBox_servername.Location = new System.Drawing.Point(1454, 384);
            textBox_servername.Name = "textBox_servername";
            textBox_servername.Size = new System.Drawing.Size(116, 23);
            textBox_servername.TabIndex = 19;
            // 
            // textBox_localID
            // 
            textBox_localID.Location = new System.Drawing.Point(1454, 433);
            textBox_localID.Name = "textBox_localID";
            textBox_localID.Size = new System.Drawing.Size(116, 23);
            textBox_localID.TabIndex = 20;
            // 
            // textBox_type
            // 
            textBox_type.Location = new System.Drawing.Point(1454, 476);
            textBox_type.Name = "textBox_type";
            textBox_type.Size = new System.Drawing.Size(116, 23);
            textBox_type.TabIndex = 21;
            // 
            // textBox_serialnumber
            // 
            textBox_serialnumber.Location = new System.Drawing.Point(1454, 527);
            textBox_serialnumber.Name = "textBox_serialnumber";
            textBox_serialnumber.Size = new System.Drawing.Size(116, 23);
            textBox_serialnumber.TabIndex = 22;
            // 
            // textBox_Date
            // 
            textBox_Date.Location = new System.Drawing.Point(1454, 574);
            textBox_Date.Name = "textBox_Date";
            textBox_Date.Size = new System.Drawing.Size(116, 23);
            textBox_Date.TabIndex = 24;
            // 
            // button_All
            // 
            this.button_All.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_All.Location = new System.Drawing.Point(689, 36);
            this.button_All.Name = "button_All";
            this.button_All.Size = new System.Drawing.Size(87, 27);
            this.button_All.TabIndex = 25;
            this.button_All.Text = "ALL";
            this.button_All.UseVisualStyleBackColor = true;
            this.button_All.Click += new System.EventHandler(this.button_All_Click);
            // 
            // textBox_Time
            // 
            this.textBox_Time.Location = new System.Drawing.Point(148, 140);
            this.textBox_Time.Name = "textBox_Time";
            this.textBox_Time.Size = new System.Drawing.Size(72, 23);
            this.textBox_Time.TabIndex = 26;
            this.textBox_Time.Text = "53";
            this.textBox_Time.TextChanged += new System.EventHandler(this.textBox_Time_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 14);
            this.label5.TabIndex = 27;
            this.label5.Text = "定时导出时间(分)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(1324, 709);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 14);
            this.label6.TabIndex = 28;
            // 
            // textBox_ID
            // 
            textBox_ID.Location = new System.Drawing.Point(1451, 245);
            textBox_ID.Name = "textBox_ID";
            textBox_ID.Size = new System.Drawing.Size(116, 23);
            textBox_ID.TabIndex = 30;
            // 
            // button_Clear
            // 
            this.button_Clear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Clear.Location = new System.Drawing.Point(812, 36);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(87, 27);
            this.button_Clear.TabIndex = 32;
            this.button_Clear.Text = "清除";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // textBox_Value
            // 
            this.textBox_Value.Location = new System.Drawing.Point(1451, 293);
            this.textBox_Value.Name = "textBox_Value";
            this.textBox_Value.Size = new System.Drawing.Size(116, 23);
            this.textBox_Value.TabIndex = 33;
            // 
            // textBox_GetNull
            // 
            textBox_GetNull.Location = new System.Drawing.Point(1451, 147);
            textBox_GetNull.Name = "textBox_GetNull";
            textBox_GetNull.Size = new System.Drawing.Size(116, 23);
            textBox_GetNull.TabIndex = 35;
            // 
            // textBox_fuwuqi
            // 
            textBox_fuwuqi.Location = new System.Drawing.Point(1451, 198);
            textBox_fuwuqi.Name = "textBox_fuwuqi";
            textBox_fuwuqi.Size = new System.Drawing.Size(116, 23);
            textBox_fuwuqi.TabIndex = 36;
            // 
            // button_shoudongOut
            // 
            this.button_shoudongOut.Location = new System.Drawing.Point(114, 187);
            this.button_shoudongOut.Name = "button_shoudongOut";
            this.button_shoudongOut.Size = new System.Drawing.Size(106, 27);
            this.button_shoudongOut.TabIndex = 38;
            this.button_shoudongOut.Text = "手动导出";
            this.button_shoudongOut.UseVisualStyleBackColor = true;
            this.button_shoudongOut.Click += new System.EventHandler(this.button_shoudongOut_Click);
            // 
            // button_DefaultFolder_Path
            // 
            this.button_DefaultFolder_Path.Location = new System.Drawing.Point(148, 100);
            this.button_DefaultFolder_Path.Name = "button_DefaultFolder_Path";
            this.button_DefaultFolder_Path.Size = new System.Drawing.Size(72, 27);
            this.button_DefaultFolder_Path.TabIndex = 39;
            this.button_DefaultFolder_Path.Text = "Browse";
            this.button_DefaultFolder_Path.UseVisualStyleBackColor = true;
            this.button_DefaultFolder_Path.Click += new System.EventHandler(this.button_DefaultFolder_Path_Click);
            // 
            // textBox_valueType
            // 
            textBox_valueType.Location = new System.Drawing.Point(1454, 622);
            textBox_valueType.Name = "textBox_valueType";
            textBox_valueType.Size = new System.Drawing.Size(116, 23);
            textBox_valueType.TabIndex = 40;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(1454, 338);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(116, 23);
            textBox1.TabIndex = 41;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(23, 404);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(91, 14);
            label3.TabIndex = 42;
            label3.Text = "采集未开始！";
            label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 350);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 14);
            this.label4.TabIndex = 43;
            this.label4.Text = "程序未启动！";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_DefaultFolder_Path);
            this.groupBox1.Controls.Add(this.label_Folder_Path);
            this.groupBox1.Controls.Add(this.label_Open);
            this.groupBox1.Controls.Add(this.button_Open);
            this.groupBox1.Controls.Add(this.button_Folder_Path);
            this.groupBox1.Controls.Add(this.button_shoudongOut);
            this.groupBox1.Controls.Add(this.button_DefaultFolder_Path);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_Time);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(15, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 245);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置栏";
            // 
            // label_DefaultFolder_Path
            // 
            this.label_DefaultFolder_Path.AutoSize = true;
            this.label_DefaultFolder_Path.Location = new System.Drawing.Point(27, 108);
            this.label_DefaultFolder_Path.Name = "label_DefaultFolder_Path";
            this.label_DefaultFolder_Path.Size = new System.Drawing.Size(119, 14);
            this.label_DefaultFolder_Path.TabIndex = 40;
            this.label_DefaultFolder_Path.Text = "设置本地备份路径";
            // 
            // label_Folder_Path
            // 
            this.label_Folder_Path.Location = new System.Drawing.Point(27, 73);
            this.label_Folder_Path.Name = "label_Folder_Path";
            this.label_Folder_Path.Size = new System.Drawing.Size(119, 14);
            this.label_Folder_Path.TabIndex = 32;
            this.label_Folder_Path.Text = "设置远程访问路径";
            // 
            // label_Open
            // 
            this.label_Open.AutoSize = true;
            this.label_Open.Location = new System.Drawing.Point(27, 38);
            this.label_Open.Name = "label_Open";
            this.label_Open.Size = new System.Drawing.Size(119, 14);
            this.label_Open.TabIndex = 1;
            this.label_Open.Text = "选择导入配置文件";
            // 
            // button_Folder_Path
            // 
            this.button_Folder_Path.Location = new System.Drawing.Point(148, 67);
            this.button_Folder_Path.Name = "button_Folder_Path";
            this.button_Folder_Path.Size = new System.Drawing.Size(72, 27);
            this.button_Folder_Path.TabIndex = 31;
            this.button_Folder_Path.Text = "Browse";
            this.button_Folder_Path.UseVisualStyleBackColor = true;
            this.button_Folder_Path.Click += new System.EventHandler(this.button_Folder_Path_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_valueType);
            this.groupBox2.Controls.Add(this.label_endTime);
            this.groupBox2.Controls.Add(this.label_startTime);
            this.groupBox2.Controls.Add(this.button_summary);
            this.groupBox2.Controls.Add(this.comboBox_valueType);
            this.groupBox2.Controls.Add(this.dateTimePicker_End);
            this.groupBox2.Controls.Add(this.dateTimePicker_Start);
            this.groupBox2.Location = new System.Drawing.Point(15, 449);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 245);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            // 
            // label_valueType
            // 
            this.label_valueType.AutoSize = true;
            this.label_valueType.Location = new System.Drawing.Point(20, 147);
            this.label_valueType.Name = "label_valueType";
            this.label_valueType.Size = new System.Drawing.Size(49, 14);
            this.label_valueType.TabIndex = 6;
            this.label_valueType.Text = "值类型";
            // 
            // label_endTime
            // 
            this.label_endTime.AutoSize = true;
            this.label_endTime.Location = new System.Drawing.Point(20, 93);
            this.label_endTime.Name = "label_endTime";
            this.label_endTime.Size = new System.Drawing.Size(63, 14);
            this.label_endTime.TabIndex = 5;
            this.label_endTime.Text = "结束时间";
            // 
            // label_startTime
            // 
            this.label_startTime.AutoSize = true;
            this.label_startTime.Location = new System.Drawing.Point(20, 31);
            this.label_startTime.Name = "label_startTime";
            this.label_startTime.Size = new System.Drawing.Size(63, 14);
            this.label_startTime.TabIndex = 4;
            this.label_startTime.Text = "开始时间";
            // 
            // button_summary
            // 
            this.button_summary.Location = new System.Drawing.Point(23, 195);
            this.button_summary.Name = "button_summary";
            this.button_summary.Size = new System.Drawing.Size(75, 23);
            this.button_summary.TabIndex = 3;
            this.button_summary.Text = "汇总导出";
            this.button_summary.UseVisualStyleBackColor = true;
            this.button_summary.Click += new System.EventHandler(this.button_summary_Click);
            // 
            // comboBox_valueType
            // 
            this.comboBox_valueType.FormattingEnabled = true;
            this.comboBox_valueType.Items.AddRange(new object[] {
            "",
            "0",
            "1"});
            this.comboBox_valueType.Location = new System.Drawing.Point(89, 139);
            this.comboBox_valueType.Name = "comboBox_valueType";
            this.comboBox_valueType.Size = new System.Drawing.Size(80, 22);
            this.comboBox_valueType.TabIndex = 2;
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimePicker_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_End.Location = new System.Drawing.Point(89, 87);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(167, 23);
            this.dateTimePicker_End.TabIndex = 1;
            // 
            // dateTimePicker_Start
            // 
            this.dateTimePicker_Start.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimePicker_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_Start.Location = new System.Drawing.Point(89, 25);
            this.dateTimePicker_Start.Name = "dateTimePicker_Start";
            this.dateTimePicker_Start.Size = new System.Drawing.Size(169, 23);
            this.dateTimePicker_Start.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(textBox1);
            this.Controls.Add(textBox_valueType);
            this.Controls.Add(textBox_fuwuqi);
            this.Controls.Add(textBox_GetNull);
            this.Controls.Add(this.textBox_Value);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(textBox_ID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button_All);
            this.Controls.Add(textBox_Date);
            this.Controls.Add(textBox_serialnumber);
            this.Controls.Add(textBox_type);
            this.Controls.Add(textBox_localID);
            this.Controls.Add(textBox_servername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(textBox1_LocalID);
            this.Controls.Add(textBox_GetValue);
            this.Controls.Add(this.button_Air);
            this.Controls.Add(this.button_Water);
            this.Controls.Add(this.button_Power);
            this.Controls.Add(this.button_Gas);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "基于MES的可视化车间数据自动采集系统V1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Open;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Gas;
        private System.Windows.Forms.Button button_Power;
        private System.Windows.Forms.Button button_Water;
        private System.Windows.Forms.Button button_Air;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_All;
        private System.Windows.Forms.TextBox textBox_Time;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.TextBox textBox_Value;
        private System.Windows.Forms.Button button_shoudongOut;
        private System.Windows.Forms.Button button_DefaultFolder_Path;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_valueType;
        private System.Windows.Forms.Label label_endTime;
        private System.Windows.Forms.Label label_startTime;
        private System.Windows.Forms.Button button_summary;
        private System.Windows.Forms.ComboBox comboBox_valueType;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Start;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_DefaultFolder_Path;
        private System.Windows.Forms.Label label_Folder_Path;
        private System.Windows.Forms.Label label_Open;
        private System.Windows.Forms.Button button_Folder_Path;
        private static System.Windows.Forms.Label label3;
        private static System.Windows.Forms.TextBox textBox1;
        private static System.Windows.Forms.TextBox textBox_GetValue;
        private static System.Windows.Forms.TextBox textBox1_LocalID;
        private static System.Windows.Forms.TextBox textBox_servername;
        private static System.Windows.Forms.TextBox textBox_localID;
        private static System.Windows.Forms.TextBox textBox_type;
        private static System.Windows.Forms.TextBox textBox_serialnumber;
        private static System.Windows.Forms.TextBox textBox_Date;
        private static System.Windows.Forms.TextBox textBox_ID;
        private static System.Windows.Forms.TextBox textBox_GetNull;
        private static System.Windows.Forms.TextBox textBox_fuwuqi;
        private static  System.Windows.Forms.TextBox textBox_valueType;
    }
}

