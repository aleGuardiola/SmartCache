using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SmartCache
{
    public class CacheDB
    {
        string[] cachedTypes;
        Dictionary<string, object> cachedValues;

        public CacheDB()
        {
            var type = this.GetType();
            cachedValues = new Dictionary<string, object>();

            var properties = type.GetProperties();
            cachedTypes = properties.Select(t => t.PropertyType.Name).ToArray();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(prop);
                cachedValues.Add(prop.Name, value);
            }
        }

        internal void UpdateCachedType(string name, object value)
        {
            var cachedType = cachedValues[name];
            var type = cachedTypes.GetType();
            var AddMethod = type.GetMethod("Add");

            AddMethod.Invoke(cachedType, new[] { value });
        }

        internal void RemoveCachedType(string name, object value)
        {
            var cachedType = cachedValues[name];
            var type = cachedTypes.GetType();
            var RemoveMethod = type.GetMethod("Remove");

            RemoveMethod.Invoke(cachedType, new[] { value });
        }

        internal IEnumerable<string> GetCachedTypes()
        {
            return cachedTypes;
        }
    }
}
