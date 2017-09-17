using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common
{

    /// <summary>
    /// 通用方法类
    /// </summary>
    public static class Utility
    {
        public static bool IsNullOrBlank(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return true;
            }
            else
            {
                return s.Trim() == "";
            }
        }

        /// <summary>
        /// 判读是否为空
        /// </summary>
        /// <Author>王培贤</Author>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object obj)
        {
            bool flag = false;
            if (obj == null)
                flag = true;
            else if (string.IsNullOrEmpty(obj.ToString()))
                flag = true;
            else if (obj.ToString().Trim().Length == 0)
                flag = true;

            return flag;
        }

        public static bool IsNullOREmptyGuid(object obj)
        {
            bool flag = false;
            Guid guid;
            if (IsNullOrEmpty(obj))
                return true;
            guid = new Guid(obj.ToString());
            if (guid == Guid.Empty || guid == new Guid("00000000-0000-0000-0000-000000000000"))
                return true;

            return flag;
        }

        public static string ConvertNullToString(object val)
        {
            string str = string.Empty;
            if (val != null)
                str = val.ToString();

            return str;
        }

        /// <summary>
        /// 将string类型的值转化为整形值
        /// </summary>
        /// <param name="intString">string类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ParseToInt(string intString, int defaultValue)
        {
            int r = 0;
            if (!int.TryParse(intString, out r))
            {
                r = defaultValue;
            }

            return r;
        }

        /// <summary>
        /// 将string类型的值转化为Guid
        /// </summary>
        /// <param name="guidString">string类型的Guid值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Guid ParseToGuid(string guidString, Guid defaultValue)
        {
            Guid val = defaultValue;
            try
            {
                if (!IsNullOrBlank(guidString))
                {
                    val = new Guid(guidString);
                }
            }
            catch
            {
                val = defaultValue;
            }

            return val;
        }

        /// <summary>
        /// 将Object类型的值转化为整形值
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ConvertToINT(object obj, int defaultValue)
        {
            int val = defaultValue;
            try
            {
                if (obj == null)
                    val = defaultValue;
                else
                    val = Convert.ToInt32(obj);
            }
            catch
            {
                val = defaultValue;
            }

            return val;
        }

        /// <summary>
        /// 将Object类型的值转化为整形值,默认值为-1
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <returns></returns>
        public static int ConvertToINT(object obj)
        {
            return ConvertToINT(obj, -1);
        }

        /// <summary>
        /// 将string类型的值转化为整形值,默认为空， 适用于数据字典
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <returns></returns>
        public static int? ConvertToINTDict(string str)
        {
            int? r = ParseToInt(str, -1);
            if (r == -1)
            {
                r = null;
            }
            return r;
        }

        /// <summary>
        /// 将字典数据值转化为string类型的值，当-1或者长度为零时，转成null
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <returns></returns>
        public static string ConvertToStringDict(string str)
        {
            if (str == null || str.Trim().Length == 0 || str.Equals("-1"))
            {
                return null;
            }

            return str;
        }

        /// <summary>
        /// 将string类型的值转化为整形值,默认为空， 适用于创建记录时，表的主键字段
        /// </summary>
        /// <param name="obj">string类型的值</param>
        /// <returns></returns>
        public static int? ConvertToINTID(string str)
        {
            if (str == null || str.Length == 0)
            {
                return null;
            }

            return ParseToInt(str, -1);
        }


        /// <summary>
        /// 将Object类型的值转化为日期时间值
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(object obj, DateTime defaultValue)
        {
            DateTime val = defaultValue;
            try
            {
                if (obj == null)
                    val = defaultValue;
                else
                    val = Convert.ToDateTime(obj);
            }
            catch
            {
                val = defaultValue;
            }

            return val;
        }

        /// <summary>
        /// 将Object类型的值转化为日期时间值，默认值为DateTime.MinValue
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(object obj)
        {
            return ConvertToDateTime(obj, DateTime.MinValue);
        }

        /// <summary>
        /// 将Object类型的值转化为日期时间值，默认值为DateTime.MinValue
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <returns></returns>
        public static DateTime? ConvertToDateTimeNull(object obj)
        {
            DateTime? date = ConvertToDateTime(obj);
            if (date.Value == DateTime.MinValue)
            {
                date = null;
            }

            return date;
        }


        /// <summary>
        /// 将Object类型的值转化为Decimal值
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(object obj, decimal defaultValue)
        {
            decimal val = defaultValue;
            try
            {
                if (obj == null)
                    val = defaultValue;
                else
                    val = Convert.ToDecimal(obj);
            }
            catch
            {
                val = defaultValue;
            }

            return val;
        }

        /// <summary>
        /// 将Object类型的值转化为Decimal值
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(object obj)
        {
            decimal val;

            val = ConvertToDecimal(obj, 0);

            return val;
        }

        /// <summary>
        /// 将字符串类型的值转化为Decimal值
        /// </summary>
        /// <param name="str">字符串类型的值</param>
        /// <returns></returns>
        public static decimal? ConvertToDecimalNULL(string str)
        {
            decimal val;

            if (str == null || str.Length == 0) return null;
            if (!decimal.TryParse(str, out val)) return null;

            return val;
        }

        /// <summary>
        /// 将字符串类型的值转化为Decimal值
        /// </summary>
        /// <param name="obj">字符串类型的值</param>
        /// <returns></returns>
        public static decimal? ConvertToDecimalNULL(object obj)
        {
            decimal val;

            if (obj == null || obj.Equals(DBNull.Value)) return null;
            val = ConvertToDecimal(obj);

            return val;
        }

        /// <summary>
        /// 将Object类型的值转化为Double值
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static double ConvertToDouble(object obj, double defaultValue)
        {
            double val = defaultValue;
            try
            {
                if (obj == null)
                    val = defaultValue;
                else
                    val = Convert.ToDouble(obj);
            }
            catch
            {
                val = defaultValue;
            }

            return val;
        }

        /// <summary>
        /// 将Object类型的值转化为Decimal值
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <returns></returns>
        public static double ConvertToDouble(object obj)
        {
            double val;

            val = ConvertToDouble(obj, 0);

            return val;
        }

        /// <summary>
        /// 将Object类型的值转化为字符串,null转为空字符串
        /// </summary>
        /// <param name="obj">Object类型的值</param>
        /// <returns></returns>
        public static string ConvertToString(object obj)
        {
            string val = string.Empty;

            if (obj != null && !object.Equals(obj, null))
                val = obj.ToString();

            return val;
        }


        /// <summary>
        /// 将Decimal类型的值转化为xx,xxx.xxx,xx格式的字符串，主要用于财务数据的展示
        /// </summary>
        /// <param name="decValue">decimal类型的值</param>
        /// <returns>string</returns>
        public static string ConvertToMoneyString(object obj)
        {
            return ConvertToMoneyString(ConvertToDecimalNULL(obj));

        }
        /// <summary>
        /// 将Decimal类型的值转化为xx,xxx.xxx,xx格式的字符串，主要用于财务数据的展示
        /// </summary>
        /// <param name="decValue">decimal类型的值</param>
        /// <returns>string</returns>
        public static string ConvertToMoneyString(decimal? decValue)
        {
            if (decValue == null)
            {
                return "0.00";
            }

            return decValue.Value.ToString("#,##0.00");
        }


        /// <summary>
        /// 将Decimal类型的值小数点后保留2位的字符串，主要用于财务数据的展示
        /// </summary>
        /// <param name="decValue">decimal类型的值</param>
        /// <returns>string</returns>
        public static string DecimalToString(decimal? decValue)
        {
            string rtv = "";
            if (decValue == null)
            {
                return " ";
            }

            string decValueStr = decValue.ToString();
            string[] strArray = decValueStr.Split('.');
            if (strArray.Length > 1 && strArray[1].Length > 2)
            {
                rtv = strArray[0] + "." + strArray[1].Substring(0, 2);
            }
            else
            {
                rtv = decValueStr;
            }

            return rtv;
        }


        public static string ConvertDateTimeStringToString(Object origDate)
        {
            if (origDate == null)
            {
                return "";
            }
            try
            {
                DateTime dateNew = Convert.ToDateTime(origDate);
                return dateNew.ToShortDateString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 前台转化日期显示
        /// </summary>
        /// <remarks>wangpeixian</remarks>
        /// <param name="obj"></param>
        /// <param name="languageType">0:中文；1:英文</param>
        /// <returns></returns>
        public static string ConvertDate(object obj)
        {
            string val = string.Empty;
            DateTime date = Utility.ConvertToDateTime(obj);
            if (date != DateTime.MinValue)
            {
                val = date.ToString("yyyy年MM月dd日");
            }

            return val;
        }

        /// <summary>
        /// 将string与char数组拼接，并在指定位置插入分隔符号
        /// </summary>
        /// <param name="origStr">string类型的值</param>
        /// <param name="charArray">char[]类型的值</param>
        /// <param name="seperatePosition">int类型的值</param>
        /// <returns>string</returns>
        private static string ConcateCharsToStringWithSeperateForDecimal(string origStr, char[] charArray, int seperatePosition)
        {
            for (int i = 0; i < charArray.Length; i++)
            {
                if (i == seperatePosition)
                {
                    seperatePosition = seperatePosition + 3;
                    origStr = origStr + ",";
                }
                origStr = origStr + charArray[i].ToString();
            }
            return origStr;
        }

        /// <summary>
        /// 转换成bool数据类型，默认返回false
        /// </summary>
        /// <param name="obj">需要转换的对象</param>
        /// <returns>返回bool数据类型</returns>
        public static bool ConvertToBool(object obj)
        {
            return ConvertToBool(obj, false);
        }

        /// <summary>
        ///  转换成bool数据类型
        /// </summary>
        /// <param name="obj">需要转换的对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回bool数据类型</returns>
        public static bool ConvertToBool(object obj, bool defaultValue)
        {
            bool val;
            if (obj == null)
            {
                val = defaultValue;
            }
            else
            {
                try
                {
                    val = Convert.ToBoolean(obj);
                }
                catch
                {
                    val = defaultValue;
                }
            }

            return val;
        }


        public static bool ParseToBool(bool? boolVal)
        {
            if (boolVal == null)
            {
                return false;
            }
            else if (boolVal == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string FormatString(string str)
        {
            if (str == null)
            {
                return str;
            }

            return str.Trim();
        }
        /// <summary>
        /// 生成全大写无连接线的Guid串
        /// </summary>
        /// <returns>全大写无连接线的Guid串</returns>
        public static String GenerateGuid()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }
       
    }
}
