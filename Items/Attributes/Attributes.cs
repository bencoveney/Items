using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    /// <summary>
    /// A named collection of attributes.
    /// </summary>
    public class Attributes : IDictionary<String, IAttribute>
    {
        private List<IAttribute> _internalAttributes = new List<IAttribute>();

        public void Add(IAttribute value)
        {
            Add(value.Name, value);
        }

        public void Add(string key, IAttribute value)
        {
            if(value.Name != key)
                throw new Exception("Attribute key is it's name");

            if(_internalAttributes.Any(attribute => attribute.Name == key))
                throw new Exception("Attribute with that name already exists");

            _internalAttributes.Add(value);
        }

        public bool ContainsKey(String key)
        {
            return _internalAttributes.Any(attribute => attribute.Name == key);
        }

        public ICollection<string> Keys
        {
            get
            {
                return _internalAttributes.Select<IAttribute, String>(attribute => attribute.Name).ToList();
            }
        }

        public bool Remove(String key)
        {
            return _internalAttributes.RemoveAll(attribute => attribute.Name == key) > 0;
        }

        public bool TryGetValue(String key, out IAttribute value)
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

        public ICollection<IAttribute> Values
        {
            get { return _internalAttributes; }
        }

        public IAttribute this[String key]
        {
            get
            {
                return _internalAttributes.Single(attribute => attribute.Name == key);
            }
            set
            {
                this.Add(key, value);
            }
        }

        public void Add(KeyValuePair<string, IAttribute> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _internalAttributes = new List<IAttribute>();
        }

        public bool Contains(KeyValuePair<string, IAttribute> item)
        {
            IAttribute result;
            this.TryGetValue(item.Key, out result);
            return (result == item.Value);
        }

        public void CopyTo(KeyValuePair<string, IAttribute>[] array, int arrayIndex)
        {
            if(_internalAttributes.Count + arrayIndex > array.Length)
                throw new IndexOutOfRangeException();

            for(int i = 0; i < array.Length - arrayIndex; i++)
            {
                array[i + arrayIndex] = new KeyValuePair<string,IAttribute>(_internalAttributes[i].Name, _internalAttributes[i]);
            }
        }

        public int Count
        {
            get { return _internalAttributes.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, IAttribute> item)
        {
            // doesnt check whether the item only matches on one, could be bad data
            return _internalAttributes.RemoveAll(attribute => attribute.Name == item.Key && attribute == item.Value) > 0;
        }

        public IEnumerator<KeyValuePair<string, IAttribute>> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
