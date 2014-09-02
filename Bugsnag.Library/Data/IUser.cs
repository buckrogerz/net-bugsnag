// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUser.cs" company="n/a">
//   2014
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bugsnag.Library.Data
{
	using System;

	/// <summary>
	/// guarentees information on the user
	/// </summary>
	public interface IUser
	{
		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>
		/// The username.
		/// </value>
		string Username { get; set; }

		/// <summary>
		/// Gets or sets the user unique identifier.
		/// </summary>
		/// <value>
		/// The user unique identifier.
		/// </value>
		Guid UserGuid { get; set; }
	}
}
