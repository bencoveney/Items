using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Items
{
    public abstract class ItemBase
    {
        // TODO Singletons?

        /// <summary>
        /// The name of the item
        /// ReadOnly?
        /// </summary>
        public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// A special case of unique attribute which can be used to identify the item
        /// Should be in the list of attributes
        /// shold we require an identifier in order to perform lookups?
        /// </summary>
        private ValueAttribute _stringIdentifier;
        public ValueAttribute StringIdentifer
        {
            get
            {
                return _stringIdentifier;
            }
            set
            {
                if (!(value.Type is SystemType<String>))
                    throw new ArgumentException("StringIdentifier must be of type SystemType<String>");

                _stringIdentifier = value;
            }
        }

        /// <summary>
        /// A special case of unique attribute which can be used to identify the item
        /// Should be in the list of attributes
        /// shold we require an identifier in order to perform lookups?
        /// </summary>
        private ValueAttribute _integerIdentifier;
        public ValueAttribute IntegerIdentifer
        {
            get
            {
                return _integerIdentifier;
            }
            set
            {
                // TODO accept other size ints?
                if (!(value.Type is SystemType<Int32>))
                    throw new ArgumentException("IntegerIdentifer must be of type SystemType<Int32>");

                _integerIdentifier = value;
            }
        }

        /// <summary>
        /// The data for the instance of the item
        /// Should be dictionary, with the identifier being a key on the dictionary
        /// </summary>
        public Attributes Attributes
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates the item
        /// </summary>
        public ItemBase(String name)
        {
            Name = name;
            Attributes = new Attributes();
        }
    }
}
