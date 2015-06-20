namespace Items
{
	using System.Collections.ObjectModel;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Used to define relations between objects
	/// e.g. 1 fridge contains many bacons
	/// TODO is in necessary to make a distinction between aggregation and composition
	/// </summary>
	public class Relationship
		: Thing
	{
		/// <summary>
		/// Gets the links this relationship has to other items (should only be 2)
		/// </summary>
		private RelationshipLink[] relationshipLinks;

		/// <summary>
		/// Initializes a new instance of the <see cref="Relationship"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		public Relationship(string name, Thing left, Thing right)
			: base(name)
		{
			this.relationshipLinks = new RelationshipLink[2];
			this.relationshipLinks[0] = new RelationshipLink(left, 0);
			this.relationshipLinks[1] = new RelationshipLink(right, 0);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Relationship"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		public Relationship(string name, RelationshipLink left, RelationshipLink right)
			: base(name)
		{
			this.relationshipLinks = new RelationshipLink[2];
			this.relationshipLinks[0] = left;
			this.relationshipLinks[1] = right;
		}

		/// <summary>
		/// Gets the links this relationship has to other items (should only be 2)
		/// </summary>
		/// <value>
		/// The links.
		/// </value>
		public Collection<RelationshipLink> Links
		{
			get
			{
				return new Collection<RelationshipLink>(this.relationshipLinks);
			}
		}

		/// <summary>
		/// Gets the linked things.
		/// </summary>
		/// <value>
		/// The linked things.
		/// </value>
		public IEnumerable<Thing> LinkedThings
		{
			get
			{
				return this.Links.Select<RelationshipLink, Thing>(link => link.Thing);
			}
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
	}
}