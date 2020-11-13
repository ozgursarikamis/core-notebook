using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AwesomeShop.QueueLibrary.Messages;
using AwesomeShop.QueueLibrary.QueueConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AwesomeShop.Web.Models;

namespace AwesomeShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQueueCommunicator _communicator;

        public HomeController(ILogger<HomeController> logger, IQueueCommunicator communicator)
        {
            _logger = logger;
            _communicator = communicator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(string contactName, string emailAddress)
        {
            var thankYouEmail = new SendEmailCommand
            {
                To = emailAddress,
                Subject = "Some subject",
                Body = "We will contact you shortly"
            };
            await _communicator.SendAsync(thankYouEmail);

            var adminEmail = new SendEmailCommand
            {
                To = "9678df77-5884-47ab-af1d-947a96921fd7@mailinator.com",
                Subject = "Some subject",
                Body = $"{contactName}  has react out to you..."
            };
            await _communicator.SendAsync(adminEmail);

            ViewBag.Message = "Thank you for your message";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
