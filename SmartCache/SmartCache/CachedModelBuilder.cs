using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SmartCache
{
    public class CachedModelBuilder<T>
    {
        object cachedModel;

        internal CachedModelBuilder()
        {

        }

        public CachedModelBuilder<T> SetPrimaryKey<Key>(string primaryKey)
        {
            cachedModel = new CachedModel<T, Key>(primaryKey);
            return this;
        }

        public CachedModelBuilder<T> SetPrimaryKey<Key>( Expression< Func<T, Key> > primaryKeySelector)
        {
            cachedModel = new CachedModel<T, Key>(Helper.GetPropertyName<T>(primaryKeySelector.Body));
            return this;
        }

        public CachedModelBuilder<T> AddUniqueIndex<Key>(string propertyName)
        {
            if (cachedModel == null)
                throw new InvalidOperationException("Unique Index tried to set before primary key");

            var method = cachedModel.GetType().GetMethod("AddUniqueIndex", BindingFlags.Instance | BindingFlags.NonPublic);

            var generic = method.MakeGenericMethod(typeof(Key));
            generic.Invoke(cachedModel, new[] { propertyName });
            return this;
        }


        public CachedModelBuilder<T> AddUniqueIndex<Key>(Expression<Func<T, Key>> indexSelector)
        {
            var propertyName = Helper.GetPropertyName<T>(indexSelector.Body);

            AddUniqueIndex<Key>(propertyName);

            return this;
        }

        public CachedModel<T, Key> Build<Key>()
        {
            return (CachedModel<T, Key>)cachedModel;
        }

    }
}
