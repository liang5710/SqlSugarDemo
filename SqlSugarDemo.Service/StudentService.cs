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
        private IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository) 
        {
            this._studentRepository = studentRepository;
        }
      
        public int Sum(int i, int j)
        {
            return _studentRepository.Sum(i, j);
        }

        public int Add(Student student) 
        {
            return _studentRepository.Add(student);
        }
        public bool Delete(Student student) 
        {
            return _studentRepository.Delete(student);
        }
        public bool Update(Student student) 
        {
            return _studentRepository.Update(student);
        }
        public List<Student> Query(Expression<Func<Student, bool>> whereExpression) 
        {
            return _studentRepository.Query(whereExpression);
        }
    }
}
