using System;
using System.Collections.Generic;
using System.Text;

namespace SqlSugarDemo.IRepository
{
    public interface IEntityRepository
    {
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        bool CreateEntity(string entityName, string filePath);
    }
}
