using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GP.MAGICL6800.ORM.Mubea_BTDataSetTableAdapters;


/// <summary>
///  Data Table Result Operations define
///  Include Query Update Insert and Delete
/// </summary>
namespace GP.MAGICL6800.ORM
{
    /// <summary>
    /// define class to match the Sample query result
    /// </summary>

    public class UserInfoQueryBLL
    {
        private static UserInfoTableAdapter _UserInfoTableAdapter;

        private static Mubea_BTDataSet.UserInfoDataTable _UserInfoTable;


        public static Mubea_BTDataSet.UserInfoDataTable UserInfoTable
        {
            get
            {
                //if (_UserInfoTable == null)
                {
                    _UserInfoTable = _UserInfoTableAdapter.GetData();
                }

                return _UserInfoTable;
            }
        }
        

        static UserInfoQueryBLL()
        {
            _UserInfoTableAdapter = new UserInfoTableAdapter();
        }

        public static void RefreshUserInfoTable()
        {
            _UserInfoTable = _UserInfoTableAdapter.GetData();
        }



        /// <summary>
        /// BLL functions
        /// </summary>
        /// <param name="_userID"></param>
        /// <param name="_userName"></param>
        /// <param name="_password"></param>
        /// <param name="_role"></param>
        /// <returns></returns>
        public static bool UserInfoInsert(string _userID, string _userName, string _password, byte _role)
        {
            _UserInfoTableAdapter.InsertUser(_userID, _userName, _password, _role);

            return true;
        }

        public static bool UserInfoDelete(string _userName)
        {
            _UserInfoTableAdapter.DeleteUser(_userName);

            return true;
        }

        public static bool UpdateUserInfo(Mubea_BTDataSet.UserInfoRow row)
		{
			return _UserInfoTableAdapter.Update(row) > 0;
		}

        /// <summary>
        /// Get all names without repeat
        /// </summary>
        /// <returns></returns>
        public static List<string> GetUsers()
        {
            List<string> userArr = new List<string>();

            foreach (var val in UserInfoTable)
            {
                // This is to delete duplicate comboName
                if (!(userArr.Exists(item => item.Equals(val.UserName))))
                {
                    userArr.Add(val.UserName); ;
                }
            }

            return userArr;
        }
    }
}
