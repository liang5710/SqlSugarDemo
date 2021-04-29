using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugarDemo.IService;
using SqlSugarDemo.Model;
using SqlSugarDemo.Service;

namespace SqlSugarDemo.Api.Controllers
{
    /// <summary>
    /// 测试示例
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }
        /// <summary>
        /// 测试用例
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [HttpGet]
        public int Get(int i, int j)
        {
            return _studentService.Sum(i, j);
        }

        /// <summary>
        /// 新增学员
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public int Add(Student student)
        {
            return _studentService.Add(student);
        }

        /// <summary>
        /// 获取学员信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public List<Student> Get(int id)
        {
            return _studentService.Query(d => d.Id == id);
        }
    }
}
