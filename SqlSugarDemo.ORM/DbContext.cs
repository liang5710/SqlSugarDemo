using SqlSugar;
using System;

namespace SqlSugarDemo.ORM
{
    public class DbContext
    {
        private static string _connectionString;
        private static DbType _dbType;
        private SqlSugarClient _db;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        /// <summary>
        /// 数据连接对象
        /// </summary>
        public SqlSugarClient Db
        {
            get { return _db; }
            set { _db = value; }
        }

        /// <summary>
        /// 数据库上下文实例（自动关闭连接）
        /// </summary>
        public static DbContext Context
        {
            get
            {
                return new DbContext();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private DbContext()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentNullException("数据连接字符串为空");
            _db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString=_connectionString,
                DbType=_dbType,
                IsAutoCloseConnection=true,
                IsShardSameThread=true
            });
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IsAutoCloseConnection">是否自动关闭</param>
        private DbContext(bool IsAutoCloseConnection)
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentNullException("数据连接字符串为空");
            _db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = _connectionString,
                DbType = _dbType,
                IsAutoCloseConnection = IsAutoCloseConnection,
                IsShardSameThread = true
            });
        }

        #region 实例方法
        /// <summary>
        /// 获取数据库处理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public SimpleClient<T> GetEntityDB<T>() where T : class, new() 
        {
            return new SimpleClient<T>(_db);
        }

        public SimpleClient<T> GetEntityDB<T>(SqlSugarClient db) where T : class, new() 
        {
            return new SimpleClient<T>(db);
        }
        #endregion
    }
}
