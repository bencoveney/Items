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
		/// Gets the left link.
		/// </summary>
		/// <value>
		/// The left link.
		/// </value>
		public RelationshipLink LeftLink
		{
			get
			{
				return this.Links[0];
			}
		}

		/// <summary>
		/// Gets the right link.
		/// </summary>
		/// <value>
		/// The right link.
		/// </value>
		public RelationshipLink RightLink
		{
			get
			{
				return this.Links[1];
			}
		}

		/// <summary>
		/// Used by a relationship to indicate what thing (and the quantity of those things) is being related 
		/// </summary>
		public class RelationshipLink
		{
			/// <summary>
			/// Gets or sets the lowest amount of things which can be encompassed by this link (can't be negative)
			/// </summary>
			/// <value>
			/// The amount lower.
			/// </value>
			public int AmountLower { get; set; }

			/// <summary>
			/// Gets or sets the highest amount of things which can be encompassed by this link (null if no limit)
			/// </summary>
			/// <value>
			/// The amount upper.
			/// </value>
			public int? AmountUpper { get; set; }

			/// <summary>
			/// Gets or sets the thing being related by this link
			/// </summary>
			/// <value>
			/// The thing.
			/// </value>
			public Thing Thing { get; set; }
		}
	}
}