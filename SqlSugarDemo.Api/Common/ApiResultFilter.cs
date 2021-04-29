using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlSugarDemo.Api.Common
{
    public class ApiResultFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Action执行完成，返回结果处理
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null) 
            {
                ObjectResult result = context.Result as ObjectResult;
                if (result != null) 
                {
                    ResultState<object> resultState = new ResultState<object>();
                    resultState.Success(result.Value);
                    ObjectResult objectResult = new ObjectResult(resultState);
                    context.Result = objectResult;
                }
            }
            base.OnActionExecuted(context);
        }
    }
}
