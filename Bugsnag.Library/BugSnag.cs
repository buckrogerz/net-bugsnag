// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BugSnag.cs" company="n/a">
//   2014
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bugsnag.Library
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Web;
	using Data;
	using ServiceStack.Text;

	/// <summary>
	/// .NET notifier for BugSnag error reporting
	/// </summary>
	public class BugSnag
	{
		#region Constants

		/// <summary>
		/// Http based url for reporting errors to BugSnag
		/// </summary>
		private const string HTTPURL = "http://notify.bugsnag.com";

		/// <summary>
		/// Https based url for reporting errors to BugSnag
		/// </summary>
		private const string HTTPSURL = "https://notify.bugsnag.com";
		#endregion Constants

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="BugSnag"/> class.
		/// </summary>
		/// <param name="apiKey">The API key.</param>
		public BugSnag(string apiKey) : this()
		{
			this.APIKey = apiKey;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BugSnag"/> class.
		/// </summary>
		public BugSnag()
		{
			this.CommonInit();
		}
		#endregion Constructors

		#region Properties

		/// <summary>
		/// The APIKey for the project
		/// </summary>
		public string APIKey { get; set; }

		/// <summary>
		/// The current release stage for the application 
		/// (development/test/production)
		/// </summary>
		public string ReleaseStage { get; set; }

		/// <summary>
		/// If this is true, the plugin should notify Bugsnag using SSL
		/// </summary>
		public bool UseSSL { get; set; }

		/// <summary>
		/// The version number of the application which generated the error
		/// </summary>
		public string ApplicationVersion { get; set; }

		/// <summary>
		/// The operating system version of the client that the error was 
		/// generated on.
		/// </summary>
		public string OSVersion { get; set; }
		#endregion Properties

		#region public methods

		/// <summary>
		/// Gathers information for the last error (if any error is available) 
		/// and reports it to BugSnag using information from the application
		/// configuration file and other defaults
		/// </summary>
		/// <param name="extraData">Any extra data to pass when reporting this error</param>
		public void Notify(object extraData = null)
		{
			// If we're a web application, we can report errors automagically
			if (HttpContext.Current != null)
			{
				// If we have errors...
				if (HttpContext.Current.AllErrors != null && HttpContext.Current.AllErrors.Any())
				{
					// ... go through all of the errors and report them
					var events = new List<Event>
					{
						this.ProcessExceptions(
							HttpContext.Current.AllErrors.ToList(), 
							HttpContext.Current.Request.Path, 
							string.Empty, 
							SeverityLevel.Error, 
							null, 
							string.Empty, 
							extraData)
					};

					// Send the notification:
					var notification = new ErrorNotification
					{
						Api_Key = this.APIKey, 
						Events = events
					};

					this.SendNotification(notification, this.UseSSL);
				}
			}

			// If we're not a web application, we're SOL ATM (call another method)
		}

		/// <summary>
		/// Report a single exception to BugSnag using defaults
		/// </summary>
		/// <param name="ex">The exception to report</param>
		/// <param name="extraData">Data that will be sent as meta-data along with this error</param>
		public void Notify(System.Exception ex, object extraData = null)
		{
			this.Notify(ex, string.Empty, string.Empty, SeverityLevel.Error, null, string.Empty, extraData);
		}

		/// <summary>
		/// Report a list of exceptions to BugSnag
		/// </summary>
		/// <param name="exceptionList">The list of Exceptions to report</param>
		/// <param name="extraData">Data that will be sent as meta-data along with this error</param>
		public void Notify(IEnumerable<System.Exception> exceptionList, object extraData = null)
		{
			this.Notify(exceptionList, string.Empty, string.Empty, SeverityLevel.Error, null, string.Empty, extraData);
		}

		/// <summary>
		/// Notifies the specified ex.
		/// </summary>
		/// <param name="ex">The exception to report</param>
		/// <param name="user">The user.</param>
		/// <param name="extraData">Data that will be sent as meta-data along with this error</param>
		public void Notify(System.Exception ex, UserInfo user, object extraData = null)
		{
			var exceptionList = new List<System.Exception> { ex };

			this.Notify(exceptionList, string.Empty, string.Empty, SeverityLevel.Error, user, string.Empty, extraData);
		}

		/// <summary>
		/// Notifies the specified ex.
		/// </summary>
		/// <param name="ex">The exception to report</param>
		/// <param name="context">The context.</param>
		/// <param name="user">The user.</param>
		/// <param name="extraData">The extra data.</param>
		public void Notify(System.Exception ex, string context, UserInfo user, object extraData = null)
		{
			var exceptionList = new List<System.Exception> { ex };

			this.Notify(exceptionList, context, string.Empty, SeverityLevel.Error, user, string.Empty, extraData);
		}

		/// <summary>
		/// Notifies the specified ex.
		/// </summary>
		/// <param name="ex">The exception to report</param>
		/// <param name="context">The context.</param>
		/// <param name="user">The user.</param>
		/// <param name="hostName">Name of the host.</param>
		/// <param name="extraData">The extra data.</param>
		public void Notify(System.Exception ex, string context, UserInfo user, string hostName, object extraData = null)
		{
			var exceptionList = new List<System.Exception> { ex };

			this.Notify(exceptionList, context, string.Empty, SeverityLevel.Error, user, hostName, extraData);
		}

		/// <summary>
		/// Report an exception to Bugsnag with other per-request or per-session data
		/// </summary>
		/// <param name="ex">
		/// The exception to report
		/// </param>
		/// <param name="context">
		/// The context that is currently active in the application
		/// </param>
		/// <param name="groupingStr">
		/// </param>
		/// <param name="severity">
		/// </param>
		/// <param name="user">
		/// </param>
		/// <param name="hostName">
		/// </param>
		/// <param name="extraData">
		/// Data that will be sent as meta-data along with this error
		/// </param>
		public void Notify(System.Exception ex, string context, string groupingStr, SeverityLevel severity, UserInfo user, string hostName, object extraData = null)
		{
			var exceptionList = new List<System.Exception> { ex };

			this.Notify(exceptionList, context, groupingStr, severity, user, hostName, extraData);
		}

		/// <summary>
		/// Report a list of exceptions to Bugsnag with other per-request or per-session data
		/// </summary>
		/// <param name="exceptionList">
		/// The list of exceptions to report
		/// </param>
		/// <param name="context">
		/// The context that is currently active in the application
		/// </param>
		/// <param name="groupingStr">
		/// </param>
		/// <param name="severity">
		/// </param>
		/// <param name="user">
		/// </param>
		/// <param name="hostName">
		/// </param>
		/// <param name="extraData">
		/// Data that will be sent as meta-data along with every error
		/// </param>
		public void Notify(IEnumerable<System.Exception> exceptionList, string context, string groupingStr, SeverityLevel severity, UserInfo user, string hostName, object extraData = null)
		{
			// Add an event for this exception list:
			var events = new List<Event>();
			events.Add(this.ProcessExceptions(exceptionList, context, groupingStr, severity, user, hostName, extraData));

			// Send the notification:
			var notification = new ErrorNotification
			{
				Api_Key = this.APIKey, 
				Events = events
			};

			this.SendNotification(notification, this.UseSSL);
		}
		#endregion public methods

		#region private methods

		/// <summary>
		/// Process a list of exceptions into an event
		/// </summary>
		/// <param name="exceptionList">A list of exceptions</param>
		/// <param name="context">The context for the event</param>
		/// <param name="groupingStr"></param>
		/// <param name="severity"></param>
		/// <param name="user"></param>
		/// <param name="hostName"></param>
		/// <param name="extraData">Extra data to annotate on the event</param>
		/// <returns>the built event</returns>
		private Event ProcessExceptions(IEnumerable<System.Exception> exceptionList, string context, string groupingStr, SeverityLevel severity, UserInfo user, string hostName, object extraData)
		{
			// Create an event to return
			var retval = new Event
			{
				Context = context, 
				GroupingHash = groupingStr, 
				Severity = severity != SeverityLevel.None ? severity.ToString().ToLower() : SeverityLevel.Error.ToString().ToLower(), 
				User = user ?? new UserInfo(), 
				ExtraData = extraData, ApplicationInformation = new AppInfo
				{
					Version = this.ApplicationVersion,
					ReleaseStage = this.ReleaseStage
				}, 
				DeviceInformation = new Device
				{
					OSVersion = this.OSVersion, 
					HostName = hostName
				}
			};

			// Our list of exceptions:
			var exceptions = new List<Exception>();

			// For each exception passed...
			foreach (var ex in exceptionList)
			{
				List<Stacktrace> stacktraces;

				// ... Create a list of stacktraces
				// This may not be the best way to get this information:
				// http://blogs.msdn.com/b/jmstall/archive/2005/03/20/399287.aspx
				if (!string.IsNullOrEmpty(ex.StackTrace))
				{
					stacktraces = (from item in new System.Diagnostics.StackTrace(ex, true).GetFrames()
						select new Stacktrace
						{
							File = item.GetFileName() ?? item.GetType().Name, 
							LineNumber = item.GetFileLineNumber(), 
							Method = item.GetMethod().Name
						}).ToList();
				}
				else
				{
					stacktraces = new List<Stacktrace>();
					stacktraces.Add(new Stacktrace
					{
						File = "No file available", 
						LineNumber = 0, 
						Method = "No stack trace information available"
					});
				}

				// Add a new exception, and use the stacktrace list:
				exceptions.Add(new Exception
				{
					ErrorClass = (ex.TargetSite != null && string.IsNullOrEmpty(ex.TargetSite.Name)) ? ex.TargetSite.Name : "Unknown", 
					Message = ex.Message, 
					Stacktrace = stacktraces
				});
			}

			// Set our list of exceptions
			retval.Exceptions = exceptions;

			// Return the event:
			return retval;
		}

		/// <summary>
		/// Sends current set of events to BugSnag via a JSON post
		/// </summary>
		/// <param name="notification">The notification to send</param>
		/// <param name="useSSL">Indicates the post should use SSL when sending JSON data</param>
		private void SendNotification(ErrorNotification notification, bool useSSL)
		{
			string serializedJSON = notification.SerializeToString();

			// Create a byte array:
			byte[] byteArray = Encoding.UTF8.GetBytes(serializedJSON);

			// Post JSON to server:
			WebRequest request = useSSL ? WebRequest.Create(HTTPSURL) : WebRequest.Create(HTTPURL);
			request.Method = WebRequestMethods.Http.Post;
			request.ContentType = "application/json";
			request.ContentLength = byteArray.Length;

			Stream dataStream = request.GetRequestStream();
			dataStream.Write(byteArray, 0, byteArray.Length);
			dataStream.Close();

			// Get the response.  See https://bugsnag.com/docs/notifier-api for response codes
			// ReSharper disable once UnusedVariable
			var response = request.GetResponse();
		}

		/// <summary>
		/// Commons the initialize.
		/// </summary>
		private void CommonInit()
		{
			// SSL is set to 'off' by default
			this.UseSSL = false;
			this.OSVersion = string.Empty;
			this.APIKey = string.Empty;
			this.ApplicationVersion = string.Empty;
		}
		#endregion private methods
	}
}