namespace TWM_3D_Integration
{
    partial class frmSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tab1 = new System.Windows.Forms.TabControl();
            this.CmpPage = new System.Windows.Forms.TabPage();
            this.grpTrusted = new System.Windows.Forms.GroupBox();
            this.txtDBPassword = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtDBUser = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.chbTrusted = new System.Windows.Forms.CheckBox();
            this.cboDbType = new System.Windows.Forms.ComboBox();
            this.Label29 = new System.Windows.Forms.Label();
            this.btnTestCon = new System.Windows.Forms.Button();
            this.txtLcnsPort = new System.Windows.Forms.TextBox();
            this.Label27 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.Label25 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.txtLcnsSrvr = new System.Windows.Forms.TextBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.txtSrvrName = new System.Windows.Forms.TextBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.Label24 = new System.Windows.Forms.Label();
            this.MenuStrip1.SuspendLayout();
            this.Tab1.SuspendLayout();
            this.CmpPage.SuspendLayout();
            this.grpTrusted.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.AutoSize = false;
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(505, 20);
            this.MenuStrip1.TabIndex = 2;
            this.MenuStrip1.Text = "MenuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToolStripMenuItem,
            this.ToolStripMenuItem1,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 16);
            this.FileToolStripMenuItem.Text = "&File";
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.SaveToolStripMenuItem.Text = "&Save";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(95, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.ExitToolStripMenuItem.Text = "E&xit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // Tab1
            // 
            this.Tab1.Controls.Add(this.CmpPage);
            this.Tab1.Location = new System.Drawing.Point(2, 30);
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 0;
            this.Tab1.Size = new System.Drawing.Size(513, 406);
            this.Tab1.TabIndex = 3;
            // 
            // CmpPage
            // 
            this.CmpPage.Controls.Add(this.grpTrusted);
            this.CmpPage.Controls.Add(this.chbTrusted);
            this.CmpPage.Controls.Add(this.cboDbType);
            this.CmpPage.Controls.Add(this.Label29);
            this.CmpPage.Controls.Add(this.btnTestCon);
            this.CmpPage.Controls.Add(this.txtLcnsPort);
            this.CmpPage.Controls.Add(this.Label27);
            this.CmpPage.Controls.Add(this.txtPassword);
            this.CmpPage.Controls.Add(this.Label25);
            this.CmpPage.Controls.Add(this.txtUserName);
            this.CmpPage.Controls.Add(this.Label26);
            this.CmpPage.Controls.Add(this.Label21);
            this.CmpPage.Controls.Add(this.txtLcnsSrvr);
            this.CmpPage.Controls.Add(this.Label22);
            this.CmpPage.Controls.Add(this.txtSrvrName);
            this.CmpPage.Controls.Add(this.Label23);
            this.CmpPage.Controls.Add(this.txtDBName);
            this.CmpPage.Controls.Add(this.Label24);
            this.CmpPage.Location = new System.Drawing.Point(4, 22);
            this.CmpPage.Name = "CmpPage";
            this.CmpPage.Padding = new System.Windows.Forms.Padding(3);
            this.CmpPage.Size = new System.Drawing.Size(505, 380);
            this.CmpPage.TabIndex = 2;
            this.CmpPage.Text = "Company";
            this.CmpPage.UseVisualStyleBackColor = true;
            // 
            // grpTrusted
            // 
            this.grpTrusted.Controls.Add(this.txtDBPassword);
            this.grpTrusted.Controls.Add(this.Label2);
            this.grpTrusted.Controls.Add(this.txtDBUser);
            this.grpTrusted.Controls.Add(this.Label1);
            this.grpTrusted.Enabled = false;
            this.grpTrusted.Location = new System.Drawing.Point(38, 209);
            this.grpTrusted.Name = "grpTrusted";
            this.grpTrusted.Size = new System.Drawing.Size(428, 100);
            this.grpTrusted.TabIndex = 39;
            this.grpTrusted.TabStop = false;
            this.grpTrusted.Text = "Use Trusted";
            // 
            // txtDBPassword
            // 
            this.txtDBPassword.Location = new System.Drawing.Point(99, 45);
            this.txtDBPassword.Name = "txtDBPassword";
            this.txtDBPassword.PasswordChar = '*';
            this.txtDBPassword.Size = new System.Drawing.Size(323, 20);
            this.txtDBPassword.TabIndex = 42;
            this.txtDBPassword.TextChanged += new System.EventHandler(this.txtDBPassword_TextChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(9, 48);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(71, 13);
            this.Label2.TabIndex = 41;
            this.Label2.Text = "DB Password";
            // 
            // txtDBUser
            // 
            this.txtDBUser.Location = new System.Drawing.Point(99, 23);
            this.txtDBUser.Name = "txtDBUser";
            this.txtDBUser.Size = new System.Drawing.Size(323, 20);
            this.txtDBUser.TabIndex = 40;
            this.txtDBUser.TextChanged += new System.EventHandler(this.txtDBUser_TextChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 26);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 39;
            this.Label1.Text = "DB UserName";
            // 
            // chbTrusted
            // 
            this.chbTrusted.AutoSize = true;
            this.chbTrusted.Checked = true;
            this.chbTrusted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbTrusted.Location = new System.Drawing.Point(38, 189);
            this.chbTrusted.Name = "chbTrusted";
            this.chbTrusted.Size = new System.Drawing.Size(84, 17);
            this.chbTrusted.TabIndex = 36;
            this.chbTrusted.Text = "Use Trusted";
            this.chbTrusted.UseVisualStyleBackColor = true;
            this.chbTrusted.CheckedChanged += new System.EventHandler(this.chbTrusted_CheckedChanged);
            // 
            // cboDbType
            // 
            this.cboDbType.FormattingEnabled = true;
            this.cboDbType.Location = new System.Drawing.Point(125, 58);
            this.cboDbType.Name = "cboDbType";
            this.cboDbType.Size = new System.Drawing.Size(341, 21);
            this.cboDbType.TabIndex = 35;
            this.cboDbType.SelectedIndexChanged += new System.EventHandler(this.cboDbType_SelectedIndexChanged);
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.Location = new System.Drawing.Point(35, 61);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(65, 13);
            this.Label29.TabIndex = 34;
            this.Label29.Text = "Server Type";
            // 
            // btnTestCon
            // 
            this.btnTestCon.Location = new System.Drawing.Point(369, 331);
            this.btnTestCon.Name = "btnTestCon";
            this.btnTestCon.Size = new System.Drawing.Size(97, 23);
            this.btnTestCon.TabIndex = 21;
            this.btnTestCon.Text = "Test Connection";
            this.btnTestCon.UseVisualStyleBackColor = true;
            this.btnTestCon.Click += new System.EventHandler(this.btnTestCon_Click);
            // 
            // txtLcnsPort
            // 
            this.txtLcnsPort.Location = new System.Drawing.Point(398, 122);
            this.txtLcnsPort.Name = "txtLcnsPort";
            this.txtLcnsPort.Size = new System.Drawing.Size(68, 20);
            this.txtLcnsPort.TabIndex = 18;
            this.txtLcnsPort.TextChanged += new System.EventHandler(this.txtLcnsPort_TextChanged);
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Location = new System.Drawing.Point(366, 125);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(26, 13);
            this.Label27.TabIndex = 33;
            this.Label27.Text = "Port";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(125, 164);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(341, 20);
            this.txtPassword.TabIndex = 20;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(35, 167);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(53, 13);
            this.Label25.TabIndex = 31;
            this.Label25.Text = "Password";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(125, 143);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(341, 20);
            this.txtUserName.TabIndex = 19;
            this.txtUserName.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.Location = new System.Drawing.Point(35, 146);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(60, 13);
            this.Label26.TabIndex = 29;
            this.Label26.Text = "User Name";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label21.Location = new System.Drawing.Point(20, 36);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(105, 16);
            this.Label21.TabIndex = 28;
            this.Label21.Text = "Connections  :";
            // 
            // txtLcnsSrvr
            // 
            this.txtLcnsSrvr.Location = new System.Drawing.Point(125, 122);
            this.txtLcnsSrvr.Name = "txtLcnsSrvr";
            this.txtLcnsSrvr.Size = new System.Drawing.Size(220, 20);
            this.txtLcnsSrvr.TabIndex = 17;
            this.txtLcnsSrvr.TextChanged += new System.EventHandler(this.txtLcnsSrvr_TextChanged);
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(35, 125);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(78, 13);
            this.Label22.TabIndex = 26;
            this.Label22.Text = "License Server";
            // 
            // txtSrvrName
            // 
            this.txtSrvrName.Location = new System.Drawing.Point(125, 101);
            this.txtSrvrName.Name = "txtSrvrName";
            this.txtSrvrName.Size = new System.Drawing.Size(341, 20);
            this.txtSrvrName.TabIndex = 16;
            this.txtSrvrName.TextChanged += new System.EventHandler(this.txtSrvrName_TextChanged);
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Location = new System.Drawing.Point(35, 104);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(69, 13);
            this.Label23.TabIndex = 24;
            this.Label23.Text = "Server Name";
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(125, 80);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(341, 20);
            this.txtDBName.TabIndex = 15;
            this.txtDBName.TextChanged += new System.EventHandler(this.txtDBName_TextChanged);
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(35, 83);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(84, 13);
            this.Label24.TabIndex = 22;
            this.Label24.Text = "Database Name";
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 427);
            this.Controls.Add(this.Tab1);
            this.Controls.Add(this.MenuStrip1);
            this.Name = "frmSetting";
            this.Text = "B2B No UI Setup";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.Tab1.ResumeLayout(false);
            this.CmpPage.ResumeLayout(false);
            this.CmpPage.PerformLayout();
            this.grpTrusted.ResumeLayout(false);
            this.grpTrusted.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.MenuStrip MenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripMenuItem1;
        internal System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        internal System.Windows.Forms.TabControl Tab1;
        internal System.Windows.Forms.TabPage CmpPage;
        internal System.Windows.Forms.GroupBox grpTrusted;
        internal System.Windows.Forms.TextBox txtDBPassword;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtDBUser;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.CheckBox chbTrusted;
        internal System.Windows.Forms.ComboBox cboDbType;
        internal System.Windows.Forms.Label Label29;
        internal System.Windows.Forms.Button btnTestCon;
        internal System.Windows.Forms.TextBox txtLcnsPort;
        internal System.Windows.Forms.Label Label27;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Label Label25;
        internal System.Windows.Forms.TextBox txtUserName;
        internal System.Windows.Forms.Label Label26;
        internal System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.TextBox txtLcnsSrvr;
        internal System.Windows.Forms.Label Label22;
        internal System.Windows.Forms.TextBox txtSrvrName;
        internal System.Windows.Forms.Label Label23;
        internal System.Windows.Forms.TextBox txtDBName;
        internal System.Windows.Forms.Label Label24;
    }
}