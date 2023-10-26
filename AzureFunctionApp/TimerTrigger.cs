using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;

namespace AzureFunctionApp
{
    public class TimerTrigger
    {

        private readonly IOrganizationService _service;
        public TimerTrigger(IOrganizationService service)
        {
            _service = service;
        }

        [FunctionName("TimerTrigger")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ExecutionContext context, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
