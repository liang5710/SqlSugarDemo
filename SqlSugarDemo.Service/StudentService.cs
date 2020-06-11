using SqlSugarDemo.IRepository;
using SqlSugarDemo.IService;
using SqlSugarDemo.Model;
using SqlSugarDemo.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SqlSugarDemo.Service
{
    public class StudentService : IStudentService
    {
        public IStudentRepository dal = new StudentRepository();
        public int Sum(int i, int j)
        {
            return dal.Sum(i, j);
        }

        public int Add(Student student) 
        {
            return dal.Add(student);
        }
        public bool Delete(Student student) 
        {
            return dal.Delete(student);
        }
        public bool Update(Student student) 
        {
            return dal.Update(student);
        }
        public List<Student> Query(Expression<Func<Student, bool>> whereExpression) 
        {
            return dal.Query(whereExpression);
        }
    }
}
