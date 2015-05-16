using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    /// <summary>
    /// A named collection of behaviors.
    /// </summary>
    public class Behaviors : IDictionary<String, Behavior>
    {
        private List<Behavior> _internalBehaviors = new List<Behavior>();

        public void Add(Behavior value)
        {
            Add(value.Name, value);
        }

        public void Add(string key, Behavior value)
        {
            if(value.Name != key)
                throw new Exception("Behavior key is it's name");

            if(_internalBehaviors.Any(behavior => behavior.Name == key))
                throw new Exception("Behavior with that name already exists");

            _internalBehaviors.Add(value);
        }

        public bool ContainsKey(String key)
        {
            return _internalBehaviors.Any(behavior => behavior.Name == key);
        }

        public ICollection<string> Keys
        {
            get
            {
                return _internalBehaviors.Select<Behavior, String>(behavior => behavior.Name).ToList();
            }
        }

        public bool Remove(String key)
        {
            return _internalBehaviors.RemoveAll(behavior => behavior.Name == key) > 0;
        }

        public bool TryGetValue(String key, out Behavior value)
        {
            if (this.ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            else
            {
                value = null;
                return false;
            }

        }

        public ICollection<Behavior> Values
        {
            get { return _internalBehaviors; }
        }

        public Behavior this[String key]
        {
            get
            {
                return _internalBehaviors.Single(behavior => behavior.Name == key);
            }
            set
            {
                this.Add(key, value);
            }
        }

        public void Add(KeyValuePair<string, Behavior> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _internalBehaviors = new List<Behavior>();
        }

        public bool Contains(KeyValuePair<string, Behavior> item)
        {
            Behavior result;
            this.TryGetValue(item.Key, out result);
            return (result == item.Value);
        }

        public void CopyTo(KeyValuePair<string, Behavior>[] array, int arrayIndex)
        {
            if (_internalBehaviors.Count + arrayIndex > array.Length)
                throw new IndexOutOfRangeException();

            for (int i = 0; i < array.Length - arrayIndex; i++)
            {
                array[i + arrayIndex] = new KeyValuePair<string, Behavior>(_internalBehaviors[i].Name, _internalBehaviors[i]);
            }
        }

        public int Count
        {
            get { return _internalBehaviors.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, Behavior> item)
        {
            // doesnt check whether the item only matches on one, could be bad data
            return _internalBehaviors.RemoveAll(behavior => behavior.Name == item.Key && behavior == item.Value) > 0;
        }

        public IEnumerator<KeyValuePair<string, Behavior>> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
