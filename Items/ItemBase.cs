namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A basic "thing". A container of data
    /// </summary>
    public abstract partial class ItemBase
    {
        /// <summary>
        /// The string identifier
        /// </summary>
        private ValueAttribute stringIdentifier;

        /// <summary>
        /// The integer identifier
        /// </summary>
        private ValueAttribute integerIdentifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemBase" /> class.
        /// Creates the item
        /// </summary>
        /// <param name="name">The name.</param>
        public ItemBase(string name)
        {
            this.Name = name;
            this.Attributes = new Attributes();
        }

        /// <summary>
        /// Gets or sets the name of the item
        /// ReadOnly?
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a special case of unique attribute which can be used to identify the item
        /// Should be in the list of attributes
        /// should we require an identifier in order to perform lookups?
        /// </summary>
        public ValueAttribute StringIdentifer
        {
            get
            {
                return this.stringIdentifier;
            }

            set
            {
                if (!(value.Type is SystemType<string>))
                {
                    throw new ArgumentException("stringIdentifier must be of type SystemType<string>");
                }

                this.stringIdentifier = value;
            }
        }

        /// <summary>
        /// Gets or sets a special case of unique attribute which can be used to identify the item
        /// Should be in the list of attributes
        /// should we require an identifier in order to perform lookups?
        /// </summary>
        public ValueAttribute IntegerIdentifer
        {
            get
            {
                return this.integerIdentifier;
            }

            set
            {
                // TODO accept other size ints?
                if (!(value.Type is SystemType<int>))
                {
                    throw new ArgumentException("IntegerIdentifer must be of type SystemType<Int32>");
                }

                this.integerIdentifier = value;
            }
        }

        /// <summary>
        /// Gets the data for the instance of the item
        /// Should be dictionary, with the identifier being a key on the dictionary
        /// </summary>
        public Attributes Attributes
        {
            get;
            private set;
        }
    }
}
