using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GP.MAGICL6800.ORM
{
	public class ConnectStringHelper
	{
		public static bool IsConnectedToDB
		{
			get
			{
				using (SqlConnection conn = new SqlConnection(ConnectString))
				{
					try
					{
						conn.Open();
						SqlCommand cmd = new SqlCommand("Select * From UserInfo", conn);
						cmd.ExecuteNonQuery();
						conn.Close();
					}
					catch
					{
						return false;
					}

					return true;
				}
			}
		}

		/// <summary>
		/// 用于动态修改连接字符串
		/// </summary>
		public static string ConnectString
		{
			get { return Properties.Settings.Default.CIConnectionString; }
			set 
			{ 
				Properties.Settings.Default["CIConnectionString"] = value;
				Properties.Settings.Default.Save();
			}
		}
	}
}
