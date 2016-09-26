using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Texter.Models;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Texter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMessages()
        {
            var allMessages = Message.GetMessages();
            return View(allMessages);
        }

        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message newMessage)
        {
            newMessage.From = EnvironmentVariables.userPhone;
            if(newMessage.Body.ToLower() == "cat")
            {
                newMessage.Body = GetCatFact();
            }
            else if (newMessage.Body.ToUpper() == "BILL MURRAY")
            {
                newMessage.Body = GetBillBio();
            }
            else if (newMessage.Body.ToUpper() == "CHUCK NORRIS")
            {
                newMessage.Body = GetRoundhouseKickedInTheFace();
            }
            else
            {
                newMessage.Body = "It's always "+GetWeather(newMessage.Body)+ " in " + newMessage.Body;
            }
            ViewData["NumberSentTo"] = newMessage.To;
            newMessage.Send();
            return View("Index");
        }

        public string GetWeather(string City)
        {
            string weatherData = Weather.GetCityWeather(City);
            return weatherData;
        }

        public string GetCatFact()
        {
            var client = new RestClient("http://catfacts-api.appspot.com/api");
            var request = new RestRequest("facts", Method.GET);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            string fact = jsonResponse["facts"][0].ToString();
            return fact;
        }

        public string GetBillBio()
        {
            var client = new RestClient("https://en.wikipedia.org/w/");
            var request = new RestRequest("api.php?format=json&action=query&prop=extracts&exintro=&explaintext=&titles=Bill%20Murray", Method.GET);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            string fact = jsonResponse["query"]["pages"]["102994"]["extract"].ToString();
            return fact;
        }

        public string GetRoundhouseKickedInTheFace()
        {
            var client = new RestClient("http://api.icndb.com/");
            var request = new RestRequest("jokes/random", Method.GET);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            string fact = jsonResponse["value"]["joke"].ToString();
            return fact;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
