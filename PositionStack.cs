using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using WebApiCall.Models;

namespace WebApiCall
{
  public static class PositionStack
  {
    [FunctionName("PositionStack")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "PositionStackApi/{Location}/{code}")] HttpRequest req,
        ILogger log, string location, string code)
    {
      log.LogInformation("Function starting to call the location api");
      // TEST URLS
      // http://localhost:7071/api/PositionStackApi/40.7638435,-73.9729691/1
      // http://localhost:7071/api/PositionStackApi/1600%20Pennsylvania%20Ave%20NW,%20Washington%20DC/2

      var json = string.Empty;
      var url = string.Empty;

      // 1 = LATITUDE LONG
      if (code.Equals("1"))
      {
         url = Environment.GetEnvironmentVariable("ReverseUrl") + location;
        try
        {
          json = await GetJsonAsync(url);
          Reverse reverserLoc = JsonConvert.DeserializeObject<Reverse>(json);
          return new OkObjectResult(reverserLoc.data[0]);
        }
        catch (Exception ex)
        {
          log.LogInformation(ex.Message);
        }

      } 
      // 2 = ADDRESS
      else if (code.Equals("2"))
      {
        url = Environment.GetEnvironmentVariable("ForwardUrl") + location;
        try
        {
          json = await GetJsonAsync(url);
          Forward reverserLoc = JsonConvert.DeserializeObject<Forward>(json);
          return new OkObjectResult(reverserLoc.data[0]);
        }
        catch (Exception ex)
        {
          log.LogInformation(ex.Message);
        }
      }
      return new OkObjectResult(json);
    }

    public static async Task<string> GetJsonAsync(string url)
    {
      HttpClient client = new HttpClient();
      var json = await client.GetStringAsync(url);
      return json;
    }
  }
}
