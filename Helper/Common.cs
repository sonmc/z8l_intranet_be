
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

        public static string GenerateJwtToken(int userId, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public static int GetRefreshTokenExpiryTime()
        {
            var refreshTokenExpiryDate = DateTime.Now.AddDays(7);
            return Common.DateTimeToUnixTimeStamp(refreshTokenExpiryDate);
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
        public static string ContentMail(string organName, string email, string password)
        {

            string template = string.Format("<div style='padding:0px 33px 0 0px;width:437px;'>"
                 + "<img src='{3}'/>"
                 + "<div style='padding-left:30px'>"
                 + "<h2> Thông tin đăng nhập </h2>"
                 + "<p> Hệ thống LMS - {0} </p>"
                 + "<hr/>"
                 + "<p> Tên đăng nhập: {1} </p>"
                 + "<p> Mật khẩu: {2} </p>"
                 //+ "<p> Để đảm bảo sự bảo mật của tài khoản, đổi mật khẩu<a href='goole.com.vn'> tại đây</a></p>"
                 + "<div style='background:#F2CE5F;width:66%;display:flex;justify-content:center;text-decoration:none;border-radius:4px;'>"
                 + "<a href ='https://lms.stg.kidsenglish.vn/' style='font-size:18px;font-weight:600;padding:11px;font-family:SYSTEM-UI;text-decoration:none;'>Đăng nhâp vào hệ thống LMS</a>"
                 + "</div>"
                 + "<p style='font-size:17px;font-weight:600;'>Bạn cần hỗ trợ ?</p>"
                 + "<p style='font-size:17px;'> Email hỗ trợ: <a href='mailto:hotro@vietec.com.vn'> hotro@vietec.com.vn </a></p>"
                 + "</div>"
                 + "</div>", organName, email, password, "https://stg.static.kidsenglish.vn/files/logo_mail.png");
            return template;
        }

        public static string Encode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        public static string Decode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        public static string GenerateCode(string str, int idx)
        {
            //string[] arr = (string[])str.Split(' ').Reverse();
            var index = (10000000 + idx).ToString();
            var result = "Ecl" + index;
            return result;
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
