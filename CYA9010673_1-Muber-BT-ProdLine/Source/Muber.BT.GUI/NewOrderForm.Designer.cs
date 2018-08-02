namespace Mubea.AutoTest.GUI
{
    partial class NewOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewOrder));
            this.colSex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIsEmergency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRemark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.comboBox_Prepare = new System.Windows.Forms.ComboBox();
            this.label_Prepare = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnNewOrderOK = new System.Windows.Forms.Button();
            this.textNewOrderCount = new System.Windows.Forms.TextBox();
            this.textProdNo = new System.Windows.Forms.TextBox();
            this.txtNewOrderNo = new System.Windows.Forms.TextBox();
            this.label_NewOrderCount = new System.Windows.Forms.Label();
            this.label_NewProductNo = new System.Windows.Forms.Label();
            this.label_NewOrderNo = new System.Windows.Forms.Label();
            this.listView_NewOrder = new System.Windows.Forms.ListView();
            this.SerialNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MaterialNO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MaterialName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            // comboBox_Prepare
            // 
            this.comboBox_Prepare.BackColor = System.Drawing.Color.White;
            this.comboBox_Prepare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Prepare.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_Prepare.FormattingEnabled = true;
            this.comboBox_Prepare.Items.AddRange(new object[] {
            "是",
            "否"});
            this.comboBox_Prepare.Location = new System.Drawing.Point(684, 16);
            this.comboBox_Prepare.Name = "comboBox_Prepare";
            this.comboBox_Prepare.Size = new System.Drawing.Size(55, 30);
            this.comboBox_Prepare.TabIndex = 20;
            // 
            // label_Prepare
            // 
            this.label_Prepare.AutoSize = true;
            this.label_Prepare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label_Prepare.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Prepare.ForeColor = System.Drawing.Color.White;
            this.label_Prepare.Location = new System.Drawing.Point(601, 21);
            this.label_Prepare.Name = "label_Prepare";
            this.label_Prepare.Size = new System.Drawing.Size(82, 22);
            this.label_Prepare.TabIndex = 19;
            this.label_Prepare.Text = "备料请求:";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(623, 543);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(86, 37);
            this.btnClear.TabIndex = 18;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnNewOrderOK
            // 
            this.btnNewOrderOK.BackColor = System.Drawing.Color.Transparent;
            this.btnNewOrderOK.BackgroundImage = global::Mubea.AutoTest.GUI.Properties.Resources._1确定;
            this.btnNewOrderOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNewOrderOK.FlatAppearance.BorderSize = 0;
            this.btnNewOrderOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewOrderOK.Location = new System.Drawing.Point(619, 476);
            this.btnNewOrderOK.Name = "btnNewOrderOK";
            this.btnNewOrderOK.Size = new System.Drawing.Size(95, 54);
            this.btnNewOrderOK.TabIndex = 17;
            this.btnNewOrderOK.UseVisualStyleBackColor = false;
            this.btnNewOrderOK.Click += new System.EventHandler(this.btnNewOrderOK_Click);
            // 
            // textNewOrderCount
            // 
            this.textNewOrderCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNewOrderCount.ForeColor = System.Drawing.Color.Black;
            this.textNewOrderCount.Location = new System.Drawing.Point(496, 17);
            this.textNewOrderCount.Name = "textNewOrderCount";
            this.textNewOrderCount.Size = new System.Drawing.Size(80, 28);
            this.textNewOrderCount.TabIndex = 10;
            // 
            // textProdNo
            // 
            this.textProdNo.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textProdNo.ForeColor = System.Drawing.Color.Black;
            this.textProdNo.Location = new System.Drawing.Point(278, 17);
            this.textProdNo.Name = "textProdNo";
            this.textProdNo.Size = new System.Drawing.Size(110, 28);
            this.textProdNo.TabIndex = 9;
            // 
            // txtNewOrderNo
            // 
            this.txtNewOrderNo.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewOrderNo.ForeColor = System.Drawing.Color.Black;
            this.txtNewOrderNo.Location = new System.Drawing.Point(81, 17);
            this.txtNewOrderNo.Name = "txtNewOrderNo";
            this.txtNewOrderNo.Size = new System.Drawing.Size(110, 28);
            this.txtNewOrderNo.TabIndex = 0;
            // 
            // label_NewOrderCount
            // 
            this.label_NewOrderCount.AutoSize = true;
            this.label_NewOrderCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label_NewOrderCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_NewOrderCount.ForeColor = System.Drawing.Color.White;
            this.label_NewOrderCount.Location = new System.Drawing.Point(413, 21);
            this.label_NewOrderCount.Name = "label_NewOrderCount";
            this.label_NewOrderCount.Size = new System.Drawing.Size(82, 22);
            this.label_NewOrderCount.TabIndex = 5;
            this.label_NewOrderCount.Text = "订单数量:";
            // 
            // label_NewProductNo
            // 
            this.label_NewProductNo.AutoSize = true;
            this.label_NewProductNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label_NewProductNo.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_NewProductNo.ForeColor = System.Drawing.Color.White;
            this.label_NewProductNo.Location = new System.Drawing.Point(211, 20);
            this.label_NewProductNo.Name = "label_NewProductNo";
            this.label_NewProductNo.Size = new System.Drawing.Size(65, 22);
            this.label_NewProductNo.TabIndex = 4;
            this.label_NewProductNo.Text = "产品号:";
            // 
            // label_NewOrderNo
            // 
            this.label_NewOrderNo.AutoSize = true;
            this.label_NewOrderNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label_NewOrderNo.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_NewOrderNo.ForeColor = System.Drawing.Color.White;
            this.label_NewOrderNo.Location = new System.Drawing.Point(14, 20);
            this.label_NewOrderNo.Name = "label_NewOrderNo";
            this.label_NewOrderNo.Size = new System.Drawing.Size(65, 22);
            this.label_NewOrderNo.TabIndex = 3;
            this.label_NewOrderNo.Text = "订单号:";
            // 
            // listView_NewOrder
            // 
            this.listView_NewOrder.AutoArrange = false;
            this.listView_NewOrder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.listView_NewOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SerialNumber,
            this.MaterialNO,
            this.MaterialName,
            this.Count});
            this.listView_NewOrder.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(120)));
            this.listView_NewOrder.Location = new System.Drawing.Point(59, 82);
            this.listView_NewOrder.Name = "listView_NewOrder";
            this.listView_NewOrder.Size = new System.Drawing.Size(521, 518);
            this.listView_NewOrder.SmallImageList = this.imageList1;
            this.listView_NewOrder.TabIndex = 2;
            this.listView_NewOrder.UseCompatibleStateImageBehavior = false;
            this.listView_NewOrder.View = System.Windows.Forms.View.Details;
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
            // Count
            // 
            this.Count.Text = "所需数量";
            this.Count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Count.Width = 115;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(831, 60);
            this.label5.TabIndex = 16;
            // 
            // NewOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(832, 716);
            this.Controls.Add(this.comboBox_Prepare);
            this.Controls.Add(this.label_Prepare);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnNewOrderOK);
            this.Controls.Add(this.textNewOrderCount);
            this.Controls.Add(this.textProdNo);
            this.Controls.Add(this.txtNewOrderNo);
            this.Controls.Add(this.label_NewOrderCount);
            this.Controls.Add(this.label_NewProductNo);
            this.Controls.Add(this.label_NewOrderNo);
            this.Controls.Add(this.listView_NewOrder);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewOrder";
            this.Text = "新订单";
            this.TransparencyKey = System.Drawing.Color.Navy;
            this.Load += new System.EventHandler(this.HomeOverView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ListView listView_NewOrder;
        private System.Windows.Forms.ColumnHeader MaterialNO;
		private System.Windows.Forms.ColumnHeader Count;
		private System.Windows.Forms.ColumnHeader colSex;
		private System.Windows.Forms.ColumnHeader colAge;
		private System.Windows.Forms.ColumnHeader MaterialName;
        private System.Windows.Forms.ColumnHeader colIsEmergency;
		private System.Windows.Forms.ColumnHeader colRemark;
        private System.Windows.Forms.Label label_NewOrderNo;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader SerialNumber;
        private System.Windows.Forms.Label label_NewOrderCount;
        private System.Windows.Forms.Label label_NewProductNo;
        private System.Windows.Forms.TextBox txtNewOrderNo;
        private System.Windows.Forms.TextBox textProdNo;
        private System.Windows.Forms.TextBox textNewOrderCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNewOrderOK;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label_Prepare;
        private System.Windows.Forms.ComboBox comboBox_Prepare;

	}
}