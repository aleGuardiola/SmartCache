using AVLTree;
using System;
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

        protected string[] TypesCached;

        public CachedType()
        {
            Type = typeof(T);            
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

                var res = enumerable.Where(s => properties.Any(p => p.PropertyType.Name == s));
                TypesCached = res.ToArray();
            }
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

    public class DictionaryCachedType<Key, T> : CachedType<T>
    {

        protected PropertyInfo PrimaryKeyInfo;
        protected AVLTree<Key, T> PrimaryKeyTree;

        public DictionaryCachedType(string primaryKey)
        {
            PrimaryKeyInfo = Type.GetProperty(primaryKey);
            PrimaryKeyTree = new AVLTree<Key, T>();
        }

        T this[Key index]
        {
            get
            {
                T result;
                var exist = TryToGet(index, out result);
                if (!exist)
                    throw new KeyNotFoundException($"No object found with primary key{result}");

                return result;
            }
        }

        public bool TryToGet(Key key, out T result)
        {
            return PrimaryKeyTree.TryGetValue(key, out result);
        }

        public override void Add(T item)
        {
            base.Add(item);

            var value = (Key)PrimaryKeyInfo.GetValue(item);
            
            foreach(var cached in TypesCached)
            {
                var properties = Type.GetProperties().Where(p=>p.PropertyType.Name == cached);

                foreach(var p in properties)
                {
                    var val = p.GetValue(Type);
                    Db.UpdateCachedType(cached, val);
                }                  
            }
        }
    }

}
