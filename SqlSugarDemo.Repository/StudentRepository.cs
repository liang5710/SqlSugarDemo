using SqlSugar;
using SqlSugarDemo.IRepository;
using SqlSugarDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SqlSugarDemo.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<Student> entityDB;

        internal SqlSugarClient Db
        {
            get { return db; }
            private set { db = value; }
        }
        public DbContext Context
        {
            get { return context; }
            set { context = value; }
        }

        public StudentRepository() 
        {
            DbContext.Init(BaseDBConfig)
        }

        public int Sum(int i, int j)
        {
            return i + j;
        }

        public int Add(Student student)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Student student)
        {
            throw new NotImplementedException();
        }

        public List<Student> Query(Expression<Func<Student, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public bool Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
