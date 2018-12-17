using AVLTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmartCache
{
    public abstract class CachedType<T>
    {
        bool isInitialized = false;
        object isInitializedLock = new object();

        protected Type Type;
        protected CacheDB Db;

        protected Type[] TypesCached;

        internal CachedType()
        {
            Type = typeof(T);            
        }

        internal Type GetDataType()
        {
            return Type;
        }

        protected void ThrowIfNotInitialized()
        {
            lock(isInitializedLock)
            {
                if (!isInitialized)
                    throw new InvalidOperationException("No CacheDB is attached");
            }
        }

        internal void Initialize(CacheDB db)
        {
            lock(isInitializedLock)
            {
                if (isInitialized)
                    throw new InvalidOperationException("Cache Db is already attached");

                Db = db;

                var enumerable = db.GetCachedTypes();

                var properties = Type.GetProperties();

                var res = enumerable.Where(s => properties.Any(p => p.PropertyType == s||implementsIEnumerable(p.PropertyType, s)));
                TypesCached = res.ToArray();

                isInitialized = true;
            }
        }

        internal protected bool implementsIEnumerable(Type type, Type genericType)
        {
            var enumerables = type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            if (enumerables.Count() == 0)
                return false;

            var enumerable = enumerables.First();
            return enumerable.GenericTypeArguments[0] == genericType;
        }

        public virtual IEnumerable<T> GetItems()
        {
            ThrowIfNotInitialized();
            return null;
        }

        public virtual void Add(T item)
        {
            ThrowIfNotInitialized();
        }

        public virtual void Remove(T item)
        {
            ThrowIfNotInitialized();
        }

        public virtual void Clear()
        {
            ThrowIfNotInitialized();
        }
    }
}
