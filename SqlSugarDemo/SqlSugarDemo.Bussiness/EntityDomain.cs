using ExampleDemo.IService;
using ExampleDemo.Service;
using System;

namespace ExampleDemo.Bussiness
{
    public class EntityDomain
    {
        public IEntity iEntity = new EntityService();

        public bool CreateEntity(string entityName, string filePath) 
        {
            string[] arr = filePath.Split('\\');
            string baseFileProvider = "";
            for (int i = 0; i < arr.Length - 1; i++) 
            {
                baseFileProvider += arr[i];
                baseFileProvider += "\\";
            }
            string path = baseFileProvider + "ExampleDemo.Model";

            return iEntity.CreateEntity(entityName,path);
        }
    }
}
