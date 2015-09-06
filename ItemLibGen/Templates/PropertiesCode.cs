using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibGen.Templates
{
	/// <summary>
	/// A code template for generating properties
	/// </summary>
	partial class Properties
		: PropertiesBase, ITemplate
	{
		/// <summary>
		/// The things properties are generated for
		/// </summary>
		private IEnumerable<Items.IThing> things;

		/// <summary>
		/// Initializes a new instance of the <see cref="Properties"/> class.
		/// </summary>
		/// <param name="model">The model.</param>
		public Properties(Model model)
		{
			this.things = model.Things;
		}
	}
}
