
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace z8l_intranet_be.Helper
{
    public static class Common
    {
        public const string phone_pattern = @"^\(?([0-9]{3})\)?([0-9]{3})?([0-9]{4})$";
        public const string DEFAULT_PASSWORD = "abc123";
        public const string CURRENT_USER = "CURRENT_USER";
        private static Random random = new Random();

        public struct ResponseHeaders
        {
            public const string TOKEN_EXPIRED = "Token-Expired";
        }

        public enum ExceptionType
        {
            DEFAULT = 0,
            NOT_FOUND = 1,
            NO_PERMISSION = 2,
            BAD_REQUEST = 3,
            UNAUTHORIZED = 4
        }

        public struct TIMEZONE_ID
        {
            public const string GMT7 = "SE Asia Standard Time";

            public const string GMT0 = "Greenwich Standard Time";
        }

        public struct STANDAR_NAME
        {
            public const string GMT7 = "Indochina Time";

        }

        public enum TIME_UNIX
        {
            MINUTE = 60,
            HOUR = MINUTE * 60,
            SEVEN_HOUR = HOUR * 7,
            DAY = HOUR * 24,
            WEEK = DAY * 7,
        }

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string GetRandom(int from, int to)
        {
            Random rand = new();
            string strGenerated = rand.Next(from, to).ToString();
            return strGenerated;
        }

        public static string GeneratePassword(this string inValue)
        {
            string result = "";
            byte[] data;
            MD5 hashMd5 = new MD5CryptoServiceProvider();
            data = hashMd5.ComputeHash(Encoding.ASCII.GetBytes(inValue.ToCharArray()));

            for (int i = 0; i < data.Length; i++)
            {
                result += data[i].ToString("X2");
            }
            return result;
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new();
            MD5CryptoServiceProvider md5provider = new();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static bool IsPhoneNbr(string number)
        {
            if (number != null) return Regex.IsMatch(number, phone_pattern);
            else return false;
        }
        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
        public static DateTime UnixTimeStampToDateTimeGMT7(int unixTimeStamp)
        {
            DateTime dateTime = UnixTimeStampToDateTime(unixTimeStamp);
            return ConvertToGMT7(dateTime);
        }
        public static Int32 DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            DateTime localDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, TIMEZONE_ID.GMT0);
            Int32 unixTimestamp = (int)(localDateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            return unixTimestamp;
        }
        public static int GetStartWeek(int timeStamp)
        {
            TimeZone localZone = TimeZone.CurrentTimeZone;
            DateTime dt = Common.UnixTimeStampToDateTime(timeStamp);
            DateTime localDateTime = ConvertToGMT7(dt);
            dt = localDateTime.StartOfWeek(DayOfWeek.Monday);
            int _dt = 0;
            if (localZone.StandardName.Contains(STANDAR_NAME.GMT7))
            {
                _dt = DateTimeToUnixTimeStamp(dt);
            }
            else
            {
                _dt = DateTimeToUnixTimeStamp(dt) - (int)TIME_UNIX.SEVEN_HOUR;
            }
            return _dt;
        }


        public static int GetEndWeek(int timeStamp)
        {
            TimeZone localZone = TimeZone.CurrentTimeZone;
            DateTime dt = Common.UnixTimeStampToDateTime(timeStamp);
            DateTime localDateTime = ConvertToGMT7(dt);
            dt = localDateTime.StartOfWeek(DayOfWeek.Monday);
            dt = dt.AddDays(6);
            int _dt = 0;
            if (localZone.StandardName.Contains(STANDAR_NAME.GMT7))
            {
                _dt = DateTimeToUnixTimeStamp(dt) + (int)TIME_UNIX.DAY - 1;
            }
            else
            {
                _dt = DateTimeToUnixTimeStamp(dt) + (int)TIME_UNIX.DAY - 1 - (int)TIME_UNIX.SEVEN_HOUR;
            }
            return _dt;
        }
        public static int GetCurrentTimeUnix()
        {
            return Convert.ToInt32(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        }

        public static DateTime ConvertToGMT7(DateTime dateTime)
        {
            DateTime localDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, TIMEZONE_ID.GMT7);
            return localDateTime;
        }
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
        public static int GetStartDay(DateTime dateTime)
        {
            DateTime dt = StartOfDay(dateTime);
            int _dt = DateTimeToUnixTimeStamp(dt) - (int)TIME_UNIX.SEVEN_HOUR;
            return _dt;
        }
        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, DateTimeKind.Utc);
        }


        public static List<int> ConvertStringToList(string str, string key)
        {
            string[] _listId = new string[] { };
            if (!string.IsNullOrEmpty(str))
            {
                _listId = str.Split(key);
            }
            return _listId.Select(x => Int32.Parse(x.Trim())).ToList();
        }
        public static string ConvertListToString(List<string> list, string key)
        {
            return string.Join(key, list);
        }

        public static string FormatPhoneNumber(string phoneNumber)
        {
            bool isZeroFirst = phoneNumber.StartsWith("0");
            return isZeroFirst ? phoneNumber : phoneNumber.Substring(0);
        }

    }
}
