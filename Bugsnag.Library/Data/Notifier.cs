// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Notifier.cs" company="n/a">
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
    /// Notifier meta data
    /// </summary>
    [DataContract]
    public class Notifier
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Notifier"/> class.
		/// </summary>
        public Notifier()
        {
			this.Name = ".NET Bugsnag";
			this.Version = "0.2";
			this.Url = "https://github.com/buckrogerz/net-bugsnag";
        }

        /// <summary>
        /// The notifier name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

		/// <summary>
        /// The notifier's current version
        /// </summary>
		[DataMember(Name = "version")]
		public string Version { get; set; }

		/// <summary>
        /// The URL associated with the notifier
        /// </summary>
		[DataMember(Name = "url")]
		public string Url { get; set; }
    }
}
