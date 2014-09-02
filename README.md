.NET Bugsnag
===========

.NET Bugsnag is a notifier library for http://bugsnag.com.  It includes support for logging Events, Exceptions, Stacktrace information, and meta information.  

[![Build status](https://ci.appveyor.com/api/projects/status/jncmi5xfno8u99m9)](https://ci.appveyor.com/project/buckrogerz/net-bugsnag)

For more information about the examples below, you can visit https://bugsnag.com/docs/notifier-api for a full reference.

Quick Start
-----------

Install the [NuGet package](https://www.nuget.org/packages/Bugsnag.Library/):
```powershell
Install-Package Bugsnag.Library
```

Next, you will need to provide .NET BugSnag with your API key.  

```CSharp
BugSnag bs = new BugSnag()
{
    apiKey = "YOUR_API_KEY"
};
```

Use the library :)

Examples
-----------

### Logging all errors in a web application

If you have a web application, this is the simplest way to add exception logging.  In your global.asax, add the following to your application error handler (or create the handler if it doesn't already exist):

```CSharp
protected void Application_Error(object sender, EventArgs e)
{
    //  Create a new Bugsnag notifier
    BugSnag bs = new BugSnag();

    //  Notify.  This will get configuration from the web.config
    //  and gather all known errors and report them.  It's just that simple!
    bs.Notify();
}
```
        
### Logging a single exception

Logging an exception in your application is also incredibly easy.  

```CSharp
try
{
   // Some code that causes an exception
}
catch(SomeSpecificExceptionType ex)
{
   BugSnag bs = new BugSnag();
   bs.Notify(ex, null);
}
```

### Including extra data

If you'd like to include extra data when you log exceptions, just pass it in the 'extra data' parameter.  It can be a simple object with a few properties, or an incredibly complex set of meta data
The class you create to store the extra data must be serizable.

```CSharp
try
{
   // Some code that causes an exception
}
catch(SomeSpecificExceptionType ex)
{

   BugSnag bs = new BugSnag();
   bs.Notify(ex, new
   {
       OtherReallyCoolData = new
       {
           color = "Yellow",
           mood = "Mellow"
       }
   });
}
```
        

