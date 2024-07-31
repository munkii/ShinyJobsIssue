
namespace ShinyJobsTest
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    //using Microsoft.Extensions.Logging;
    //using ProjectBreatheApp.Data.Models.DB;
    //using ProjectBreatheApp.Http.Calls;
    //using ProjectBreatheApp.Interfaces;
    //using ProjectBreatheApp.Interfaces.Clinical;
    using Shiny.Jobs;
    using Shiny.Notifications;

    public class DownloadClinicsJob : Job
    {
        private readonly INotificationManager notificationManager;
        private readonly ILogger<DownloadClinicsJob> logger;

        //private readonly IBreatheServices breatheServices;
        //private readonly IClinicalService clinicalService;
        //private readonly IConfigurationService configurationService;
        //private readonly IB2CConfigurationService b2CConfigurationService;
        ////private DateTime? lastRunTimeUtc;
        ////private int executionCount = 0;

        public DownloadClinicsJob(
            INotificationManager notificationManager,
            ILogger<DownloadClinicsJob> logger)
                    : base(logger)
        {
            this.notificationManager = notificationManager;
            this.logger = logger;

            ////IBreatheServices breatheServices,
            ////IClinicalService clinicalService,
            ////IConfigurationService configurationService,
            ////IB2CConfigurationService b2CConfigurationService,

            ////this.breatheServices = breatheServices;
            ////this.clinicalService = clinicalService;
            ////this.configurationService = configurationService;
            ////this.b2CConfigurationService = b2CConfigurationService;
            ////this.MinimumTime = TimeSpan.FromMinutes(10);
        }

        protected override async Task Run(CancellationToken cancelToken)
        {
            try
            {
                string contextData;

                if (this.LastRunTime.HasValue == false)
                {
                    contextData = "firstTime";
                }
                else
                {
                    ////this.breatheServices.LoggingService.LogInfo("DownloadClinicsJob not first time");
                    contextData = $"timeSincePreviousSecs={(DateTimeOffset.UtcNow - this.LastRunTime.Value).TotalSeconds}";
                }

                System.Diagnostics.Debug.WriteLine("Running DownloadClinicsJob");

                Notification n = new Notification()
                                        {
                                            Title = "From Job",
                                            Message = contextData,
                                        };


                var state = await this.notificationManager.RequestRequiredAccess(n);
                
                await this.notificationManager.Send(n);

                ////var getClinicsCall =
                ////        new GetClinicsCall(
                ////                        this.breatheServices,
                ////                        this.clinicalService,
                ////                        this.configurationService,
                ////                        this.b2CConfigurationService,
                ////                        contextData);

                ////var clinicsResponse = await getClinicsCall.Run();

                ////if (clinicsResponse.Successful)
                ////{
                ////    await this.breatheServices.DataService.DeleteAsync<Clinic>(c => true, false, true);
                ////    await this.breatheServices.DataService.SaveValuesAsync<Clinic>(clinicsResponse.Result, true);
                ////}
            }
            catch (Exception ex)
            {
                ////this.breatheServices.LoggingService.LogInfo("Exception in DownloadClinicsJob: " + ex.Message);
            }
        }
    }
}
