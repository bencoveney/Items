using Items;
using ItemLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibGen.Templates
{
	/// <summary>
	/// A code template for generating PopulateFromIdentifier functions
	/// </summary>
	partial class PopulateFromIdentifier
		: PopulateFromIdentifierBase, ITemplate
	{
		/// <summary>
		/// The things constructors are generated for
		/// </summary>
		private IEnumerable<Items.IThing> things;

		/// <summary>
		/// Initializes a new instance of the <see cref="PopulateFromIdentifierCode"/> class.
		/// </summary>
		/// <param name="model">The model.</param>
		public PopulateFromIdentifier(Model model)
		{
			this.things = model.Things;
		}

		private string ProduceSqlForStringIdentifier(IDbiThing thing)
		{
			if (thing.StringIdentifier == null)
			{
				throw new Exception("No string identifier");
			}

			if (!(thing.StringIdentifier is DbiDataMember))
			{
				throw new Exception("String identifier is not based on a column");
			}

			// SELECT
			StringBuilder builder = new StringBuilder("SELECT ");
			List<string> attributeNames = new List<string>();
			foreach (DbiDataMember member in thing.Attributes.OfType<DbiDataMember>())
			{
				attributeNames.Add(member.SqlColumn);
			}
			builder.Append(string.Join(", ", attributeNames));

			// FROM
			builder.Append(" FROM ");
			builder.Append(thing.Name);

			// WHERE
			builder.Append(" WHERE ");
			builder.Append(((DbiDataMember)thing.StringIdentifier).SqlColumn);
			builder.Append(" = ");
			builder.Append("@identifier");

			return builder.ToString();
		}

		private string ProduceSqlForIntegerIdentifier(IDbiThing thing)
		{
			if (thing.IntegerIdentifier == null)
			{
				throw new Exception("No integer identifier");
			}

			if (!(thing.IntegerIdentifier is DbiDataMember))
			{
				throw new Exception("Integer identifier is not based on a column");
			}

			// SELECT
			StringBuilder builder = new StringBuilder("SELECT ");
			List<string> attributeNames = new List<string>();
			foreach (DbiDataMember member in thing.Attributes.OfType<DbiDataMember>())
			{
				attributeNames.Add(member.SqlColumn);
			}
			builder.Append(string.Join(", ", attributeNames));

			// FROM
			builder.Append(" FROM ");
			builder.Append(thing.Name);

			// WHERE
			builder.Append(" WHERE ");
			builder.Append(((DbiDataMember)thing.IntegerIdentifier).SqlColumn);
			builder.Append(" = ");
			builder.Append("@identifier");

			return builder.ToString();
		}
	}
}