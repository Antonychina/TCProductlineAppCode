namespace Mubea.AutoTest.GUI
{
	partial class EmergSupply
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_EmergSupplyOK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.txtMaterialCount = new System.Windows.Forms.TextBox();
            this.label_MaterialNo = new System.Windows.Forms.Label();
            this.comboMaterialNo = new System.Windows.Forms.ComboBox();
            this.label_MaterialCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(118, 149);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(562, 424);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // btn_EmergSupplyOK
            // 
            this.btn_EmergSupplyOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btn_EmergSupplyOK.BackgroundImage = global::Mubea.AutoTest.GUI.Properties.Resources._4确认;
            this.btn_EmergSupplyOK.FlatAppearance.BorderSize = 0;
            this.btn_EmergSupplyOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_EmergSupplyOK.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_EmergSupplyOK.Location = new System.Drawing.Point(575, 13);
            this.btn_EmergSupplyOK.Name = "btn_EmergSupplyOK";
            this.btn_EmergSupplyOK.Size = new System.Drawing.Size(81, 44);
            this.btn_EmergSupplyOK.TabIndex = 2;
            this.btn_EmergSupplyOK.Tag = "0";
            this.btn_EmergSupplyOK.UseVisualStyleBackColor = false;
            this.btn_EmergSupplyOK.Click += new System.EventHandler(this.btn_EmergSupplyOK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btn_Cancel.BackgroundImage = global::Mubea.AutoTest.GUI.Properties.Resources._4取消;
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Cancel.Location = new System.Drawing.Point(667, 13);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(72, 41);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Tag = "0";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // txtMaterialCount
            // 
            this.txtMaterialCount.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMaterialCount.Location = new System.Drawing.Point(375, 19);
            this.txtMaterialCount.Name = "txtMaterialCount";
            this.txtMaterialCount.Size = new System.Drawing.Size(100, 29);
            this.txtMaterialCount.TabIndex = 8;
            // 
            // label_MaterialNo
            // 
            this.label_MaterialNo.AutoSize = true;
            this.label_MaterialNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label_MaterialNo.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_MaterialNo.ForeColor = System.Drawing.Color.White;
            this.label_MaterialNo.Location = new System.Drawing.Point(41, 22);
            this.label_MaterialNo.Name = "label_MaterialNo";
            this.label_MaterialNo.Size = new System.Drawing.Size(73, 22);
            this.label_MaterialNo.TabIndex = 0;
            this.label_MaterialNo.Text = "零件号:";
            // 
            // comboMaterialNo
            // 
            this.comboMaterialNo.AllowDrop = true;
            this.comboMaterialNo.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboMaterialNo.FormattingEnabled = true;
            this.comboMaterialNo.Location = new System.Drawing.Point(116, 19);
            this.comboMaterialNo.Name = "comboMaterialNo";
            this.comboMaterialNo.Size = new System.Drawing.Size(120, 30);
            this.comboMaterialNo.TabIndex = 1;
            this.comboMaterialNo.SelectedIndexChanged += new System.EventHandler(this.comboMaterialNo_SelectedIndexChanged);
            this.comboMaterialNo.Click += new System.EventHandler(this.comboMaterialNo_Click);
            // 
            // label_MaterialCount
            // 
            this.label_MaterialCount.AutoSize = true;
            this.label_MaterialCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label_MaterialCount.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_MaterialCount.ForeColor = System.Drawing.Color.White;
            this.label_MaterialCount.Location = new System.Drawing.Point(301, 22);
            this.label_MaterialCount.Name = "label_MaterialCount";
            this.label_MaterialCount.Size = new System.Drawing.Size(73, 22);
            this.label_MaterialCount.TabIndex = 5;
            this.label_MaterialCount.Text = "数  量:";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(-4, -1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(884, 68);
            this.label5.TabIndex = 16;
            // 
            // EmergSupply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_EmergSupplyOK);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.txtMaterialCount);
            this.Controls.Add(this.label_MaterialNo);
            this.Controls.Add(this.comboMaterialNo);
            this.Controls.Add(this.label_MaterialCount);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "EmergSupply";
            this.Text = "物料补充";
            this.Load += new System.EventHandler(this.EmergSupply_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button btn_EmergSupplyOK;
		private System.Windows.Forms.ComboBox comboMaterialNo;
        private System.Windows.Forms.Label label_MaterialNo;
        private System.Windows.Forms.Label label_MaterialCount;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox txtMaterialCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
	}
}