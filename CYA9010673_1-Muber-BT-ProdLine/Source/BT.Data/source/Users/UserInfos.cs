using System;
using System.Collections.Generic;
using System.Text;
using GP.MAGICL6800.ORM;

namespace Mubea.AutoTest
{
    /// <summary>
    /// Single sample.
    /// </summary>
    public class UserInfos
    {
        List<UserInfo> _userInfos;
        
        /// <summary>
        /// Constructure of samples
        /// </summary>
        public UserInfos()
        {
            _userInfos = new List<UserInfo>();
        }

        /// <summary>
        /// Get Samples
        /// </summary>
        List<UserInfo> UserInfoList
        {
            get { return _userInfos; }
        }


        /// <summary>
        /// Create single sample
        /// </summary>
        /// <param name="sampleNO">sampleNO</param>
        /// <param name="slotNo">slotNo</param>
        /// <param name="cupNo">cupNo</param>
        /*public void CreateSamples(string sampleNO, int slotNo, int cupNo)
        {
            Sample sample = new Sample(sampleNO);
            
            //Add property here
            sample.SlotNo = slotNo;
            sample.CupNo = cupNo;

            _samples.Add(sample);
        }*/

        /// <summary>
        /// Create sample
        /// </summary>
        /// <param name="sampleNO"></param>
        /// <param name="slotNo"></param>
        /// <param name="cupNo"></param>
        /// <param name="repeatTimes"></param>
        /// <param name="testType"></param>
        /// <param name="sampleType"></param>
        /// <param name="dilu"></param>
        /// <param name="reagents"></param>
        public void CreateUserInfo(string userID, string userName, string password, byte role)
        {
            UserInfo userInfo = new UserInfo();

            //Add property here
            userInfo.UserID = Guid.NewGuid().ToString();
            userInfo.UserName = userName;
            userInfo.Password = password;
            userInfo.Role = role;

            //sample.CreateTasks(reagents);

            _userInfos.Add(userInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<UserInfo> GetUserInfoQryResult()
        {
            List<UserInfo> retUserInfoList = new List<UserInfo>();
            //int i = 0;
            foreach (var val in UserInfoQueryBLL.UserInfoTable)
            {
                UserInfo userinfotemp = new UserInfo();

                userinfotemp.UserID = val.NameId;
                userinfotemp.UserName = val.UserName;
                userinfotemp.Password = val.Password;
                userinfotemp.Role = val.Role;

                retUserInfoList.Add(userinfotemp);
            }

            return retUserInfoList;
        }


        public static List<string> GetUsersName()
        {
            return UserInfoQueryBLL.GetUsers();
        
        }

        public static string GetPWDByUser(string username)
        {
            foreach(var _userinfo in UserInfoQueryBLL.UserInfoTable)
            {
                if (_userinfo.UserName == username)
                    return _userinfo.Password;
            }
            return null;
        }

        public static byte GetRoleByUser(string username)
        {
			if (username.ToLower() == "muberadmin")
			{
				return (byte)LoginAuthority.SuperAdmin;
			}

            foreach (var _userinfo in UserInfoQueryBLL.UserInfoTable)
            {
                if (_userinfo.UserName == username)
                    return _userinfo.Role;
            }

            return (byte)LoginAuthority.Unkown;
        }
    }
}
