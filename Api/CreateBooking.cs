using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RUCSpeciale
{
    public static class CreateBooking
    {
        [FunctionName("CreateBooking")]
        public static async Task RunASync(
        [ServiceBusTrigger(ServiceBusQueues.UserHandlerQueue, Connection = "SBConnection")]ReservationModel reservationModel, 
        [ServiceBus(queueOrTopicName: ServiceBusQueues.BookingCreatedQueue, Connection = "SBConnection")] IAsyncCollector<dynamic> addToQueue,
            ILogger log)
        {
            log.LogInformation($"Userhandler handled email: {reservationModel.Id} {reservationModel.Email} {reservationModel.First_Name} {reservationModel.Last_Name} {reservationModel.Phone} {reservationModel.Postal}");
            DataBaseHandler dataBaseHandler = new DataBaseHandler();

            if (dataBaseHandler.rObject(reservationModel.Email) == null){
                dataBaseHandler.saveNewUser(reservationModel);
                log.LogInformation("Saving new user + reservation");
            }else{
                log.LogInformation("Saving existing user + reservartion");
            }



            var reservation = new ReservationModel
                {
                    Email = reservationModel.Email,
                    Id = reservationModel.Id
                };
            
               await addToQueue.AddAsync(reservation);
            
        }
    }
}
