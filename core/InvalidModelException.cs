﻿namespace Items
{
	using System;

	/// <summary>
	/// Raised when a problem is found with the model
	/// </summary>
	public class InvalidModelException
		: Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidModelException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public InvalidModelException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidModelException"/> class.
		/// </summary>
		public InvalidModelException()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidModelException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public InvalidModelException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
