namespace Items
{
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

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			if (AmountUpper.HasValue)
			{
				if (AmountLower == AmountUpper.Value)
				{
					return string.Format("{0} ({1})", this.Thing.Name, AmountLower);
				}
				else
				{
					return string.Format("{0} ({1} - {2})", this.Thing.Name, this.AmountLower, this.AmountUpper.Value);
				}
			}
			else
			{
				return string.Format("{0} ({1} - *)", this.Thing.Name, AmountLower);
			}
		}
	}
}
