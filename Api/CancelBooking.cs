using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RUCSpeciale
{
    public static class CancelBooking
    {
        // [FunctionName("CancelBooking")]
        // public static void Run([ServiceBusTrigger("rucspeciale-queue", Connection = "SBConnection")]string myQueueItem, ILogger log)
        // {
        //     log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        // }
    }
}
