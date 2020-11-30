using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleDemo.Bussiness;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleDemo.Api.Controllers
{
    /// <summary>
    /// 实体操作模块
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private EntityDomain entityDomain = new EntityDomain();
        private readonly IHostingEnvironment _hostingEnvironment;

        public EntityController(IHostingEnvironment hostingEnvironment) 
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public string  CreateEntity(string entityName = null) 
        {
            if (entityName == null)
                return "参数为空";
            entityDomain.CreateEntity(entityName, _hostingEnvironment.ContentRootPath);
            return "执行成功";
        }
    }
}
