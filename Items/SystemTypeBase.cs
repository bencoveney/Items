namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Defines a type which exists in the system
	/// </summary>
	public class SystemTypeBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SystemTypeBase"/> class.
		/// </summary>
		protected SystemTypeBase()
		{
			this.Details = new ImplementationDetailsDictionary();
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
