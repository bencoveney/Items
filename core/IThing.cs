namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// A basic "thing". A container of data
	/// </summary>
	public interface IThing
		: INamedObject
	{
		/// <summary>
		/// Gets or sets a special case of unique attribute which can be used to identify the item
		/// Should be in the list of attribute
		/// should we require an identifier in order to perform lookups?
		/// </summary>
		DataMember StringIdentifier
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a special case of unique attribute which can be used to identify the item
		/// Should be in the list of attribute
		/// should we require an identifier in order to perform lookups?
		/// </summary>
		DataMember IntegerIdentifier
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the data for the instance of the item
		/// Should be dictionary, with the identifier being a key on the dictionary
		/// </summary>
		NamedCollection<DataMember> Attributes
		{
			get;
		}

		/// <summary>
		/// Gets the relationships which reference this thing.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns>A collection of relationships which reference this thing.</returns>
		/// <exception cref="ArgumentNullException">model;model can not be null</exception>
		IEnumerable<Relationship> GetReferenceRelationships(Model model);
	}
}
