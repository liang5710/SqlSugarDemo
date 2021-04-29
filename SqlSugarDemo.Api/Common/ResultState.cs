using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlSugarDemo.Api.Common
{
    /// <summary>
    /// 返回结果对象
    /// ReturnObject ResultState 
    /// 默认ReturnCode为成功,Message为成功.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultState<T>
    {
        ReturnCode code = ReturnCode.Success;
        T result = default(T);
        string message = "操作成功";

        /// <summary>
        /// 执行结果
        /// </summary>
        public ReturnCode Code { get => code; set => code = value; }

        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message { get => message; set => message = value; }

        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get => result; set => result = value; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="result">返回结果</param>
        /// <param name="msg">提示消息</param>
        public void Success(T result, string msg = "执行成功") 
        {
            this.code = ReturnCode.Success;
            this.Message = msg;
            this.result = result;
        }

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="msg">提示消息</param>
        /// <param name="code"></param>
        public void Error(string msg, ReturnCode code = ReturnCode.Exception) 
        {
            this.code = code;
            this.Message = msg;
        }
    }
}
