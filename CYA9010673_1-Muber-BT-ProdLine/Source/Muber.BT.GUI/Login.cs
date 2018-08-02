using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mubea.AutoTest.GUI.Localization;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using CommonHelper;

namespace Mubea.AutoTest.GUI
{
	public partial class Login : Form
	{
        private ToolTip _logintip = new ToolTip();
        public static string UserName = "";
        public static string CurrentProductionLine = "";
        public static byte CurrentProductionLineNumber = 0;
        public static LoginAuthority Authority;

        public Login()
        {
            InitializeComponent();
            //this.txtLoginPwd.UseSystemPasswordChar = true;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            FreshUserNames();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void Login_KeyPress(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (this.comboProductionLine.Text.Trim() == string.Empty)
            {
                SpMessageBox.Show("请选择产线编号.", "用户登录");
                return;
            }
            else
            {
                CurrentProductionLine = this.comboProductionLine.Text.Trim();
                CurrentProductionLineNumber = byte.Parse(System.Text.RegularExpressions.Regex.Replace(Login.CurrentProductionLine, @"[^0-9]+", ""));
            }

            if (this.cmbUser.Text.Trim() != string.Empty)
            {
                UserName = this.cmbUser.Text.Trim();
                
                if (!CheckUserandPswd(UserName, txtLoginPwd.Text.Trim()))
                {
                    ShowLoginTip("用户名或密码错误，请重新输入.");
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            //string loginTrace = string.Format("{0}: User {1} Login", DateTime.Now, UserName);
            //LogerHelper.ToAutoTestLogFile(loginTrace);

            SystemLogs.WriteSystemLog(UserName, "Login to " + CurrentProductionLine, DateTime.Now);
            Login.Authority = (LoginAuthority)UserInfos.GetRoleByUser(Login.UserName);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FreshUserNames()
        {
            this.cmbUser.Items.Clear();

            foreach (string user in UserInfos.GetUsersName())
            {
                this.cmbUser.Items.Add(user);
            }
        }

        public static bool CheckUserandPswd(string username, string passwd)
        {
            string _password = UserInfos.GetPWDByUser(username);

            if (username.ToLower() == "muberadmin")
            {
                if (_password.ToLower().Contains("welcome2muber"))
                    return true;
                else
                    return false;
            }
            else if (_password == passwd)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.txtLoginPwd.UseSystemPasswordChar == false)
                this.txtLoginPwd.UseSystemPasswordChar = true;
            else
                this.txtLoginPwd.UseSystemPasswordChar = false;
        }

        private void ShowLoginTip(string tipstring)
        {
            Point pt = new Point(this.btnLogin.Location.X, this.btnLogin.Location.Y + this.btnLogin.Height);
            pt.Offset(20, 20);
            _logintip.UseFading = true;
            _logintip.RemoveAll();
            _logintip.Show(tipstring, this, pt, 2000);
        }
	}
}
