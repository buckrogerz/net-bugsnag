// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorNotification.cs" company="n/a">
//   2014
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bugsnag.Library.Data
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	/// <summary>
	/// class for all data to be submitted to bugsnag
	/// </summary>
	[DataContract]
    public class ErrorNotification
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorNotification"/> class.
		/// </summary>
		public ErrorNotification()
        {
			this.NotiferData = new Notifier();
			this.Events = new List<Event>();
        }

        /// <summary>
        /// The API Key associated with the project. Informs Bugsnag which project 
        /// has generated this error.
        /// </summary>
        [DataMember(Name = "apiKey")]
        public string Api_Key { get; set; }

		/// <summary>
        /// This object describes the notifier itself. These properties are used 
        /// within Bugsnag to track error rates from a notifier.
		/// </summary>
		[DataMember(Name = "notifier")]
		public Notifier NotiferData { get; set; }

		/// <summary>
        /// An array of error events that Bugsnag should be notified of. A notifier
        /// can choose to group notices into an array to minimize network traffic, or
        /// can notify Bugsnag each time an event occurs. 
        /// </summary>
		[DataMember(Name = "events")]
		public List<Event> Events { get; set; }
    }
}