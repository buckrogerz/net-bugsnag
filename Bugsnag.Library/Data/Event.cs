﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bugsnag.Library.Data
{
    /// <summary>
    /// Error event that Bugsnag should be notified of. A notifier
    /// can choose to group notices into an array to minimize network traffic, or
    /// can notify Bugsnag each time an event occurs
    /// </summary>
    [DataContract]
    public class Event
    {
        public Event()
        {
			this.Exceptions = new List<Exception>();
			this.User = new UserInfo();
			this.ApplicationInformation = new AppInfo();
			this.DeviceInformation = new Device();
			this.PayloadVersion = "2";
        }

		// The version number of the payload. If not set to 2+, Severity will
		// not be supported.
		// (required, must be set to "2")
		[DataMember(Name = "payloadVersion")]
		public string PayloadVersion { get; set; }

        /// <summary>
		/// An array of exceptions that occurred during this event. Most of the
		/// time there will only be one exception, but some languages support 
		/// "nested" or "caused by" exceptions. In this case, exceptions should 
		/// be unwrapped and added to the array one at a time. The first exception
		/// raised should be first in this array.
        /// </summary>
		[DataMember(Name = "exceptions")]
		public List<Exception> Exceptions { get; set; }

        /// <summary>
        /// A string representing what was happening in the application at the 
        /// time of the error. This string could be used for grouping purposes, 
        /// depending on the event.
        /// Usually this would represent the controller and action in a server 
        /// based project. It could represent the screen that the user was 
        /// interacting with in a client side project.
        /// For example,
        ///   * On Ruby on Rails the context could be controller#action
        ///   * In Android, the context could be the top most Activity.
        ///   * In iOS, the context could be the name of the top most UIViewController
        /// </summary>
        [DataMember(Name = "context")]
        public string Context{get;set;}

		// All errors with the same groupingHash will be grouped together within
		// the bugsnag dashboard.
		// This gives a notifier more control as to how grouping should be
		// performed. We recommend including the errorClass of the exception in
		// here so a different class of error will be grouped separately.
		// (optional)
		[DataMember(Name = "groupingHash")]
		public string GroupingHash { get; set; }

		// The severity of the error. This can be set to:
		// - "error"   used when the app crashes
		// - "warning" used when Bugsnag.notify is called
		// - "info"    can be used in manual Bugsnag.notify calls
		// (optional, default "error", filtered)
		[DataMember(Name = "severity")]
		public string Severity { get; set; }

		// Information about the user affected by the crash.
		// These fields are optional but highly recommended.
		[DataMember(Name = "user")]
		public UserInfo User { get; set; }

		// Information about the app that crashed.
		// These fields are optional but highly recommended
		[DataMember(Name = "app")]
		public AppInfo ApplicationInformation { get; set; }

		// Information about the computer/device running the app
		// These fields are optional but highly recommended
		[DataMember(Name = "device")]
		public Device DeviceInformation { get; set; }

        /// <summary>
        /// An object containing any further data you wish to attach to this error
        /// event. This should contain one or more objects, with each object being
        /// displayed in its own tab on the event details on the Bugsnag website.
        /// (Optional).
        /// </summary>
        [DataMember(Name = "metaData")]
        public object ExtraData{get;set;}
    }
}