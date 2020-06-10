using SqlSugar;
using SqlSugarDemo.IRepository;
using SqlSugarDemo.Model;
using SqlSugarDemo.ORM;
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
            DbContext.Init(BaseDBConfig.ConnectionString);
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<Student>(db);
        }

        public int Sum(int i, int j)
        {
            return i + j;
        }

        public int Add(Student student)
        {
            var i = db.Insertable(student).ExecuteReturnBigIdentity();
            return i.ObjToInt();
        }

        public bool Delete(Student student)
        {
            var i = db.Deleteable(student).ExecuteCommand();
            return i>0;
        }

        public List<Student> Query()
        {
            return entityDB.GetList();
        }

        public bool Update(Student student)
        {
            var i = db.Updateable(student).ExecuteCommand();
            return i > 0;
        }
    }
}
