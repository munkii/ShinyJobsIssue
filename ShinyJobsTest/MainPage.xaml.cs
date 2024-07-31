using Microsoft.Extensions.DependencyInjection;
using Shiny.Jobs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShinyJobsTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var jobManager = Shiny.Hosting.Host.Current.Services.GetRequiredService<IJobManager>();

            jobManager.RequestAccess();

            ////var job = jobManager.GetJob(nameof(DownloadClinicsJob));

            ////jobManager.Run(nameof(DownloadClinicsJob));
        }
    }
}
