using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RUCSpeciale
{
    public static class ConfirmBooking
    {
        [FunctionName("ConfirmBooking")]
        public static void Run([ServiceBusTrigger("rucspeciale-queue", Connection = "SBConnection")]ReservationModel reservationModel, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {reservationModel.Id} {reservationModel.Email}");
        }
    }
}
