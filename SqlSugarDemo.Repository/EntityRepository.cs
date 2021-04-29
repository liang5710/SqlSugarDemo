using SqlSugar;
using SqlSugarDemo.IRepository;
using SqlSugarDemo.ORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlSugarDemo.Repository
{
    public class EntityRepository : IEntityRepository
    {

        private DbContext context;
        private SqlSugarClient db;
        private readonly ITransaction _transaction;


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

        public EntityRepository(ITransaction transaction)
        {
            _transaction = transaction;
            DbContext.Init(BaseDBConfig.ConnectionString);
            context = DbContext.GetDbContext();
            db = _transaction.GetDbClient();
        }
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool CreateEntity(string entityName, string filePath)
        {
            db.DbFirst.IsCreateAttribute().Where(entityName).CreateClassFile(filePath);
            return true;
        }
    }
}
