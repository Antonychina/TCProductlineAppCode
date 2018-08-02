namespace Mubea.AutoTest.GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitBack = new System.Windows.Forms.SplitContainer();
            this.pb_Pause = new System.Windows.Forms.PictureBox();
            this.lb_Pause = new System.Windows.Forms.Label();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.label_LoginName = new System.Windows.Forms.Label();
            this.picture_prodline = new System.Windows.Forms.PictureBox();
            this.titleBar = new Mubea.GUI.CustomControl.TitleBtnCtrl();
            this.label_ProductionLine = new System.Windows.Forms.Label();
            this.pcbUser = new System.Windows.Forms.PictureBox();
            this.sidebar = new Mubea.GUI.CustomControl.Sidebar();
            ((System.ComponentModel.ISupportInitialize)(this.splitBack)).BeginInit();
            this.splitBack.Panel1.SuspendLayout();
            this.splitBack.Panel2.SuspendLayout();
            this.splitBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Pause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_prodline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUser)).BeginInit();
            this.SuspendLayout();
            // 
            // splitBack
            // 
            this.splitBack.Location = new System.Drawing.Point(0, 0);
            this.splitBack.Name = "splitBack";
            this.splitBack.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitBack.Panel1
            // 
            this.splitBack.Panel1.BackColor = System.Drawing.Color.White;
            this.splitBack.Panel1.Controls.Add(this.pb_Pause);
            this.splitBack.Panel1.Controls.Add(this.lb_Pause);
            this.splitBack.Panel1.Controls.Add(this.panelLogo);
            this.splitBack.Panel1.Controls.Add(this.label_LoginName);
            this.splitBack.Panel1.Controls.Add(this.picture_prodline);
            this.splitBack.Panel1.Controls.Add(this.titleBar);
            this.splitBack.Panel1.Controls.Add(this.label_ProductionLine);
            this.splitBack.Panel1.Controls.Add(this.pcbUser);
            this.splitBack.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitBack.Panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            // 
            // splitBack.Panel2
            // 
            this.splitBack.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitBack.Panel2.Controls.Add(this.sidebar);
            this.splitBack.Size = new System.Drawing.Size(1024, 768);
            this.splitBack.SplitterDistance = 53;
            this.splitBack.SplitterWidth = 1;
            this.splitBack.TabIndex = 0;
            // 
            // pb_Pause
            // 
            this.pb_Pause.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pb_Pause.ErrorImage = null;
            this.pb_Pause.Image = global::Mubea.AutoTest.GUI.Properties.Resources.Start;
            this.pb_Pause.InitialImage = null;
            this.pb_Pause.Location = new System.Drawing.Point(261, 12);
            this.pb_Pause.Name = "pb_Pause";
            this.pb_Pause.Size = new System.Drawing.Size(47, 41);
            this.pb_Pause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Pause.TabIndex = 8;
            this.pb_Pause.TabStop = false;
            this.pb_Pause.Click += new System.EventHandler(this.pb_Pause_Click);
            // 
            // lb_Pause
            // 
            this.lb_Pause.AutoSize = true;
            this.lb_Pause.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Pause.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lb_Pause.Location = new System.Drawing.Point(306, 21);
            this.lb_Pause.Name = "lb_Pause";
            this.lb_Pause.Size = new System.Drawing.Size(48, 24);
            this.lb_Pause.TabIndex = 7;
            this.lb_Pause.Text = "运行";
            this.lb_Pause.Click += new System.EventHandler(this.pb_Pause_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panelLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelLogo.BackgroundImage")));
            this.panelLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(192, 53);
            this.panelLogo.TabIndex = 0;
            // 
            // label_LoginName
            // 
            this.label_LoginName.AutoSize = true;
            this.label_LoginName.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_LoginName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label_LoginName.Location = new System.Drawing.Point(654, 21);
            this.label_LoginName.Name = "label_LoginName";
            this.label_LoginName.Size = new System.Drawing.Size(68, 24);
            this.label_LoginName.TabIndex = 3;
            this.label_LoginName.Text = "admin";
            // 
            // picture_prodline
            // 
            this.picture_prodline.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.picture_prodline.ErrorImage = null;
            this.picture_prodline.Image = global::Mubea.AutoTest.GUI.Properties.Resources.productionline;
            this.picture_prodline.InitialImage = null;
            this.picture_prodline.Location = new System.Drawing.Point(444, 15);
            this.picture_prodline.Name = "picture_prodline";
            this.picture_prodline.Size = new System.Drawing.Size(42, 38);
            this.picture_prodline.TabIndex = 6;
            this.picture_prodline.TabStop = false;
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.White;
            this.titleBar.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBar.ForeColor = System.Drawing.Color.White;
            this.titleBar.Location = new System.Drawing.Point(792, 1);
            this.titleBar.Margin = new System.Windows.Forms.Padding(6, 9, 6, 9);
            this.titleBar.MultiSelect = false;
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(228, 58);
            this.titleBar.TabIndex = 0;
            // 
            // label_ProductionLine
            // 
            this.label_ProductionLine.AutoSize = true;
            this.label_ProductionLine.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ProductionLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label_ProductionLine.Location = new System.Drawing.Point(492, 21);
            this.label_ProductionLine.Name = "label_ProductionLine";
            this.label_ProductionLine.Size = new System.Drawing.Size(78, 24);
            this.label_ProductionLine.TabIndex = 5;
            this.label_ProductionLine.Text = "1号产线";
            // 
            // pcbUser
            // 
            this.pcbUser.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pcbUser.ErrorImage = null;
            this.pcbUser.Image = global::Mubea.AutoTest.GUI.Properties.Resources.user2;
            this.pcbUser.InitialImage = null;
            this.pcbUser.Location = new System.Drawing.Point(613, 15);
            this.pcbUser.Name = "pcbUser";
            this.pcbUser.Size = new System.Drawing.Size(35, 38);
            this.pcbUser.TabIndex = 1;
            this.pcbUser.TabStop = false;
            this.pcbUser.Click += new System.EventHandler(this.pcbUser_Click);
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.sidebar.EnableDisplayTime = false;
            this.sidebar.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sidebar.Location = new System.Drawing.Point(0, -1);
            this.sidebar.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.sidebar.MultiSelect = false;
            this.sidebar.Name = "sidebar";
            this.sidebar.SelectedColor = System.Drawing.Color.Empty;
            this.sidebar.Size = new System.Drawing.Size(192, 715);
            this.sidebar.TabIndex = 0;
            this.sidebar.TimeFont = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sidebar.TimeFormat = "yyyy/MM/dd HH:mm";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.splitBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Muber BT 生产系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitBack.Panel1.ResumeLayout(false);
            this.splitBack.Panel1.PerformLayout();
            this.splitBack.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitBack)).EndInit();
            this.splitBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Pause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_prodline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitBack;
        private Mubea.GUI.CustomControl.Sidebar sidebar;
        private System.Windows.Forms.Label label_LoginName;
        private Mubea.GUI.CustomControl.TitleBtnCtrl titleBar;
		private System.Windows.Forms.PictureBox pcbUser;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label label_ProductionLine;
        private System.Windows.Forms.PictureBox picture_prodline;
        private System.Windows.Forms.PictureBox pb_Pause;
        private System.Windows.Forms.Label lb_Pause;
        //private System.Windows.Forms.Timer timer_handshake;
        
    }
}

