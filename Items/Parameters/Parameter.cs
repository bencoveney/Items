namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// A piece of data passed into a stored procedure
	/// </summary>
	public class Parameter
		: DataDefinition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Parameter"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="type">The type.</param>
		/// <param name="nullability">The emptiness.</param>
		public Parameter(string name, IType type, Nullability nullability)
			: base(name, type, nullability)
		{
		}
	}
}
