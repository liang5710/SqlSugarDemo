
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlSugarDemo.ORM
{
    public interface ITransaction
    {
        SqlSugarClient  GetDbClient();
        void BeginTran();
        void Commit();
        void Rollback();
    }
}
