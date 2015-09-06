using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibGen.Templates
{
	/// <summary>
	/// A code template for generating thing classes
	/// </summary>
	partial class Thing : ThingBase, ITemplate
	{
		/// <summary>
		/// The items classes are generated for
		/// </summary>
		private IEnumerable<ItemLoader.DbiItem> items;

		/// <summary>
		/// The relationships classes are generated for
		/// </summary>
		private IEnumerable<ItemLoader.DbiRelationship> relationships;

		/// <summary>
		/// The categories classes are generated for
		/// </summary>
		private IEnumerable<ItemLoader.DbiCategory> categories;

		/// <summary>
		/// Initializes a new instance of the <see cref="Thing"/> class.
		/// </summary>
		/// <param name="model">The model.</param>
		public Thing(Model model)
		{
			this.items = model.Items.OfType<ItemLoader.DbiItem>();
			this.relationships = model.Relationships.OfType<ItemLoader.DbiRelationship>();
			this.categories = model.Categories.OfType<ItemLoader.DbiCategory>();
		}
	}
}
