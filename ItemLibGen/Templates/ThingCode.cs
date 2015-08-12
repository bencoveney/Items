using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibGen.Templates
{
	partial class Thing : ThingBase
	{
		private IEnumerable<Items.Thing> items;
		private IEnumerable<Items.Relationship> relationships;
		private IEnumerable<Items.Category> categories;

		public Thing(Model model)
		{
			this.items = model.Items;
			this.relationships = model.Relationships;
			this.categories = model.Categories;
		}
	}
}
