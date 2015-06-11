namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Defines a type which exists in the system
	/// </summary>
	/// <typeparam name="T">The system type</typeparam>
	public class SystemType<T>
		: IType
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SystemType{T}"/> class.
		/// </summary>
		public SystemType()
		{
			this.Details = new ImplementationDetailsDictionary();
		}

		/// <summary>
		/// Gets system type
		/// </summary>
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
		public string Name
		{
			get
			{
				return this.DataType.FullName;
			}
		}

		/// <summary>
		/// Gets the implementation specific details attached to the type
		/// </summary>
		/// <value>
		/// The details.
		/// </value>
		public ImplementationDetailsDictionary Details
		{
			get;
			private set;
		}
	}
}
