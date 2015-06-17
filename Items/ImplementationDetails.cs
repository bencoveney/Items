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
		/// Initializes a new instance of the <see cref="ImplementationDetailsDictionary" /> class.
		/// </summary>
		/// <param name="schema">The schema.</param>
		public ImplementationDetailsDictionary(Dictionary<string, Type> schema)
			: base()
		{
			this.Schema = schema;
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

		/// <summary>
		/// Gets the schema defining which keys and value types are allowed.
		/// </summary>
		/// <value>
		/// The schema.
		/// </value>
		public Dictionary<string, Type> Schema
		{
			get;
			private set;
		}

		/// <summary>
		/// Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.
		/// </summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</param>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
