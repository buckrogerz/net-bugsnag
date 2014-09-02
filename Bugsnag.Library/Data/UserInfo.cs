// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserInfo.cs" company="n/a">
//   2014
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bugsnag.Library.Data
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// class to hold user info
	/// </summary>
	[DataContract]
	public class UserInfo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserInfo"/> class.
		/// </summary>
		public UserInfo()
		{
			this.UserId = Guid.Empty.ToString();
			this.Name = "Unknown";
			this.Email = "development@spindle.com";
		}

		/// <summary>
		/// A unique identifier for a user affected by this event. This could be 
		/// any distinct identifier that makes sense for your application/platform.
		/// This field is optional but highly recommended.
		/// </summary>
		[DataMember(Name = "id")]
		public string UserId { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// The user's name, or a string you use to identify them.
		/// (optional, searchable)
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// The user's email address.
		/// (optional, searchable)
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		[DataMember(Name = "email")]
		public string Email { get; set; }
	}
}