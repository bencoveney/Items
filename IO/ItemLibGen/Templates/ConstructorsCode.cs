using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibGen.Templates
{
	/// <summary>
	/// A code template for generating constructors
	/// </summary>
	partial class Constructors
		: ConstructorsBase, ITemplate
	{
		/// <summary>
		/// The things constructors are generated for
		/// </summary>
		private IEnumerable<Items.IThing> things;

		/// <summary>
		/// Initializes a new instance of the <see cref="Constructors"/> class.
		/// </summary>
		/// <param name="model">The model.</param>
		public Constructors(Model model)
		{
			this.things = model.Things;
		}
	}
}
