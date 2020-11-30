using ExampleDemo.IService;
using SqlSugarDemo.ORM;
using System;

namespace ExampleDemo.Service
{
    public class EntityService : SqlSugarBase, IEntity
    {
        
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool CreateEntity(string entityName, string filePath)
        {
            try
            {
                DB.DbFirst.IsCreateAttribute().Where(entityName).CreateClassFile(filePath);
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
