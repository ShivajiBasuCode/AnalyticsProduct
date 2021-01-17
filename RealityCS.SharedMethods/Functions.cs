using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RealityCS.SharedMethods
{
    public static class Functions
    {
        /// <summary>
        /// RemoveSpaceFromStrig
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpaceFromStrig(string str)
        {
            return Regex.Replace(str, @"[^\w\.@-]", string.Empty, RegexOptions.Multiline).TrimEnd();            
        }
        /// <summary>
        /// RemoveSpecialCharactersFromString
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharactersFromString(string str)
        {
            //https://www.c-sharpcorner.com/blogs/replace-special-characters-from-string-using-regex1
            //return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
            return Regex.Replace(str, @"[^0-9a-zA-Z]+", "", RegexOptions.Compiled);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        /// <summary>
        /// dd/MM/YYYY Format
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ConvertStringToDate(string date)
        {

            string[] arrdate = string.IsNullOrEmpty(date) == false ? date.Split('/') : new string[] { };
            string formateddate = string.Empty;
            if (arrdate.Length == 3)
            {
                string tmpdate = string.Empty;
                string tmpmonth = string.Empty;
                //tmpdate = arrdate[0].Length == 1 ? "0" + arrdate[0] : arrdate[0];
                tmpdate = arrdate[0];
                tmpmonth = arrdate[1].Length == 1 ? "0" + arrdate[1] : arrdate[1];
                formateddate = tmpmonth + "/" + tmpdate + "/" + arrdate[2];
            }
            if (!string.IsNullOrEmpty(formateddate))
            {
                // DateTime odateTime = DateTime.ParseExact(formateddate, "MM/dd/yyyy", CultureInfo.InvariantCulture).Date;
                DateTime odateTime = Convert.ToDateTime(formateddate, CultureInfo.InvariantCulture);
                return odateTime;
            }
            else
            {
                return DateTime.Now;
            }


        }
        /// <summary>
        /// ConvertDateToString
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ConvertDateToString(DateTime? date)
        {

            return date.HasValue ? date.Value.Date.ToString("dd/MM/yyyy") : string.Empty;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static TimeSpan ConvertStringToTime(string time)
        {
            string text = time;
            string[] formats = { "hhmm", "hmm", @"hh\:mm", @"h\:mm\:ss", @"h:mm", @"h:mm tt" };

            var success = DateTime.TryParseExact(text, formats, CultureInfo.CurrentCulture,
                DateTimeStyles.None, out var value);

            return value.TimeOfDay;
        }
        /// <summary>
        /// ConvertTimeToString
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ConvertTimeToString(TimeSpan? time)
        {
            //string text = time;
            //string[] formats = { "hhmm", "hmm", @"hh\:mm", @"h\:mm\:ss", @"h:mm", @"h:mm tt" };
            string strtime = time.HasValue ? DateTime.Today.Add((TimeSpan)time).ToString("hh:mm tt") : string.Empty;
            //var success = DateTime.TryParseExact(text, formats, CultureInfo.CurrentCulture,
            //    DateTimeStyles.None, out var value);

            //return value.TimeOfDay;
            return strtime;
        }
        /// <summary>
        /// Convert System Time To Custom Time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static TimeSpan ConvertSystemTimeToCustomTime(TimeSpan time)
        {
            string text = time.ToString(@"h\:mm\:ss");

            var success = TimeSpan.TryParse(text, out TimeSpan value);

            return value;
        }
        /// <summary>
        /// Random password with given characters
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";//!@#$%^&*?_-
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }


    }
}
