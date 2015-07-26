namespace Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

	/// <summary>
	/// An attribute
	/// </summary>
    [DataContract]
	public class DataMember
		: DataDefinition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DataMember"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="type">The type.</param>
		/// <param name="nullConstraint">The emptiness.</param>
		public DataMember(string name, IType type, NullConstraints nullConstraint)
			: base(name, type, nullConstraint)
		{
		}
	}
}