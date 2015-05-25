namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Used to define relations between objects
	/// e.g. 1 fridge contains many bacons
	/// TODO is in necessary to make a distinction between aggregation and composition
	/// </summary>
	public class Relationship
		: Thing
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Relationship"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		public Relationship(string name, Thing left, Thing right)
			: base(name)
		{
			this.Links = new RelationshipLink[2];
			this.Links[0] = new RelationshipLink() { AmountLower = 0, AmountUpper = null, Thing = left };
			this.Links[1] = new RelationshipLink() { AmountLower = 0, AmountUpper = null, Thing = right };
		}

		/// <summary>
		/// Gets the links this relationship has to other items (should only be 2)
		/// </summary>
		/// <value>
		/// The links.
		/// </value>
		public RelationshipLink[] Links
		{
			get;
			private set;
		}

		/// <summary>
		/// Used by a relationship to indicate what thing (and the quantity of those things) is being related 
		/// </summary>
		public struct RelationshipLink
		{
			/// <summary>
			/// The lowest amount of things which can be encompassed by this link (can't be negative)
			/// </summary>
			public int AmountLower;

			/// <summary>
			/// The highest amount of things which can be encompassed by this link (null if no limit)
			/// </summary>
			public int? AmountUpper;

			/// <summary>
			/// The thing being related by this link
			/// </summary>
			public Thing Thing;
		}
	}
}