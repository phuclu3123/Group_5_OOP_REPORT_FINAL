using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Logistics.Core.DataAccess.Repositories
{
    internal sealed class LogisticsSerializationBinder : ISerializationBinder
    {
        public static readonly LogisticsSerializationBinder Instance = new LogisticsSerializationBinder();

        private const string CoreNamespacePrefix = "Logistics.Core.";
        private static readonly string CoreAssemblyName = typeof(LogisticsSerializationBinder).Assembly.GetName().Name ?? "Logistics.Core";

        private LogisticsSerializationBinder()
        {
        }

        public Type BindToType(string? assemblyName, string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName) ||
                !typeName.StartsWith(CoreNamespacePrefix, StringComparison.Ordinal))
            {
                throw new JsonSerializationException("JSON type is not allowed: " + typeName);
            }

            string requestedAssembly = string.IsNullOrWhiteSpace(assemblyName) ? CoreAssemblyName : assemblyName.Split(',')[0].Trim();
            if (!string.Equals(requestedAssembly, CoreAssemblyName, StringComparison.Ordinal))
            {
                throw new JsonSerializationException("JSON assembly is not allowed: " + requestedAssembly);
            }

            Type? type = Type.GetType(typeName + ", " + CoreAssemblyName, false);
            if (type == null)
            {
                throw new JsonSerializationException("Cannot resolve JSON type: " + typeName);
            }

            return type;
        }

        public void BindToName(Type serializedType, out string? assemblyName, out string? typeName)
        {
            assemblyName = serializedType.Assembly.GetName().Name;
            typeName = serializedType.FullName;
        }
    }
}
