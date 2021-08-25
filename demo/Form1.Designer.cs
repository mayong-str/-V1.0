namespace demo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel_login = new System.Windows.Forms.Panel();
            this.button_login = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label_username = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.label_title = new System.Windows.Forms.Label();
            this.panel_login.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_login
            // 
            this.panel_login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel_login.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_login.BackgroundImage")));
            this.panel_login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_login.Controls.Add(this.button_login);
            this.panel_login.Controls.Add(this.button_clear);
            this.panel_login.Controls.Add(this.textBox_password);
            this.panel_login.Controls.Add(this.label_username);
            this.panel_login.Controls.Add(this.label_password);
            this.panel_login.Controls.Add(this.textBox_username);
            this.panel_login.Controls.Add(this.label_title);
            this.panel_login.Location = new System.Drawing.Point(174, 89);
            this.panel_login.Name = "panel_login";
            this.panel_login.Size = new System.Drawing.Size(580, 311);
            this.panel_login.TabIndex = 0;
            // 
            // button_login
            // 
            this.button_login.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_login.BackgroundImage")));
            this.button_login.Location = new System.Drawing.Point(211, 194);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(75, 32);
            this.button_login.TabIndex = 8;
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // button_clear
            // 
            this.button_clear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_clear.BackgroundImage")));
            this.button_clear.Location = new System.Drawing.Point(316, 194);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(75, 32);
            this.button_clear.TabIndex = 7;
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(211, 154);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(180, 21);
            this.textBox_password.TabIndex = 5;
            this.textBox_password.TextChanged += new System.EventHandler(this.textBox_password_TextChanged);
            // 
            // label_username
            // 
            this.label_username.BackColor = System.Drawing.Color.Transparent;
            this.label_username.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_username.Location = new System.Drawing.Point(122, 102);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(83, 23);
            this.label_username.TabIndex = 4;
            this.label_username.Text = "用户名：";
            // 
            // label_password
            // 
            this.label_password.BackColor = System.Drawing.Color.Transparent;
            this.label_password.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_password.Location = new System.Drawing.Point(122, 153);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(82, 23);
            this.label_password.TabIndex = 3;
            this.label_password.Text = "密  码：";
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(211, 103);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(180, 21);
            this.textBox_username.TabIndex = 2;
            // 
            // label_title
            // 
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_title.ForeColor = System.Drawing.Color.Teal;
            this.label_title.Location = new System.Drawing.Point(77, 41);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(438, 32);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "全自动码垛机器人包装生产线管理系统";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(957, 526);
            this.Controls.Add(this.panel_login);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel_login.ResumeLayout(false);
            this.panel_login.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_login;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.Label label_password;
    }
}

