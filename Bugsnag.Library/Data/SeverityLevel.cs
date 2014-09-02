// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SeverityLevel.cs" company="n/a">
//   2014
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bugsnag.Library.Data
{
	/// <summary>
	/// The level of the error
	/// </summary>
	public enum SeverityLevel
	{
		/// <summary>
		/// to be used like a null with not using the nullable version
		/// </summary>
		None = 0,

		/// <summary>
		/// The error
		/// </summary>
		Error = 1,

		/// <summary>
		/// The warning
		/// </summary>
		Warning = 2,

		/// <summary>
		/// The information
		/// </summary>
		Info = 3
	}
}