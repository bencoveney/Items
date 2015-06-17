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
		/// The schema of implementation details which are allowed to be put into the implementation details dictionary for this class.
		/// </summary>
		private static Dictionary<string, Type> schema = new Dictionary<string, Type>();

		/// <summary>
		/// Initializes a new instance of the <see cref="StoredProcedureAction"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		public StoredProcedureAction(string name, string storedProcedureName)
		{
			this.Name = name;
			this.StoredProcedureName = storedProcedureName;
			this.Details = new ImplementationDetailsDictionary(schema);
		}

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
		public ImplementationDetailsDictionary Details
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
