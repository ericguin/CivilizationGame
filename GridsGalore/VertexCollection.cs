using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GridsGalore
{
    internal class VertexCollection
    {
        private readonly AutoDictionary<double, Dictionary<double, Vertex>> VertexMap = new AutoDictionary<double, Dictionary<double, Vertex>>();

        public VertexCollection()
        {
            VertexMap.Defaulter = () => new Dictionary<double, Vertex>();
        }
    }

    internal class AutoDictionary<K, V> : IDictionary<K, V>
    {
        public virtual Func<V> Defaulter { get; set; } = null;

        protected Dictionary<K, V> store = new Dictionary<K, V>();

        public V this[K key] { get => GetOrDefault(key); set => Set(key, value); }

        public ICollection<K> Keys => store.Keys;

        public ICollection<V> Values => store.Values;

        public int Count => store.Count;

        public bool IsReadOnly => false;

        public void Set(K key, V value)
        {
            if (!store.ContainsKey(key))
            {
                store.Add(key, value);
            }
            else
            {
                store[key] = value;
            }
        }

        public V GetOrDefault(K key, Func<V> def = null)
        {
            if (!TryGetValue(key, out V ret))
            {
                V d = default(V);

                if (def != null)
                {
                    d = def.Invoke();
                }
                else if (Defaulter != null)
                {
                    d = Defaulter.Invoke();
                }

                Add(key, d);
            }

            return ret;
        }

        public void Add(K key, V value)
        {
            store.Add(key, value);
        }

        public void Add(KeyValuePair<K, V> item)
        {
            store.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            store.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(K key)
        {
            return store.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return store.GetEnumerator();
        }

        public bool Remove(K key)
        {
            return store.Remove(key);
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(K key, out V value)
        {
            return store.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
