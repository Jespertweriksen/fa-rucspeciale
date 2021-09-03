using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RUCSpeciale
{
    public static class UserHandler
    {
        [FunctionName("UserHandler")]
        public static async Task RunASync(
        [ServiceBusTrigger(ServiceBusQueues.UserHandlerQueue, Connection = "SBConnection")]ReservationModel reservationModel, 
        [ServiceBus(queueOrTopicName: ServiceBusQueues.BookingCreatedQueue, Connection = "SBConnection")] IAsyncCollector<dynamic> addToQueue,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {reservationModel.Id} {reservationModel.Email}");

            var reservation = new ReservationModel
                {
                    Email = reservationModel.Email,
                    Id = reservationModel.Id
                };
            
               await addToQueue.AddAsync(reservation);
            
        }
    }
}
