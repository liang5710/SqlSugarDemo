using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlSugarDemo.Api.Common
{
    public enum ReturnCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [JsonProperty("1000")]
        Success = 1000,

        /// <summary>
        /// 登录超时
        /// </summary>
        [JsonProperty("2000")]
        NeedLogin = 2000,

        /// <summary>
        /// 程序异常
        /// </summary>
        [JsonProperty("3000")]
        Exception = 3000,

        /// <summary>
        /// 系统错误
        /// </summary>
        [JsonProperty("4000")]
        SysError = 4000

    }
}
