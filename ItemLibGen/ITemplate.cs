using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibGen
{
	/// <summary>
	/// Can generate text
	/// Facilitates re-use of text template handling code
	/// </summary>
	internal interface ITemplate
	{
		/// <summary>
		/// Transforms the text.
		/// </summary>
		/// <returns>The result of templating</returns>
		string TransformText();
	}
}
