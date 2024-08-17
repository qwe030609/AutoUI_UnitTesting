using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    public class MemoryCache<TKey, TValue>
    {
        private readonly object Lock = new object();
        private readonly IDictionary<TKey, TValue> Cache = new Dictionary<TKey, TValue>();

        //// Constructor to initialize the cache with an existing dictionary
        //public MemoryCache(IDictionary<TKey, TValue> initialCache)
        //{
        //    lock (Lock)
        //    {
        //        foreach (var kvp in initialCache)
        //        {
        //            Cache.Add(kvp.Key, kvp.Value);
        //        }
        //    }
        //}

        /// <summary>
        /// Function for setting the cache's value when the key isn't found.
        /// </summary>
        //public Func<TKey, TValue> Set { get; set; }

        /// <summary>
        /// Retrieves the value marked by the indicated key in a thread-safe way.
        /// If the item is not found, uses Set to retrieve it and store it in the cache before returning the value.
        /// </summary>
        public TValue Get(TKey key)
        {
            lock (Lock)
            {
                //if (Cache.TryGetValue(key, out var value) == false)
                //{
                //value = Set(key);
                //Cache.Add(key, value);
                //}
                Cache.TryGetValue(key, out var value);
                return value;
            }
        }

        /// <summary>
        /// Add value in a thread-safe way.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void Add(TKey key, TValue value)
        {
            lock (Lock)
            {
                if (Cache.ContainsKey(key))
                    Cache[key] = value;
                else
                    Cache.Add(key, value);
            }
        }

        /// <summary>
        /// Gets the size of the cache.
        /// </summary>
        public int CacheSize => Cache.Count;

        /// <summary>
        /// Gets the cache data itself.
        /// </summary>
        public IDictionary<TKey, TValue> CacheData => Cache;

        /// <summary>
        /// Gets the keys in the cache.
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get
            {
                lock (Lock)
                {
                    return Cache.Keys.ToList();
                }
            }
        }

        /// <summary>
        /// Gets the values in the cache.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                lock (Lock)
                {
                    return Cache.Values.ToList();
                }
            }
        }

        /// <summary>
        /// Checks if the cache is empty.
        /// </summary>
        /// <returns>True if the cache is empty; otherwise, false.</returns>
        public bool IsEmpty()
        {
            lock (Lock)
            {
                return Cache.Count == 0;
            }
        }

        /// <summary>
        /// Clears all values in the cache.
        /// </summary>
        public void Clear()
        {
            lock (Lock)
            {
                Cache.Clear();
            }
        }
    }
}
