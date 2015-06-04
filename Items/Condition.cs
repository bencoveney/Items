namespace Items
{
	using System.Collections.ObjectModel;

	/// <summary>
	/// Dictates what needs to be met in order for something to happen/be allowed to happen
	/// </summary>
	public class Condition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Condition"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public Condition(string name)
		{
			this.Name = name;
			this.Inputs = new Collection<object>();
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the inputs.
		/// TODO object is terrible for this
		/// </summary>
		/// <value>
		/// The inputs.
		/// </value>
		public Collection<object> Inputs
		{
			get;
			private set;
		}

		// Condition comparison type? this = that? this in enum? this exists? this is of type?
	}
}
