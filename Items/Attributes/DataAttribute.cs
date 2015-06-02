namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// An DataAttribute
	/// </summary>
	public class DataAttribute
		: DataDefinition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DataAttribute"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="type">The type.</param>
		/// <param name="nullability">The emptiness.</param>
		public DataAttribute(string name, IType type, Nullability nullability)
			: base(name, type, nullability)
		{
		}
	}
}