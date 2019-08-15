using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Services
{
    public static class UnixDateTimeService
    {
        //Unix起始時間
        private static DateTime BaseTime = new DateTime(1970, 1, 1);

        /// <summary>
        /// 轉換UNIX時戳格式為C#的DateTime格式
        /// </summary>
        /// <param name="timeStamp">UNIX時戳</param>
        /// <returns>C#的DateTme格式</returns>
        public static DateTime Get(long timeStamp)
        {
            return BaseTime.AddTicks((timeStamp + 8 * 60 * 60) * 10000000);
        }

        /// <summary>
        /// 轉換UNIX時戳格式為C#的DateTime格式
        /// </summary>
        /// <param name="timeStamp">UNIX時戳</param>
        /// <returns>C#的DateTme格式</returns>
        public static DateTime Get(string timeStamp)
        {
            return Get(long.Parse(timeStamp));
        }

        /// <summary>
        /// 轉換C#的DateTime格式為UNIX時戳格式
        /// </summary>
        /// <param name="dateTime">DateTime物件</param>
        /// <returns>UNIX時戳格式</returns>
        public static long GetUNIX(DateTime dateTime)
        {
            return (dateTime.Ticks - BaseTime.Ticks) / 10000000 - 8 * 60 * 60;
        }
    }
}
