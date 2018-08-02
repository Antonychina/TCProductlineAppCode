using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;
using System.Management;
using Microsoft.Reporting.WinForms;
using CommonHelper;
using AH.AutoServer;
using AH.Network;

namespace Mubea.AutoTest.GUI
{
    public partial class UserAuthority : BaseViewForm
    {
        private ConfigHelper _autoTestConfig = new ConfigHelper();
        private ConfigHelper _testerDataConfig = new ConfigHelper();
        public static bool isSupportReTest = false;
        public static int alarmOnDUTFailTimes = 2;
        public UserAuthority()
        {
            InitializeComponent();
            InitializeListView(lsvUserInfo);
            RefreshForm += User_Refresh;
        }


        private void UserAuthority_Load(object sender, EventArgs e)
        {
            //
            lsvUser_Load("");

            //initialize access right set.           
            combo_Authority.Items.Add("操作员");
            combo_Authority.Items.Add("管理员");
            combo_Authority.Items.Add("超级管理员");
            combo_Authority.SelectedIndex = -1;


            //if the current user is not admin/muberadmin, disable all the btns
            //if (Login.Authority != LoginAuthority.Administrator && Login.Authority != LoginAuthority.SuperAdmin)
            if (UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.SuperAdmin && UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.Administrator)
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;//edit
                btnConfirm.Enabled = false;//confirm
                btnCancel.Enabled = false;//cancel            
                btnRemove.Enabled = false;//delete  

                btnAdd.BackgroundImage = Properties.Resources._5_add_disable;
                btnEdit.BackgroundImage = Properties.Resources._5_edit_disable;
                btnConfirm.BackgroundImage = Properties.Resources._5_ok_disable;
                btnCancel.BackgroundImage = Properties.Resources._5_cancel_disable;
                btnRemove.BackgroundImage = Properties.Resources._5_delete_disable;


                txt_UserName.Enabled = false;
                txt_Password.Enabled = false;
                txt_PasswordConfirm.Enabled = false;
                combo_Authority.Enabled = false;
            }
        }

