namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// An action which is a stored procedure execution.
	/// </summary>
	public class StoredProcedureAction
		: IAction
	{
		/// <summary>
		/// Gets the name of the action.
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
		/// Gets the implementation specific details.
		/// </summary>
		/// <value>
		/// The details.
		/// </value>
		public ImplementationDetails Details
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the name of the action's stored procedure.
		/// </summary>
		/// <value>
		/// The name of the stored procedure.
		/// </value>
		public string StoredProcedureName
		{
			get;
			private set;
		}
	}
}
