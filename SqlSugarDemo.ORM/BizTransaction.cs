using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlSugarDemo.ORM
{
    public class BizTransaction : ITransaction
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        public BizTransaction(ISqlSugarClient sqlSugarClient) 
        {
            this._sqlSugarClient = sqlSugarClient;
        }

        /// <summary>
        /// 获取DB,保证唯一项
        /// </summary>
        /// <returns></returns>
        public SqlSugarClient GetDbClient()
        {
            return _sqlSugarClient as SqlSugarClient;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            try
            {
                GetDbClient().BeginTran();
            }
            catch (Exception ex) 
            {
                GetDbClient().RollbackTran();
                throw ex;
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            GetDbClient().CommitTran();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            GetDbClient().RollbackTran();
        }
    }
}
