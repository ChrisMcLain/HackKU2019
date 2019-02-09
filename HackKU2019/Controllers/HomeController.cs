using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackKU2019.Models;

namespace HackKU2019.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string twitter = null, string facebook = null, string instagram = null)
        {
            if (twitter != null)
            {
                var model = new GetTweets().PullTweets(twitter);
                var analyzer = new ContentAnalyzer();
                analyzer.AnalyzeContent(model.User);
                return View(model);
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}