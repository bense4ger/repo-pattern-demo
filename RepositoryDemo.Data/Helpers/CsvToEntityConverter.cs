using System;

namespace RepositoryDemo.Data.Helpers
{
    internal static class CsvToEntityConverter
    {
        public static TEntity ToEntity<TEntity>(this string[] csvData, string[] headers)
        {
            var instance = Activator.CreateInstance<TEntity>();
            var props = typeof(TEntity).GetProperties();

            foreach (var prop in props)
            {
                var ix = Array.IndexOf(headers, prop.Name);
                if (ix == -1) continue;

                var propType = prop.PropertyType;
                var converted = propType == typeof(Guid)
                                    ? Guid.Parse(csvData[ix])
                                    : Convert.ChangeType(csvData[ix], propType);
                
                prop.SetValue(instance, converted);
            }
            
            return instance;
        }
    }
}