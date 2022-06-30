using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Refit;
using Octokit.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FunctionApp2
{
    public static class Function1
    {

        /// <summary>
        /// Summary of what the function does 
        /// </summary>
        /// <response code="200">
        ///   Indicates success. 
        /// </response>
        /// <response code="400">
        ///   Indicates a bad request. Returns what was wrong in the ErrorDetails portion of the response. 
        /// </response>
        /// <response code="500">
        ///   Indicates a server error. Returns what was wrong in the ErrorDetails portion of the response. 
        /// </response>
       // [ProducesResponseType(typeof(ApiResponse.Swagger.Response<object>), StatusCodes.Status200OK)] // replace object with your return type 
        [QueryStringParameter("requestid", "Optional (either here or in the body) - Minimum 30 characters", DataType = typeof(string), Required = false)]

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            //[RequestBodyType(typeof(req<object>), "body type description")] // replace object with your request type
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
