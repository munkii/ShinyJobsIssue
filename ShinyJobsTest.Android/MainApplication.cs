using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony.Data;
using Android.Views;
using Android.Widget;
using Shiny.Notifications;
using Shiny;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shiny.Jobs;

namespace ShinyJobsTest.Droid
{
    [Application(Debuggable = true)]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer)
          : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

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
            host.Services
                .AddJob(job)
                .AddNotifications();

            host
                .Build()
                .Run();
        }
    }
}