namespace Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	
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
    }
}
