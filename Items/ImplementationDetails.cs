namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	/// <summary>
	/// A key, value collection of implementation detail names and their values
	/// </summary>
	[Serializable]
	public class ImplementationDetailsDictionary
		: Dictionary<string, object>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ImplementationDetailsDictionary"/> class.
		/// </summary>
		public ImplementationDetailsDictionary()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImplementationDetailsDictionary"/> class.
		/// </summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		protected ImplementationDetailsDictionary(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
