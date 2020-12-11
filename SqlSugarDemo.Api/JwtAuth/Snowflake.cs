using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlSugarDemo.Api.JwtAuth
{
    public class Snowflake
    {
        private static long workerId;
        private static long twepoch = 687888001020L;
        private static long sequence = 0L;
        private static int workerIdBits = 4;
        public static long maxWorkerId = -1L ^ -1L << workerIdBits;
        private static int sequenceBits = 10;
        private static int workerIdShift = sequenceBits;
        private static int timestampLeftShift = sequenceBits + workerIdBits;
        public static long sequenceMask = -1L ^ -1L << sequenceBits;
        private long lastTimestamp = -1L;

        /// <summary>
        /// 机器码
        /// </summary>
        /// <param name="workerId"></param>
        public Snowflake(long workerId)
        {
            if (workerId > maxWorkerId || workerId < 0)
            {
                throw new Exception(string.Format("算法{0}对应ID不能超过最大时间戳ID或者不能小于 0 ", workerId));
            }
            Snowflake.workerId = workerId;
        }

        public long NextId()
        {
            lock (this)
            {
                long timestamp = timeGen();
                if (this.lastTimestamp == timestamp)
                {
                    Snowflake.sequence = (Snowflake.sequence + 1) & Snowflake.sequenceMask;
                    if (Snowflake.sequence == 0)
                    {
                        timestamp = tillNextMillis(this.lastTimestamp);
                    }
                }
                else
                {
                    Snowflake.sequence = 0;
                }
                if (timestamp < lastTimestamp)
                {
                    throw new Exception(string.Format("时间戳小于之前。拒绝生成关于{0}时间戳毫秒对应的id",
                        this.lastTimestamp - timestamp));
                }
                this.lastTimestamp = timestamp;
                long NextId = (timestamp - twepoch << timestampLeftShift) | Snowflake.workerId << Snowflake.workerIdShift | Snowflake.sequence;
                return NextId;
            }
        }

        /// <summary>
        /// 获取下一微秒时间戳
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private long tillNextMillis(long lastTimestamp)
        {
            long timestamp = timeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = timeGen();
            }
            return timestamp;
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns></returns>
        private long timeGen()
        {
            return (long)(DateTime.UtcNow - new DateTime(1770, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}
