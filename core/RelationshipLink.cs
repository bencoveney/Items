namespace Items
{
	using System;
	using System.Globalization;

	/// <summary>
	/// Used by a relationship to indicate what thing (and the quantity of those things) is being related
	/// </summary>
	public class RelationshipLink
	{
		/// <summary>
		/// Backing variable for AmountLower property
		/// </summary>
		private int amountLower;

		/// <summary>
		/// Backing variable for AmountUpper property
		/// </summary>
		private int? amountUpper;

		/// <summary>
		/// Initializes a new instance of the <see cref="RelationshipLink"/> class.
		/// </summary>
		/// <param name="thing">The thing.</param>
		/// <param name="amountLower">The the lowest amount of things which can be encompassed by this link (can't be negative).</param>
		public RelationshipLink(IThing thing, int amountLower)
			: this(thing, amountLower, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RelationshipLink" /> class.
		/// </summary>
		/// <param name="thing">The thing.</param>
		/// <param name="amountLower">The lowest amount of things which can be encompassed by this link (can't be negative).</param>
		/// <param name="amountUpper">The upper amount of things which can be encompassed by this link (can't be negative).</param>
		public RelationshipLink(IThing thing, int amountLower, int? amountUpper)
		{
			if (thing == null)
			{
				throw new ArgumentNullException("thing", "thing cannot be null");
			}

			this.Thing = thing;
			this.AmountLower = amountLower;
			this.AmountUpper = amountUpper;
		}

		/// <summary>
		/// Gets the lowest amount of things which can be encompassed by this link (can't be negative)
		/// </summary>
		/// <value>
		/// The amount lower.
		/// </value>
		/// <exception cref="ArgumentException">
		/// AmountLower cannot be negative;value
		/// or
		/// AmountLower must be smaller than AmountUpper;value
		/// </exception>
		public int AmountLower
		{
			get
			{
				return this.amountLower;
			}

			private set
			{
				if (value < 0)
				{
					throw new ArgumentException("AmountLower cannot be negative", "value");
				}

				if (this.AmountUpper.HasValue && this.AmountUpper < value)
				{
					throw new ArgumentException("AmountLower must be smaller than (or equal to) AmountUpper", "value");
				}

				this.amountLower = value;
			}
		}

		/// <summary>
		/// Gets the highest amount of things which can be encompassed by this link (null if no limit)
		/// </summary>
		/// <value>
		/// The amount upper.
		/// </value>
		public int? AmountUpper
		{
			get
			{
				return this.amountUpper;
			}

			private set
			{
				if (value.HasValue && value < 0)
				{
					throw new ArgumentException("AmountLower cannot be negative", "value");
				}

				if (value.HasValue && this.AmountLower > value)
				{
					throw new ArgumentException("AmountUpper must be larger than (or equal to) AmountLower", "value");
				}

				this.amountUpper = value;
			}
		}

		/// <summary>
		/// Gets the thing being related by this link
		/// </summary>
		/// <value>
		/// The thing.
		/// </value>
		public IThing Thing
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets a string indicating the amount of items this link can represent.
		/// </summary>
		/// <returns>A string indicating the amount of items this link can represent.</returns>
		public string AmountString
		{
			get
			{
				if (this.AmountUpper.HasValue)
				{
					if (this.AmountLower == this.AmountUpper.Value)
					{
						return this.AmountLower.ToString(CultureInfo.CurrentCulture);
					}
					else
					{
						return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", this.AmountLower, this.AmountUpper.Value);
					}
				}
				else
				{
					return string.Format(CultureInfo.CurrentCulture, "{0} - *", this.AmountLower);
				}
			}
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", this.Thing.Name, this.AmountString);
		}
	}
}
