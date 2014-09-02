// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppInfo.cs" company="n/a">
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
	/// class to hold data on the app
	/// </summary>
	[DataContract]
	public class AppInfo
	{
		/// <summary>
		/// The version number of the application which generated the error.
		/// If appVersion is set and an error is resolved in the dashboard
		/// the error will not unresolve until a crash is seen in a newer
		/// version of the app.
		/// (optional, default none, filtered)
		/// </summary>
		[DataMember(Name = "version")]
		public string Version { get; set; }

		/// <summary>
		/// Gets the release stage that this error occurred in, for example
		/// "development", "staging" or "production".
		/// (optional, default "production", filtered)
		/// </summary>
		[DataMember(Name = "releaseStage")]
		public string ReleaseStage { get; set; }
	}
}