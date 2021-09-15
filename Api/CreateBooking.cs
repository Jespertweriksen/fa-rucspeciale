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

namespace RUCSpecialeFunctionProject
{
    public static class CreateBooking
    {
        [FunctionName("CreateBooking")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [ServiceBus(queueOrTopicName: ServiceBusQueues.UserHandlerQueue, Connection = "SBConnection")] IAsyncCollector<dynamic> addToQueue,
            ILogger log)
        {
            var id = Guid.NewGuid().ToString("N");
            string email = req.Query["email"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            email ??= data?.email;

            // Validate
            if (!IsValidEmail(email)) return new BadRequestObjectResult("Signup failed: Incorrect email");

            var dummyEmail = "jtw@novicell.dk";
            string badRequestResponse = "exists";

            if(email == dummyEmail){
                return new BadRequestObjectResult(badRequestResponse); 
            }
            // 
            string responseMessage = string.IsNullOrEmpty(email)? 
            "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            : $"Hello, {email}. You have successfully signed up for X petition.";
            var message = email;
            ReservationModel reservationModel = new ReservationModel()
            {
                Email = email
            };

            // Add to queue and return response
            await addToQueue.AddAsync(reservationModel);
            log.LogDebug($"Sent message to the topic {ServiceBusQueues.UserHandlerQueue} with the message: {message}");
            return new OkObjectResult(responseMessage);
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
