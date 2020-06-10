using System;

namespace SqlSugarDemo.Model
{
    /// <summary>
    /// 学生类
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 学生号
        /// </summary>
        public int StuId { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        public int CourseId { get; set; }
    }
}
