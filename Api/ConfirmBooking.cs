using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RUCSpeciale
{
    public static class ConfirmBooking
    {
        [FunctionName("ConfirmBooking")]
        public static async Task RunASync([ServiceBusTrigger(ServiceBusQueues.BookingCreatedQueue, Connection = "SBConnection")]ReservationModel reservationModel, ILogger log)
        {
             var reservation = new ReservationModel
                {
                    Email = reservationModel.Email,
                    Id = reservationModel.Id
                };

            log.LogInformation($"Confirmed booking for mail: {reservation.Id} {reservation.Email}  {reservation.First_Name}");
        }
    }
}
