using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemLoader
{
	public class DatabaseConstraintLoader
	{
		enum ConstraintQueryOrdinals
		{
			ConstraintName = 0,
			ConstraintType = 1,
			TableName = 2,
			ColumnName = 3,
			IsDeferrable = 4,
			InitiallyDeferred = 5
		}
	}
}
