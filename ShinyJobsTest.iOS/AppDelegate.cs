using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Shiny.Jobs;
using Shiny;
using UIKit;
using Shiny.Hosting;

namespace ShinyJobsTest.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var job = new JobInfo(
                                nameof(DownloadClinicsJob),
                                typeof(DownloadClinicsJob),
                                false,
                                null,
                                InternetAccess.Any,
                                false,
                                false,
                                false);


            var host = HostBuilder.Create();
            host
                .Services
                .AddJob(job)
                .AddNotifications();

            host
                .Build()
                .Run();

            Xamarin.Forms.Forms.SetFlags("CarouselView_Experimental", "Expander_Experimental");
            global::Xamarin.Forms.Forms.Init();
                        
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        /// <inheritdoc/>
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken) => Host.Lifecycle.OnRegisteredForRemoteNotifications(deviceToken);
        /// <inheritdoc/>
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler) => Host.Lifecycle.OnDidReceiveRemoteNotification(userInfo, completionHandler);
        /// <inheritdoc/>
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error) => Host.Lifecycle.OnFailedToRegisterForRemoteNotifications(error);
        /// <inheritdoc/>
        public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, Action completionHandler) => Host.Lifecycle.OnHandleEventsForBackgroundUrl(sessionIdentifier, completionHandler);

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {
            return Host.Lifecycle.OnContinueUserActivity(userActivity, completionHandler);
        }

    }
}
