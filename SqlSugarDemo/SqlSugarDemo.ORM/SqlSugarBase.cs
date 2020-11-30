using SqlSugar;
using System;

namespace SqlSugarDemo.ORM
{
    public class SqlSugarBase
    {
        public static string _connectionString { get; set; }

        public static SqlSugarClient DB 
        {
            get => new SqlSugarClient(new ConnectionConfig() 
                {
                    ConnectionString= _connectionString,
                    DbType=DbType.MySql,
                    IsAutoCloseConnection=true,
                    InitKeyType=InitKeyType.SystemTable,
                    IsShardSameThread=true
            });
        }
    }
}
