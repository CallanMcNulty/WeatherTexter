using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Texter.Models;

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
            newMessage.Body = "It's always "+GetWeather(newMessage.Body)+ " in " + newMessage.Body;
            ViewData["NumberSentTo"] = newMessage.To;
            newMessage.Send();
            return View("Index");
        }

        public string GetWeather(string City)
        {
            string weatherData = Weather.GetCityWeather(City);
            return weatherData;
        }
    }
}
