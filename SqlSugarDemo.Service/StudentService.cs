using SqlSugarDemo.IRepository;
using SqlSugarDemo.IService;
using SqlSugarDemo.Repository;
using System;

namespace SqlSugarDemo.Service
{
    public class StudentService : IStudentService
    {
        public IStudentRepository dal = new StudentRepository();
        public int Sum(int i, int j)
        {
            return dal.Sum(i, j);
        }
    }
}
