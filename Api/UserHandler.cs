using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.Storage;
using RUCSpeciale;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RUCSpecialeFunctionProject
{
    public static class UserHandler
    {

        

        [FunctionName("UserHandler")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [ServiceBus(queueOrTopicName: ServiceBusQueues.UserHandlerQueue, Connection = "SBConnection")] IAsyncCollector<dynamic> addToQueue,
            ILogger log)
        {
            DataBaseHandler dataBaseHandler = new DataBaseHandler();
            var id = Guid.NewGuid().ToString("N");
            string email = req.Query["email"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            email ??= data?.email;
            string First_Name = req.Query["first_name"];
            First_Name ??= data?.First_Name;
            string Last_Name = req.Query["last_name"];
            First_Name ??= data?.First_Name;
            string Phone = req.Query["phone"];
            First_Name ??= data?.First_Name;
            string Postal = req.Query["postal"];
            First_Name ??= data?.First_Name;

            // Validate
            if (!IsValidEmail(email)) return new BadRequestObjectResult("Signup failed: Incorrect email");

           

            if (First_Name != null){
                ReservationModel reservationModel = new ReservationModel()
                {
                Email = email,
                First_Name = First_Name,
                Last_Name = Last_Name,
                Phone = Int32.Parse(Phone),
                Postal = Int32.Parse(Postal)
                };
                await addToQueue.AddAsync(reservationModel);
                return new OkObjectResult(reservationModel);
            }

            var guest = dataBaseHandler.rObject(email);
            if (guest == null){
                return new BadRequestObjectResult("Need more info");
            }else{
            ReservationModel reservationModel = new ReservationModel()
            {
                Id = guest.Id,
                Email = guest.Email,
                First_Name = guest.First_Name,
                Last_Name = guest.Last_Name,
                Phone = guest.Phone,
                Postal = guest.Postal
            };
            await addToQueue.AddAsync(reservationModel);
              // 
            string responseMessage = string.IsNullOrEmpty(email)? 
            "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            : $"Hello, {email}. You have successfully signed up for X petition.";

            return new OkObjectResult(responseMessage);
            }

            

        }

         public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
