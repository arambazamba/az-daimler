using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Integrations
{
    public class processQueue
    {
        [FunctionName("processQueue")]
        public void Run([QueueTrigger("scaling-queue", Connection = "AzureStorageAccount")]string item, ILogger log)
        {
            System.Threading.Thread.Sleep(300);
            log.LogInformation($"C# Queue trigger function processed: {item}");
        }
    }
}
