using System;
using System.Collections.Generic;
using System.Text;
using GP.MAGICL6800.ORM;

namespace Mubea.AutoTest
{
    /// <summary>
    /// Single sample.
    /// </summary>
    public class UserInfo
    {
        private string _userID;
        private string _userName;        
        private string _password;
        private byte _role; 

        /// <summary>
        /// The name. E.G. RBS_NODE_MODEL_L
        /// </summary>
        public UserInfo()
        {
            _userID = Guid.NewGuid().ToString();
			_userName = "";
			_password = "";
			_role = (byte)LoginAuthority.Operator;
        }

        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public byte Role
        {
            get { return _role; }
            set { _role = value; }
        }

        /// <summary>
        /// UpdateSampleMain, update sample info into samplemian, if update succeed, return True, else return False
        /// </summary>
        /// <returns></returns>
        public bool InsertUserInfoTable()
        {
			try
			{
				UserInfoQueryBLL.UserInfoInsert(_userID, _userName, _password, _role);
			}
			catch (Exception ex)
			{
				//LogHelper.ToLog("DBErr.txt", ex);

				return false;
			}

            return true;
        }

        public bool DeleteUserInfoTable()
        {
			try
			{
				UserInfoQueryBLL.UserInfoDelete(_userName);
			}
			catch (Exception ex)
			{
				//LogHelper.ToLog("DBErr.txt", ex);

				return false;
			}

            return true;
        }

        public bool UpdateUserInfo()
        {
            Mubea_BTDataSet.UserInfoRow row = UserInfoQueryBLL.UserInfoTable.FindByNameId(UserID);

            if (row != null)
            {
                row.UserName = this.UserName;
                row.Password = this.Password;
                row.Role = this.Role;

                return UserInfoQueryBLL.UpdateUserInfo(row);
            }

            return false;
        }
    }
}
