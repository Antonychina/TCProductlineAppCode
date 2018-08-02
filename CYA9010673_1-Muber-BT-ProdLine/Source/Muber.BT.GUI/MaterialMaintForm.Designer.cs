namespace Mubea.AutoTest.GUI
{
    partial class MaterialMaintForm
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
            this.colSex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIsEmergency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRemark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txt_UsageTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_CountPerBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_MaterialPos = new System.Windows.Forms.TextBox();
            this.txt_MaterialName = new System.Windows.Forms.TextBox();
            this.txt_MaterialNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lsv_MaterialInfo = new System.Windows.Forms.ListView();
            this.SerialNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MaterialNO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MaterialName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Position = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CountPerBox = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UsageTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
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
            // txt_UsageTime
            // 
            this.txt_UsageTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_UsageTime.ForeColor = System.Drawing.Color.Black;
            this.txt_UsageTime.Location = new System.Drawing.Point(306, 42);
            this.txt_UsageTime.Name = "txt_UsageTime";
            this.txt_UsageTime.Size = new System.Drawing.Size(120, 26);
            this.txt_UsageTime.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(227, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 19);
            this.label6.TabIndex = 20;
            this.label6.Text = "使用时间";
            // 
            // txt_CountPerBox
            // 
            this.txt_CountPerBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_CountPerBox.ForeColor = System.Drawing.Color.Black;
            this.txt_CountPerBox.Location = new System.Drawing.Point(94, 43);
            this.txt_CountPerBox.Name = "txt_CountPerBox";
            this.txt_CountPerBox.Size = new System.Drawing.Size(110, 26);
            this.txt_CountPerBox.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(28, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 19);
            this.label4.TabIndex = 19;
            this.label4.Text = "数量/箱";
            // 
            // txt_MaterialPos
            // 
            this.txt_MaterialPos.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MaterialPos.ForeColor = System.Drawing.Color.Black;
            this.txt_MaterialPos.Location = new System.Drawing.Point(527, 9);
            this.txt_MaterialPos.Name = "txt_MaterialPos";
            this.txt_MaterialPos.Size = new System.Drawing.Size(60, 26);
            this.txt_MaterialPos.TabIndex = 10;
            // 
            // txt_MaterialName
            // 
            this.txt_MaterialName.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MaterialName.ForeColor = System.Drawing.Color.Black;
            this.txt_MaterialName.Location = new System.Drawing.Point(306, 9);
            this.txt_MaterialName.Name = "txt_MaterialName";
            this.txt_MaterialName.Size = new System.Drawing.Size(120, 26);
            this.txt_MaterialName.TabIndex = 9;
            // 
            // txt_MaterialNo
            // 
            this.txt_MaterialNo.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MaterialNo.ForeColor = System.Drawing.Color.Black;
            this.txt_MaterialNo.Location = new System.Drawing.Point(94, 9);
            this.txt_MaterialNo.Name = "txt_MaterialNo";
            this.txt_MaterialNo.Size = new System.Drawing.Size(110, 26);
            this.txt_MaterialNo.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(449, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "位置编号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(227, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "零件名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(30, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "零件号 ";
            // 
            // lsv_MaterialInfo
            // 
            this.lsv_MaterialInfo.AutoArrange = false;
            this.lsv_MaterialInfo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lsv_MaterialInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SerialNumber,
            this.MaterialNO,
            this.MaterialName,
            this.Position,
            this.CountPerBox,
            this.UsageTime});
            this.lsv_MaterialInfo.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(120)));
            this.lsv_MaterialInfo.FullRowSelect = true;
            this.lsv_MaterialInfo.Location = new System.Drawing.Point(46, 91);
            this.lsv_MaterialInfo.MultiSelect = false;
            this.lsv_MaterialInfo.Name = "lsv_MaterialInfo";
            this.lsv_MaterialInfo.Size = new System.Drawing.Size(714, 518);
            this.lsv_MaterialInfo.SmallImageList = this.imageList1;
            this.lsv_MaterialInfo.TabIndex = 2;
            this.lsv_MaterialInfo.UseCompatibleStateImageBehavior = false;
            this.lsv_MaterialInfo.View = System.Windows.Forms.View.Details;
            this.lsv_MaterialInfo.Click += new System.EventHandler(this.lsv_MaterialInfo_Click);
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
            // Position
            // 
            this.Position.Text = "位置编号";
            this.Position.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Position.Width = 100;
            // 
            // CountPerBox
            // 
            this.CountPerBox.Text = "数量/箱";
            this.CountPerBox.Width = 86;
            // 
            // UsageTime
            // 
            this.UsageTime.Text = "使用时间/箱";
            this.UsageTime.Width = 115;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(826, 76);
            this.label5.TabIndex = 15;
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btn_OK.BackgroundImage = global::Mubea.AutoTest.GUI.Properties.Resources.更新;
            this.btn_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_OK.FlatAppearance.BorderSize = 0;
            this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OK.Font = new System.Drawing.Font("Microsoft YaHei UI", 12.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OK.ForeColor = System.Drawing.Color.White;
            this.btn_OK.Location = new System.Drawing.Point(709, 17);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(86, 46);
            this.btn_OK.TabIndex = 17;
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btn_Add.BackgroundImage = global::Mubea.AutoTest.GUI.Properties.Resources._5添加;
            this.btn_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Add.FlatAppearance.BorderSize = 0;
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Add.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Add.ForeColor = System.Drawing.Color.White;
            this.btn_Add.Location = new System.Drawing.Point(620, 18);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(73, 48);
            this.btn_Add.TabIndex = 16;
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // MaterialMaintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1000, 738);
            this.Controls.Add(this.txt_UsageTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_CountPerBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.txt_MaterialPos);
            this.Controls.Add(this.txt_MaterialName);
            this.Controls.Add(this.txt_MaterialNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lsv_MaterialInfo);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "MaterialMaintForm";
            this.Text = "物料维护";
            this.Load += new System.EventHandler(this.MaterialMaint_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ListView lsv_MaterialInfo;
        private System.Windows.Forms.ColumnHeader MaterialNO;
		private System.Windows.Forms.ColumnHeader Position;
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
        private System.Windows.Forms.TextBox txt_MaterialNo;
        private System.Windows.Forms.TextBox txt_MaterialName;
        private System.Windows.Forms.TextBox txt_MaterialPos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.ColumnHeader CountPerBox;
        private System.Windows.Forms.ColumnHeader UsageTime;
        private System.Windows.Forms.TextBox txt_CountPerBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_UsageTime;
        private System.Windows.Forms.Label label6;

	}
}