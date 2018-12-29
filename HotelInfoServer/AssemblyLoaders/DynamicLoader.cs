using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HotelInfoServer.AssemblyLoaders
{
    public class DynamicLoader : IDynamicLoader
    {
        private IConfiguration _configuration;

        public DynamicLoader(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        //initilalizes generic array from reading configuration 
        public T[] LoadAssembliesDynamicallyFromConfig<T>(string parameterName, string delimiter)
        {
            List<T> assemblies = new List<T>();
            //read types from configuration with name
            var dynamicObjectNamesStr = this._configuration[parameterName];
            //parse string for multiple types
            var dynamicObjectNamesArray = dynamicObjectNamesStr.Split(delimiter);

            //create instances of  types
            foreach (string assemblyName in dynamicObjectNamesArray)
            {
                T x = (T)this.CreateInstanceFromString(assemblyName, null);
                if (x != null)
                {
                    assemblies.Add(x);
                }
            }
            return assemblies.ToArray();
        }
       
        public object CreateInstanceFromString(string typeName, params object[] args)
        {
            object instance = null;
            Type type = null;

            try
            {
                type = GetTypeFromName(typeName);
                if (type == null)
                    return null;
                //create instance with args
                instance = Activator.CreateInstance(type, args);
            }
            catch
            {
                return null;
            }
            if (instance==null)
            {
                throw new Exception("Instance can not created from type");
            }
            return instance;
        }

        public Type GetTypeFromName(string typeName)
        {
            Type type = null;

            // Let default name binding find it
            type = Type.GetType(typeName, false);
            if (type != null)
                return type;

            // look through assembly list
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // try to find manually
            foreach (Assembly asm in assemblies)
            {
                type = asm.GetType(typeName, false);

                if (type != null)
                    break;
            }
            if (type == null)
            {
                throw new Exception("Type could not find at run time");
            }
            return type;
        }
    }
}
