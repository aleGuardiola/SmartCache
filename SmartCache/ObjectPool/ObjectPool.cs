using System;
using System.Collections.Concurrent;

namespace ObjectPool
{
    public class ObjectPool<T>
    {
        ConcurrentBag<T> bag;
        Func<T> objectCreator;
        int _capacity;

        //object pool with empty constructor
        public ObjectPool(int capacity = 0)
        {
            //create default instance using activator with public ctr
            objectCreator = () => Activator.CreateInstance<T>();
        }

        //object pool with custom creator
        public ObjectPool(Func<T> creator, int capacity = 0)
        {
            bag = new ConcurrentBag<T>();
            objectCreator = creator;
            _capacity = capacity;
        }

        T Get()
        {
            T item;
            if (bag.TryTake(out item)) return item;
            return objectCreator();
        }

    }
}
