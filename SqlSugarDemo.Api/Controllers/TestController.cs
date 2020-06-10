using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugarDemo.IService;
using SqlSugarDemo.Service;

namespace SqlSugarDemo.Api.Controllers
{
    /// <summary>
    /// 测试示例
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public int Get(int i, int j) 
        {
            IStudentService studentService = new StudentService();
            return studentService.Sum(i, j);
        }
    }
}
