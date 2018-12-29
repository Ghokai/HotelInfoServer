using System;

namespace HotelInfoServer.AssemblyLoaders
{
    //interface for dependency injection
    public interface IDynamicLoader
        {
            object CreateInstanceFromString(string typeName, params object[] args);
            Type GetTypeFromName(string typeName);
            T[] LoadAssembliesDynamicallyFromConfig<T>(string parameterName, string delimiter);
        }
}
