using System;

namespace GeneratedCode
{
	/// <summary>
	/// A thing.
	/// </summary>
	public abstract partial class ThingBase
	{
		/// <summary>
		/// A flag indicating whether this instance is manifested in the database
		/// </summary>
		protected bool IsManifested = false;

		/// <summary>
		/// A flag indicating whether this object has been populated from the database.
		/// </summary>
		protected bool IsPopulated = false;

		/// <summary>
		/// The action used to manifest itself in the database.
		/// </summary>
		protected Action Manifest;

		/// <summary>
		/// The action used to populate data members with if loading is deferred
		/// </summary>
		protected Action Populate;

		/// <summary>
		/// Manifests this object in the database if that has not yet been done.
		/// </summary>
		private void EnsureManifested()
		{
			if (!IsManifested)
			{
				Manifest();

				this.IsManifested = true;
			}
		}

		/// <summary>
		/// Ensures that all data members have been loaded if deferred.
		/// </summary>
		protected void EnsurePopulated()
		{
			if(!IsPopulated)
			{
				if (Populate == null)
				{
					throw new Exception("No means of population has been provided");
				}

				Populate();

				this.IsPopulated = true;
			}
		}
	}
}
