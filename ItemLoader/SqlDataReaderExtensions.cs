namespace ItemLoader
{
	using System;
	using System.Data.SqlClient;

	/// <summary>
	/// Extension methods for the SQL Data Reader class
	/// </summary>
	public static class SqlDataReaderExtensions
	{
		/// <summary>
		/// Gets a (potentially null) string safely.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <returns>The value in the specified column.</returns>
		public static string GetNullableString(this SqlDataReader reader, string columnName)
		{
			return reader.GetNullableString(reader.GetOrdinal(columnName));
		}

		/// <summary>
		/// Gets a (potentially null) string safely.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="columnOrdinal">The column ordinal.</param>
		/// <returns>The value in the specified column.</returns>
		public static string GetNullableString(this SqlDataReader reader, int columnOrdinal)
		{
			return reader.IsDBNull(columnOrdinal) ? null : reader.GetString(columnOrdinal);
		}

		/// <summary>
		/// Gets a potentially null value safely.
		/// </summary>
		/// <typeparam name="T">The type of struct to get.</typeparam>
		/// <param name="reader">The reader.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <returns>The value in the specified column.</returns>
		public static T? GetNullable<T>(this SqlDataReader reader, string columnName) where T : struct
		{
			return reader.GetNullable<T>(reader.GetOrdinal(columnName));
		}

		/// <summary>
		/// Gets a potentially null value safely.
		/// </summary>
		/// <typeparam name="T">The type of struct to get.</typeparam>
		/// <param name="reader">The reader.</param>
		/// <param name="columnOrdinal">The column ordinal.</param>
		/// <returns>The value in the specified column.</returns>
		public static T? GetNullable<T>(this SqlDataReader reader, int columnOrdinal) where T : struct
		{
			return reader.IsDBNull(columnOrdinal) ? (T?)null : (T?)reader.GetValue(columnOrdinal);
		}
	}
}
