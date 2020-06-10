using SqlSugarDemo.IRepository;
using System;

namespace SqlSugarDemo.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public int Sum(int i, int j)
        {
            return i + j;
        }
    }
}
