using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Items;

namespace ItemSelector
{
	public class ModelQuery
	{
		ModelQueryItemLink rootItemLink;

		public ModelQuery(Item rootItem)
		{
			rootItemLink = new ModelQueryItemLink(rootItem);
		}
	}
}