        private void User_Refresh()
        {
            if (UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.SuperAdmin && UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.Administrator)
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;//edit
                btnConfirm.Enabled = false;//confirm
                btnCancel.Enabled = false;//cancel            
                btnRemove.Enabled = false;//delete  

                btnAdd.BackgroundImage = Properties.Resources._5_add_disable;
                btnEdit.BackgroundImage = Properties.Resources._5_edit_disable;
                btnConfirm.BackgroundImage = Properties.Resources._5_ok_disable;
                btnCancel.BackgroundImage = Properties.Resources._5_cancel_disable;
                btnRemove.BackgroundImage = Properties.Resources._5_delete_disable;

                txt_UserName.Enabled = false;
                txt_Password.Enabled = false;
                txt_PasswordConfirm.Enabled = false;
                combo_Authority.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;//edit
                btnConfirm.Enabled = true;//confirm
                btnCancel.Enabled = true;//cancel            
                btnRemove.Enabled = true;//delete  

                btnAdd.BackgroundImage = Properties.Resources._5_add;
                btnEdit.BackgroundImage = Properties.Resources._4_edit;
                btnConfirm.BackgroundImage = Properties.Resources._5_ok;
                btnCancel.BackgroundImage = Properties.Resources._5_cancel;
                btnRemove.BackgroundImage = Properties.Resources._5_delete;

                txt_UserName.Enabled = true;
                txt_Password.Enabled = true;
                txt_PasswordConfirm.Enabled = true;
                combo_Authority.Enabled = true;
            }
        }


        private void lsvUser_Load(string userID)
        {
            // UserInfoQuery db 
            List<UserInfo> userInfoQryResultArr = UserInfos.GetUserInfoQryResult();

            lsvUserInfo.Items.Clear();

            foreach (var Strtemp in userInfoQryResultArr)
            {
                ListViewItem lv = new ListViewItem();

                lv.Tag = Strtemp.UserID;

                lv.Text = Strtemp.UserName.Trim();
                lv.SubItems.Add((Strtemp.Role == (byte)LoginAuthority.Administrator) ? "管理员" :
                               ((Strtemp.Role == (byte)LoginAuthority.SuperAdmin) ? "超级管理员" :
                                (Strtemp.Role == (byte)LoginAuthority.Operator) ? "操作员" : "其他"));

                lsvUserInfo.Items.Add(lv);

                if (lv.Tag.Equals(userID))
                {
                    lv.Selected = true;
                }
            }
        }



        private void lsvUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fill the data to the proper place
            if (lsvUserInfo.SelectedItems.Count == 1)
            {
                txt_UserName.Enabled = false;
                txt_Password.Enabled = false;
                txt_PasswordConfirm.Enabled = false;
                combo_Authority.Enabled = false;

                //display in textbox
                txt_UserName.Text = lsvUserInfo.SelectedItems[0].SubItems[0].Text;
                txt_Password.Text = "********";
                txt_PasswordConfirm.Text = "********";
                combo_Authority.SelectedIndex = UserInfos.GetRoleByUser(txt_UserName.Text);

                if (Login.UserName == "MuberAdmin"
                    || Login.UserName == txt_UserName.Text)
                {
                    //用户只能编辑自己的用户名和密码；超级管理员权限用户编辑其他用户的信息;
                    btnEdit.Enabled = true;
                }
                else
                {
                    btnEdit.Enabled = false;
                }

                btnConfirm.Enabled = false;	//confirm
                btnEdit.Enabled = true;	//confirm
                btnAdd.Enabled = true;	//confirm
                btnCancel.Enabled = true;	//cancel 
                btnRemove.Enabled = (UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.Operator && UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.Unkown);

                btnAdd.BackgroundImage = Properties.Resources._5_add;
                btnEdit.BackgroundImage = Properties.Resources._4_edit;
                btnConfirm.BackgroundImage = Properties.Resources._5_ok_disable;
                btnCancel.BackgroundImage = Properties.Resources._5_cancel;
                btnRemove.BackgroundImage = (UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.Operator && UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.Unkown) ?
                                            Properties.Resources._5_delete : Properties.Resources._5_delete_disable;

            }
            else// if (this.lsvUser.SelectedItems.Count == 0) 
            {
                txt_UserName.Text = String.Empty;
                txt_Password.Text = String.Empty;
                txt_PasswordConfirm.Text = String.Empty;
                combo_Authority.SelectedIndex = -1;

                txt_UserName.Enabled = false;
                txt_Password.Enabled = false;
                txt_PasswordConfirm.Enabled = false;
                combo_Authority.Enabled = false;

                btnEdit.Enabled = false;//edit
                btnConfirm.Enabled = false;//confirm
                btnCancel.Enabled = false;//cancel            
                btnRemove.Enabled = false;//delete 
                combo_Authority.Enabled = false;
            }
        }

        private void addoneuserintoDB()
        {
            ListViewItem lv = new ListViewItem();
            UserInfo tmpUserInfo = new UserInfo();
            //insert data to DB.      
            tmpUserInfo.UserName = txt_UserName.Text;

            tmpUserInfo.Password = txt_Password.Text.Trim();

            //tmpUserInfo.Role = (byte)combo_Authority.SelectedIndex;
            tmpUserInfo.Role = (byte)((combo_Authority.Text.Trim() == "管理员") ? LoginAuthority.Administrator :
                    ((combo_Authority.Text.Trim() == "超级管理员") ? LoginAuthority.SuperAdmin :
                    ((combo_Authority.Text.Trim() == "操作员") ? LoginAuthority.Operator : LoginAuthority.Unkown)));

            tmpUserInfo.InsertUserInfoTable();

            lsvUser_Load(tmpUserInfo.UserID);
            SystemLogs.WriteSystemLog(Login.UserName, "Adding User " + tmpUserInfo.UserName + "to database ", DateTime.Now);

        }





        private void btnCancel_Click(object sender, EventArgs e)//button 'cancel 取消'
        {
            //clear testbox;
            txt_UserName.Text = String.Empty;
            txt_Password.Text = String.Empty;
            txt_PasswordConfirm.Text = String.Empty;
            combo_Authority.SelectedIndex = -1;

            //reset the signal from button 'edit' click.
            _opMode = Operation.None;

            combo_Authority.Enabled = false;

            btnAdd.Enabled = (UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.Operator && UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.Unkown);

            btnEdit.Enabled = true;//edit
            btnConfirm.Enabled = true;//confirm
            btnCancel.Enabled = true;//cancel            
            btnRemove.Enabled = true;//delete 
            combo_Authority.Enabled = true;

            lsvUserInfo.Enabled = true;
            lsvUserInfo.SelectedItems.Clear();

            btnAdd.BackgroundImage = Properties.Resources._5_add;
            btnEdit.BackgroundImage = Properties.Resources._4_edit;
            btnConfirm.BackgroundImage = Properties.Resources._5_ok;
            btnCancel.BackgroundImage = Properties.Resources._5_cancel;
            btnRemove.BackgroundImage = Properties.Resources._5_delete;

            //             if (lsvUser.SelectedItems.Count == 1)
            //             {
            // 				btnEdit.Enabled = lsvUser.SelectedItems[0].SubItems[0].Text == Login.UserName;//edit
            // 				btnDel.Enabled = Login.Authority != LoginAuthority.Operator;
            //             }
            //             else
            //             {
            //                 btnEdit.Enabled = false;//edit
            //                 btnDel.Enabled = false;//delete 
            //             }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            //actually, it should always pass here!
            if (UserInfos.GetRoleByUser(Login.UserName) == (byte)LoginAuthority.Operator)
            {
                SpMessageBox.Show("您没有添加用户的权限！", "用户管理");
                return;
            }
            //always, above.

            txt_UserName.Focus();
            txt_UserName.Enabled = true;
            txt_Password.Enabled = true;
            txt_PasswordConfirm.Enabled = true;
            combo_Authority.Enabled = true;

            txt_UserName.Text = String.Empty;
            txt_Password.Text = String.Empty;
            txt_PasswordConfirm.Text = String.Empty;
            combo_Authority.SelectedIndex = -1;

            btnAdd.Enabled = false;//add
            btnEdit.Enabled = false;//edit
            btnConfirm.Enabled = true;//confirm
            btnCancel.Enabled = true;//cancel            
            btnRemove.Enabled = false;//delete 


            btnAdd.BackgroundImage = Properties.Resources._5_add_disable;
            btnEdit.BackgroundImage = Properties.Resources._5_edit_disable;
            btnConfirm.BackgroundImage = Properties.Resources._5_ok;
            btnCancel.BackgroundImage = Properties.Resources._5_cancel;
            btnRemove.BackgroundImage = Properties.Resources._5_delete_disable;


            _opMode = Operation.Add;

            lsvUserInfo.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (lsvUserInfo.SelectedItems.Count < 1)
            {
                SpMessageBox.Show("请先选择一个用户，再进行编辑.", "用户管理");
                return;
            }

            if (UserInfos.GetRoleByUser(Login.UserName) == (byte)LoginAuthority.Operator)
            {
                SpMessageBox.Show("您没有添加用户的权限！", "用户管理");
                return;
            }

            combo_Authority.Enabled = true;	//超级管理员用户才能编辑其他用户的权限

            txt_UserName.Enabled = true; //could not change name
            txt_Password.Enabled = true;
            txt_PasswordConfirm.Enabled = true;
            txt_UserName.Text = lsvUserInfo.SelectedItems[0].SubItems[0].Text.Trim();
            combo_Authority.SelectedIndex = ((lsvUserInfo.SelectedItems[0].SubItems[1].Text.Trim() == "管理员") ? 1 :
                    ((combo_Authority.Text.Trim() == "超级管理员") ? 2 :
                    ((combo_Authority.Text.Trim() == "操作员") ? 0 : -1))); ;
            txt_Password.Text = "";
            txt_PasswordConfirm.Text = "";

            btnAdd.Enabled = false;//add
            btnEdit.Enabled = false;//edit
            btnConfirm.Enabled = true;//confirm
            btnCancel.Enabled = true;//cancel            
            btnRemove.Enabled = false;//delete 

            btnAdd.BackgroundImage = Properties.Resources._5_add_disable;
            btnEdit.BackgroundImage = Properties.Resources._5_edit_disable;
            btnConfirm.BackgroundImage = Properties.Resources._5_ok;
            btnCancel.BackgroundImage = Properties.Resources._5_cancel;
            btnRemove.BackgroundImage = Properties.Resources._5_delete_disable;

            _opMode = Operation.Edit;

            lsvUserInfo.Enabled = false;
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            List<UserInfo> userInfoQryResultArr = UserInfos.GetUserInfoQryResult();

            int adminCount = userInfoQryResultArr.Count(c => c.Role == (byte)LoginAuthority.Administrator);

            if (txt_UserName.Text.Trim() == "")
            {
                SpMessageBox.Show("用户名不能为空！", "用户管理");
                return;
            }

            if (txt_Password.Text.Trim() == "")
            {
                SpMessageBox.Show("密码不能为空！", "用户管理");
                return;
            }

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(txt_UserName.Text.Trim(), "MuberAdmin"))
            {
                SpMessageBox.Show("此用户名不可用！", "用户管理");
                return;
            }

            if (String.Compare(txt_Password.Text, txt_PasswordConfirm.Text) != 0)
            {
                SpMessageBox.Show("两次输入的密码不一致！", "用户管理");
                return;
            }

            if (combo_Authority.SelectedIndex == -1)
            {
                SpMessageBox.Show("请选择用户权限！", "用户管理");
                return;
            }

            //add 
            if (_opMode == Operation.Add)
            {

                foreach (ListViewItem lsvUserItemtmp in lsvUserInfo.Items)
                {
                    if (String.Compare(lsvUserItemtmp.Text, txt_UserName.Text.Trim()) == 0)
                    {
                        SpMessageBox.Show("用户已存在！", "用户管理");
                        return;
                    }
                }

                addoneuserintoDB();
            }
            else if (_opMode == Operation.Edit)//button 'Edit' has been clicked
            {
                //if (Login.Authority == LoginAuthority.SuperAdmin)
                //{
                //    if (adminCount > 1)
                //    {
                //        SpMessageBox.Show("只允许有一个管理员用户！");
                //        return;
                //    }
                //}

                UserInfo tmpUserInfo = new UserInfo();
                tmpUserInfo.UserID = lsvUserInfo.SelectedItems[0].Tag.ToString();
                tmpUserInfo.UserName = txt_UserName.Text.Trim();
                tmpUserInfo.Password = txt_Password.Text.Trim();
                tmpUserInfo.Role = (byte)((combo_Authority.Text.Trim() == "管理员") ? LoginAuthority.Administrator :
                    ((combo_Authority.Text.Trim() == "超级管理员") ? LoginAuthority.SuperAdmin :
                    ((combo_Authority.Text.Trim() == "操作员") ? LoginAuthority.Operator : LoginAuthority.Unkown)));

                if (tmpUserInfo.UpdateUserInfo())
                {
                    //if (Login.UserName != "MuberAdmin")
                    //{
                    //    Login.UserName = tmpUserInfo.UserName;
                    //    Login.Authority = (LoginAuthority)UserInfos.GetRoleByUser(tmpUserInfo.UserName);
                    //}

                    lsvUser_Load(tmpUserInfo.UserID);
                }
            }

            btnAdd.Enabled = (UserInfos.GetRoleByUser(Login.UserName) == (byte)LoginAuthority.Administrator || UserInfos.GetRoleByUser(Login.UserName) == (byte)LoginAuthority.SuperAdmin);

            _opMode = Operation.None;

            lsvUserInfo.Enabled = true;

            txt_UserName.Text = String.Empty;
            txt_Password.Text = String.Empty;
            txt_PasswordConfirm.Text = String.Empty;
            combo_Authority.SelectedIndex = -1;

            btnAdd.Enabled = true;//add
            btnEdit.Enabled = true;//edit
            btnConfirm.Enabled = true;//confirm
            btnCancel.Enabled = true;//cancel            
            btnRemove.Enabled = true;//delete 

            btnAdd.BackgroundImage = Properties.Resources._5_add;
            btnEdit.BackgroundImage = Properties.Resources._4_edit;
            btnConfirm.BackgroundImage = Properties.Resources._5_ok;
            btnCancel.BackgroundImage = Properties.Resources._5_cancel;
            btnRemove.BackgroundImage = Properties.Resources._5_delete;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            if (lsvUserInfo.SelectedItems.Count < 1)
            {
                SpMessageBox.Show("请先选择一个用户，再进行编辑.", "用户管理");
                return;
            }

            if (UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.Operator && UserInfos.GetRoleByUser(Login.UserName) != (byte)LoginAuthority.Unkown)
            {
                if (UserInfos.GetRoleByUser(lsvUserInfo.SelectedItems[0].SubItems[0].Text.Trim()) == (int)LoginAuthority.Administrator || UserInfos.GetRoleByUser(lsvUserInfo.SelectedItems[0].SubItems[0].Text.Trim()) == (int)LoginAuthority.SuperAdmin)
                {
                    SpMessageBox.Show("不能删除管理员用户！", "用户管理");
                    return;
                }
            }

            if (SpMessageBox.Show("确定删除？", "用户管理", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                UserInfo tmpUserInfo = new UserInfo();

                tmpUserInfo.UserName = lsvUserInfo.SelectedItems[0].SubItems[0].Text;

                if (tmpUserInfo.DeleteUserInfoTable())
                {
                    lsvUserInfo.SelectedItems[0].Remove();
                }
                else
                {
                    SpMessageBox.Show("删除失败！", "用户管理");
                    lsvUserInfo.SelectedItems[0].Selected = false;
                }

                //clear testbox;
                txt_UserName.Text = String.Empty;
                txt_Password.Text = String.Empty;
                txt_PasswordConfirm.Text = String.Empty;
                combo_Authority.SelectedIndex = -1;

                btnRemove.Enabled = false;
                btnEdit.Enabled = false;


                btnEdit.BackgroundImage = Properties.Resources._5_edit_disable;
                btnRemove.BackgroundImage = Properties.Resources._5_delete_disable;


                btnAdd.Enabled = true;//add
                btnEdit.Enabled = true;//edit
                btnConfirm.Enabled = true;//confirm
                btnCancel.Enabled = true;//cancel            
                btnRemove.Enabled = true;//delete 

                btnAdd.BackgroundImage = Properties.Resources._5_add;
                btnEdit.BackgroundImage = Properties.Resources._4_edit;
                btnConfirm.BackgroundImage = Properties.Resources._5_ok;
                btnCancel.BackgroundImage = Properties.Resources._5_cancel;
                btnRemove.BackgroundImage = Properties.Resources._5_delete;
            }
        }
    }
}