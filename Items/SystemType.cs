namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

	/// <summary>
	/// Defines a type which exists in the system
	/// </summary>
	/// <typeparam name="T">The system type</typeparam>
    [DataContract]
	public class SystemType<T>
		: SystemTypeBase, IType
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SystemType{T}"/> class.
		/// </summary>
		public SystemType()
			: base()
		{
		}

		/// <summary>
		/// Gets system type
		/// </summary>
        [DataMember]
		public Type DataType
		{
			get
			{
				return typeof(T);
			}
		}

		/// <summary>
		/// Gets the type's name through reflection
		/// </summary>
        [DataMember]
		public string Name
		{
			get
			{
				return this.DataType.FullName;
			}
		}
	}
}
