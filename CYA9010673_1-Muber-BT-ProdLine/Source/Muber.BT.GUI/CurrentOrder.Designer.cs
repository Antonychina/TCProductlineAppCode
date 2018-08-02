namespace Mubea.AutoTest.GUI
{
	partial class CurrentOrder
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrentOrder));
            this.colSex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIsEmergency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRemark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btn_SwitchLine = new System.Windows.Forms.Button();
            this.btnDownloadTags = new System.Windows.Forms.Button();
            this.btnOrderOK = new System.Windows.Forms.Button();
            this.textOrderCount = new System.Windows.Forms.TextBox();
            this.textProdNo = new System.Windows.Forms.TextBox();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listView_CurrentOrder = new System.Windows.Forms.ListView();
            this.SerialNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MaterialNO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MaterialName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // colSex
            // 
            this.colSex.Text = "性别";
            this.colSex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colSex.Width = 160;
            // 
            // colAge
            // 
            this.colAge.Text = "年龄";
            this.colAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colAge.Width = 200;
            // 
            // colIsEmergency
            // 
            this.colIsEmergency.Text = "是否急诊";
            this.colIsEmergency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colIsEmergency.Width = 160;
            // 
            // colRemark
            // 
            this.colRemark.Text = "备注";
            this.colRemark.Width = 300;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btn_SwitchLine
            // 
            this.btn_SwitchLine.BackColor = System.Drawing.Color.Transparent;
            this.btn_SwitchLine.BackgroundImage = global::Mubea.AutoTest.GUI.Properties.Resources._1切换订单;
            this.btn_SwitchLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_SwitchLine.FlatAppearance.BorderSize = 0;
            this.btn_SwitchLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SwitchLine.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SwitchLine.Location = new System.Drawing.Point(634, 536);
            this.btn_SwitchLine.Name = "btn_SwitchLine";
            this.btn_SwitchLine.Size = new System.Drawing.Size(96, 54);
            this.btn_SwitchLine.TabIndex = 18;
            this.btn_SwitchLine.Text = "  ";
            this.btn_SwitchLine.UseVisualStyleBackColor = false;
            this.btn_SwitchLine.Click += new System.EventHandler(this.btn_SwitchLine_Click);
            // 
            // btnDownloadTags
            // 
            this.btnDownloadTags.BackColor = System.Drawing.Color.Transparent;
            this.btnDownloadTags.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDownloadTags.BackgroundImage")));
            this.btnDownloadTags.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDownloadTags.FlatAppearance.BorderSize = 0;
            this.btnDownloadTags.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadTags.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDownloadTags.Location = new System.Drawing.Point(640, 476);
            this.btnDownloadTags.Name = "btnDownloadTags";
            this.btnDownloadTags.Size = new System.Drawing.Size(88, 54);
            this.btnDownloadTags.TabIndex = 17;
            this.btnDownloadTags.Text = "  ";
            this.btnDownloadTags.UseVisualStyleBackColor = false;
            this.btnDownloadTags.Click += new System.EventHandler(this.btnDownloadTags_Click);
            // 
            // btnOrderOK
            // 
            this.btnOrderOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOrderOK.BackgroundImage = global::Mubea.AutoTest.GUI.Properties.Resources._1确定;
            this.btnOrderOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOrderOK.FlatAppearance.BorderSize = 0;
            this.btnOrderOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrderOK.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOrderOK.Location = new System.Drawing.Point(634, 416);
            this.btnOrderOK.Name = "btnOrderOK";
            this.btnOrderOK.Size = new System.Drawing.Size(96, 54);
            this.btnOrderOK.TabIndex = 16;
            this.btnOrderOK.UseVisualStyleBackColor = false;
            this.btnOrderOK.Click += new System.EventHandler(this.btnOrderOK_Click);
            // 
            // textOrderCount
            // 
            this.textOrderCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textOrderCount.ForeColor = System.Drawing.Color.Black;
            this.textOrderCount.Location = new System.Drawing.Point(627, 17);
            this.textOrderCount.Name = "textOrderCount";
            this.textOrderCount.Size = new System.Drawing.Size(86, 29);
            this.textOrderCount.TabIndex = 10;
            // 
            // textProdNo
            // 
            this.textProdNo.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textProdNo.ForeColor = System.Drawing.Color.Black;
            this.textProdNo.Location = new System.Drawing.Point(363, 17);
            this.textProdNo.Name = "textProdNo";
            this.textProdNo.Size = new System.Drawing.Size(120, 29);
            this.textProdNo.TabIndex = 9;
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNo.ForeColor = System.Drawing.Color.Black;
            this.txtOrderNo.Location = new System.Drawing.Point(122, 17);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(115, 29);
            this.txtOrderNo.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(536, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "订单数量：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(284, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "产品号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(46, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "订单号 :";
            // 
            // listView_CurrentOrder
            // 
            this.listView_CurrentOrder.AutoArrange = false;
            this.listView_CurrentOrder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.listView_CurrentOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SerialNumber,
            this.MaterialNO,
            this.MaterialName,
            this.Status});
            this.listView_CurrentOrder.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(120)));
            this.listView_CurrentOrder.Location = new System.Drawing.Point(73, 82);
            this.listView_CurrentOrder.Name = "listView_CurrentOrder";
            this.listView_CurrentOrder.Size = new System.Drawing.Size(521, 518);
            this.listView_CurrentOrder.SmallImageList = this.imageList1;
            this.listView_CurrentOrder.TabIndex = 2;
            this.listView_CurrentOrder.UseCompatibleStateImageBehavior = false;
            this.listView_CurrentOrder.View = System.Windows.Forms.View.Details;
            // 
            // SerialNumber
            // 
            this.SerialNumber.Text = "序号";
            // 
            // MaterialNO
            // 
            this.MaterialNO.Text = "零件号";
            this.MaterialNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaterialNO.Width = 143;
            // 
            // MaterialName
            // 
            this.MaterialName.Text = "零件名称";
            this.MaterialName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaterialName.Width = 198;
            // 
            // Status
            // 
            this.Status.Text = "位置编号";
            this.Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Status.Width = 115;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(838, 60);
            this.label5.TabIndex = 15;
            // 
            // CurrentOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.btn_SwitchLine);
            this.Controls.Add(this.btnDownloadTags);
            this.Controls.Add(this.btnOrderOK);
            this.Controls.Add(this.textOrderCount);
            this.Controls.Add(this.textProdNo);
            this.Controls.Add(this.txtOrderNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView_CurrentOrder);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "CurrentOrder";
            this.Text = "当前订单";
            this.Load += new System.EventHandler(this.HomeOverView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ListView listView_CurrentOrder;
        private System.Windows.Forms.ColumnHeader MaterialNO;
		private System.Windows.Forms.ColumnHeader Status;
		private System.Windows.Forms.ColumnHeader colSex;
		private System.Windows.Forms.ColumnHeader colAge;
		private System.Windows.Forms.ColumnHeader MaterialName;
        private System.Windows.Forms.ColumnHeader colIsEmergency;
		private System.Windows.Forms.ColumnHeader colRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader SerialNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.TextBox textProdNo;
        private System.Windows.Forms.TextBox textOrderCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOrderOK;
        private System.Windows.Forms.Button btnDownloadTags;
        private System.Windows.Forms.Button btn_SwitchLine;

	}
}