using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Mubea.AutoTest;
using Mubea.AutoTest.GUI;
using Mubea.AutoTest.GUI.Localization;
using Mubea.GUI.CustomControl;
using AH.Network;

namespace CommonHelper
{
    /// <summary>  
    /// Task number generator
    /// </summary>  
    public static class RegularHelper
    {
        /// <summary>
        /// Check if string consists of 5 number
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public static bool CheckPatientNO(string patientID)
        {
            string pattern = @"^\d{5}$";
            if (!Regex.IsMatch(patientID, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

        /// <summary>
        ///  Check if string consists of 10 number
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="lenth"></param>
        /// <returns></returns>
        public static bool CheckNumbers(string barcode, int lenth)
        {
            string pattern = @"^\d{" + lenth.ToString() + @"}$";
            if (Regex.IsMatch(barcode, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool isDigtal(KeyPressEventArgs e)
        {
            try
            {
                int kc = (int)e.KeyChar;
                //kc = 0-9, enter, backspace, ctrl+a/z/x/c/v
                if ((kc >= 48 && kc <= 57) || kc == 8 || kc == 3 || kc == 13 || kc ==1 || kc == 22 || kc ==24 || kc==26)
                {
                    e.Handled = false;
                    return true;
                }
                else
                {
                    e.Handled = true;
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Can only used for short string, that can convert to int type 
        /// </summary>
        /// <param name="inputstring"></param>
        /// <returns></returns>
        public static bool isDigtal2(string inputstring)
        {
            try
            {
                //use Convert.ToInt32() or int.Parse() to check the input string is number or not
                //int parseResult = Convert.ToInt32(this.textBox1.Text);   
                if (inputstring.Length > 0)
                {
                    int parseResult = int.Parse(inputstring);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// check the string is numeric string
        /// </summary>
        /// <param name="numericstring"></param>
        /// <returns>True or False</returns>
        public static bool isNumericString(string numericstring)
        {
            if (numericstring == "" || numericstring.Length < 1)
            {
                return false;
            }

            for (int i = 0; i < numericstring.Length; i++)
            {
                if ((numericstring[i] >= 48 && numericstring[i] <= 57))
                {
                    continue;
                }
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// check the input as 'double' number. 
        /// </summary>
        /// <param name="numeric_or_point_string"></param>
        /// <returns></returns>
        public static bool is_Numeric_or_Point_String(string numeric_or_point_string)
        {
            byte point_num = 0;                                      

            for (int i = 0; i < numeric_or_point_string.Length; i++)
            {
                if ((numeric_or_point_string[i] >= 48 && numeric_or_point_string[i] <= 57))                
                    continue;                
                
                if (numeric_or_point_string[i] == 46) //'.'
                {
                    if(i == 0)
                        return false; // '.123' is wrong, return false.

                    point_num++;
                    if(point_num < 2)
                        continue;   //'0.1' is correct, continue. '0.1.2' is incorect, return false;
                }
                
                return false;
            }

            return true;
        }


        /// <summary>
        /// check the string is numeric or alpha or mix string
        /// </summary>
        /// <param name="inputtring"></param>
        /// <returns>True or False</returns>
        public static bool isNumericOrAlphaString(string inputtring)
        {
            for (int i = 0; i < inputtring.Length; i++)
            {
                if ((inputtring[i] >= 48 && inputtring[i] <= 57) ||
                    (inputtring[i] >= 65 && inputtring[i] <= 90) ||
                    (inputtring[i] >= 97 && inputtring[i] <= 122))
                {
                    continue;
                }
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool isAlphabet(KeyPressEventArgs e)
        {
            try
            {
                int kc = (int)e.KeyChar;
                //kc = a-z,A-Z, enter, backspace, ctrl+a/z/x/c/v
                if (((kc >= 65 && kc <= 90) || (kc >= 97 && kc <= 122)) || kc == 8 || kc == 3 || kc == 13 || kc == 1 || kc == 22 || kc == 24 || kc == 26)
                {
                    e.Handled = false;
                    return true;
                }
                else
                {
                    e.Handled = true;
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MaintFrequency"></param>
        /// <returns></returns>
        public static DateTime GetNextMaintTime(String MaintFrequency)
        { 
            DateTime _datetime = DateTime.Now;
            if (MaintFrequency.Contains("天") || MaintFrequency.ToLower().Contains("da"))
                _datetime = DateTime.Now.AddDays(1);
            else if (MaintFrequency.Contains("周") || MaintFrequency.ToLower().Contains("week"))
                _datetime = DateTime.Now.AddDays(7);
            else if (MaintFrequency.Contains("月") || MaintFrequency.ToLower().Contains("month"))
                _datetime = DateTime.Now.AddMonths(1);
            else if (MaintFrequency.Contains("季") || MaintFrequency.ToLower().Contains("quarter"))
                _datetime = DateTime.Now.AddMonths(3);
            else if (MaintFrequency.Contains("年") || MaintFrequency.ToLower().Contains("annual"))
                _datetime = DateTime.Now.AddYears(1);
            else
                return DateTime.MaxValue;

            return _datetime;
        }

        /// <summary>
        /// CheckTimeInPeriod
        /// </summary>
        /// <param name="timeTobeChecked"></param>
        /// <param name="MaintFrequency"></param>
        /// <returns></returns>
        public static bool CheckTimeInPeriod(DateTime timeTobeChecked,String MaintFrequency)
        {
            DateTime datetimeNow = DateTime.Now;
			if (MaintFrequency == "MaintFreq_Daily")
            {
                if (timeTobeChecked.AddDays(1) < datetimeNow)  //超过了维护周期
                    return false;
                else
                    return true;
            }
			else if (MaintFrequency == "MaintFreq_Weekly")
            {
                if (timeTobeChecked.AddDays(7) < datetimeNow)  //超过了维护周期
                    return false;
                else
                    return true;
            }
			else if (MaintFrequency == "MaintFreq_Monthly")
            {
                if (timeTobeChecked.AddMonths(1) < datetimeNow)  //超过了维护周期
                    return false;
                else
                    return true;
            }
			else if (MaintFrequency == "MaintFreq_Quarterly")
            {
                if (timeTobeChecked.AddMonths(3) < datetimeNow)  //超过了维护周期
                    return false;
                else
                    return true;
            }
			else if (MaintFrequency == "MaintFreq_Annually")
            {
                if (timeTobeChecked.AddYears(1) < datetimeNow)  //超过了维护周期
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ageNumber"></param>
        /// <param name="ageUnit"></param>
        /// <returns></returns>
        public static double AgeConversion(double ageNumber, byte ageUnit)
        {
            double ageAfterConvert = 0.0;
            switch (ageUnit)
            {
                case 1:
                    ageAfterConvert = ageNumber;
                    break;
                case 2:
                    ageAfterConvert = ageNumber/12.0;
                    break;
                case 3:
                    ageAfterConvert = ageNumber /365.0;
                    break;
                default:
                    ageAfterConvert = 0.0;
                    break;
            }
            return ageAfterConvert; 
        }


        public static byte[] GetCRC16(byte[] data)
        {
            if (data.Length == 0)
                throw new Exception("调用CRC16校验算法,（低字节在前，高字节在后）时发生异常，异常信息：被校验的数组长度为0。");
            byte[] temdata = new byte[data.Length + 2];
            int xda, xdapoly;
            byte i, j, xdabit;
            xda = 0xFFFF;
            xdapoly = 0xA001;
            for (i = 0; i < data.Length; i++)
            {
                xda ^= data[i];
                for (j = 0; j < 8; j++)
                {
                    xdabit = (byte)(xda & 0x01);
                    xda >>= 1;
                    if (xdabit == 1)
                        xda ^= xdapoly;
                }
            }
            temdata = new byte[2] { (byte)(xda & 0xFF), (byte)(xda >> 8) };
            return temdata;
        }
    }
}
