// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Device.cs" company="n/a">
//   2014
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bugsnag.Library.Data
{
	using System.Runtime.Serialization;

	/// <summary>
	/// class to holde device data
	/// </summary>
	[DataContract]
	public class Device
	{
		/// <summary>
		/// The operating system version of the client that the error was 
		/// generated on. (optional, default none)
		/// </summary>
		[DataMember(Name = "osVersion")]
		public string OSVersion { get; set; }

		/// <summary>
		/// Gets or sets the name of the host.
		///		The hostname of the server running your code
		///		(optional, default none)
		/// </summary>
		/// <value>
		/// The name of the host.
		/// </value>
		[DataMember(Name = "hostname")]
		public string HostName { get; set; }
	}
}
