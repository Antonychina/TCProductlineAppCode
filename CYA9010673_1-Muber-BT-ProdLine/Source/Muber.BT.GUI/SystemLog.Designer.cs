using Mubea.AutoTest.GUI.Localization;
namespace Mubea.AutoTest.GUI
{
	partial class SystemLog
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
            this.dateTime_StartTime = new System.Windows.Forms.DateTimePicker();
            this.lsvSysLog = new System.Windows.Forms.ListView();
            this.colNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_Operator = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDetial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_datetime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_Refresh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Export = new System.Windows.Forms.Button();
            this.textBox_Log_Keyword = new System.Windows.Forms.TextBox();
            this.label_Keyword = new System.Windows.Forms.Label();
            this.btn_LogSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateTime_StartTime
            // 
            this.dateTime_StartTime.AllowDrop = true;
            this.dateTime_StartTime.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTime_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime_StartTime.Location = new System.Drawing.Point(526, 15);
            this.dateTime_StartTime.Name = "dateTime_StartTime";
            this.dateTime_StartTime.Size = new System.Drawing.Size(152, 29);
            this.dateTime_StartTime.TabIndex = 19;
            this.dateTime_StartTime.ValueChanged += new System.EventHandler(this.dateTime_StartTime_ValueChangedFromDB);
            // 
            // lsvSysLog
            // 
            this.lsvSysLog.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lsvSysLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNo,
            this.col_Operator,
            this.colDetial,
            this.col_datetime});
            this.lsvSysLog.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsvSysLog.Location = new System.Drawing.Point(10, 67);
            this.lsvSysLog.Name = "lsvSysLog";
            this.lsvSysLog.Size = new System.Drawing.Size(809, 611);
            this.lsvSysLog.TabIndex = 0;
            this.lsvSysLog.UseCompatibleStateImageBehavior = false;
            this.lsvSysLog.View = System.Windows.Forms.View.Details;
            // 
            // colNo
            // 
            this.colNo.Text = "序号";
            this.colNo.Width = 75;
            // 
            // col_Operator
            // 
            this.col_Operator.Text = "操作员";
            this.col_Operator.Width = 108;
            // 
            // colDetial
            // 
            this.colDetial.Text = "内容";
            this.colDetial.Width = 410;
            // 
            // col_datetime
            // 
            this.col_datetime.Text = "操作时间";
            this.col_datetime.Width = 212;
            // 
            // button_Refresh
            // 
            this.button_Refresh.Location = new System.Drawing.Point(1269, 924);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(173, 60);
            this.button_Refresh.TabIndex = 18;
            this.button_Refresh.Text = "刷新";
            this.button_Refresh.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label3.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(458, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 22);
            this.label3.TabIndex = 14;
            this.label3.Text = "日期：";
            // 
            // button_Export
            // 
            this.button_Export.Enabled = false;
            this.button_Export.Location = new System.Drawing.Point(1487, 924);
            this.button_Export.Name = "button_Export";
            this.button_Export.Size = new System.Drawing.Size(173, 60);
            this.button_Export.TabIndex = 10;
            this.button_Export.Text = "导出";
            this.button_Export.UseVisualStyleBackColor = true;
            // 
            // textBox_Log_Keyword
            // 
            this.textBox_Log_Keyword.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.textBox_Log_Keyword.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Log_Keyword.Location = new System.Drawing.Point(123, 14);
            this.textBox_Log_Keyword.MaxLength = 30;
            this.textBox_Log_Keyword.Name = "textBox_Log_Keyword";
            this.textBox_Log_Keyword.Size = new System.Drawing.Size(134, 29);
            this.textBox_Log_Keyword.TabIndex = 0;
            // 
            // label_Keyword
            // 
            this.label_Keyword.AutoSize = true;
            this.label_Keyword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label_Keyword.Font = new System.Drawing.Font("YaHei Consolas Hybrid", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Keyword.ForeColor = System.Drawing.Color.White;
            this.label_Keyword.Location = new System.Drawing.Point(43, 17);
            this.label_Keyword.Name = "label_Keyword";
            this.label_Keyword.Size = new System.Drawing.Size(82, 22);
            this.label_Keyword.TabIndex = 11;
            this.label_Keyword.Text = "关键字：";
            // 
            // btn_LogSearch
            // 
            this.btn_LogSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btn_LogSearch.BackgroundImage = global::Mubea.AutoTest.GUI.Properties.Resources._3查询;
            this.btn_LogSearch.FlatAppearance.BorderSize = 0;
            this.btn_LogSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LogSearch.Location = new System.Drawing.Point(274, 9);
            this.btn_LogSearch.Name = "btn_LogSearch";
            this.btn_LogSearch.Size = new System.Drawing.Size(78, 38);
            this.btn_LogSearch.TabIndex = 20;
            this.btn_LogSearch.UseVisualStyleBackColor = false;
            this.btn_LogSearch.Click += new System.EventHandler(this.btn_LogSearch_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(-20, -3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(895, 60);
            this.label1.TabIndex = 21;
            // 
            // SystemLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.btn_LogSearch);
            this.Controls.Add(this.dateTime_StartTime);
            this.Controls.Add(this.lsvSysLog);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Log_Keyword);
            this.Controls.Add(this.label_Keyword);
            this.Controls.Add(this.button_Export);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "SystemLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogForm";
            this.Load += new System.EventHandler(this.SystemLog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ListView lsvSysLog;
        private System.Windows.Forms.ColumnHeader colDetial;
		private System.Windows.Forms.Button button_Export;
        private System.Windows.Forms.ColumnHeader colNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.DateTimePicker dateTime_StartTime;
        private System.Windows.Forms.Button btn_LogSearch;
        private System.Windows.Forms.TextBox textBox_Log_Keyword;
        private System.Windows.Forms.Label label_Keyword;
        private System.Windows.Forms.ColumnHeader col_Operator;
        private System.Windows.Forms.ColumnHeader col_datetime;
        private System.Windows.Forms.Label label1;
	}
}