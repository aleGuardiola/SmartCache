using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SmartCache
{
    public class CacheDB
    {
        Type[] cachedTypes;
        Dictionary<Type, object> cachedValues;

        public CacheDB()
        {
            
        }

        protected void Initialize()
        {
            var type = this.GetType();
            cachedValues = new Dictionary<Type, object>();

            var properties = type.GetProperties();
            cachedTypes = new Type[properties.Length];

            var index = 0;
            foreach (var prop in properties)
            {
                var value = prop.GetValue(this);
                var GetTypeMethod = value.GetType().GetMethod("GetDataType", BindingFlags.Instance | BindingFlags.NonPublic);
                var t = GetTypeMethod.Invoke(value, new object[0]) as Type;

                cachedTypes[index] = t;
                cachedValues.Add(t, value);
                index++;
            }

            foreach (var c in cachedValues)
            {
                var value = c.Value;
                var GetTypeMethod = value.GetType().GetMethod("Initialize", BindingFlags.Instance | BindingFlags.NonPublic);
                GetTypeMethod.Invoke(value, new object[] { this });
            }
        }

        internal void UpdateCachedType(Type t, object value)
        {
            var cachedType = cachedValues[t];
            var type = cachedType.GetType();
            var AddMethod = type.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);

            AddMethod.Invoke(cachedType, new[] { value });
        }

        internal void RemoveCachedType(Type t, object value)
        {
            var cachedType = cachedValues[t];
            var type = cachedType.GetType();
            var RemoveMethod = type.GetMethod("Remove");

            RemoveMethod.Invoke(cachedType, new[] { value });
        }

        internal IEnumerable<Type> GetCachedTypes()
        {
            return cachedTypes;
        }
    }
}
