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
        public IActionResult Index(string twitter = null, string options = "desc_time")
        {
            if (twitter != null)
            {
                try
                {
                    var model = new GetTweets().PullTweets(twitter, options);
                    var analyzer = new ContentAnalyzer();
                    analyzer.AnalyzeContent(model.User);
                    
                    model.Sorting = options;
                    
                    if (options.Equals("asc_time"))
                    {
                        model.Tweets.Reverse();
                    }
                    else if (options.Equals("desc_severity"))
                    {
                        model.Tweets = model.Tweets.OrderByDescending(n => n.TotalFlags).ToList();
                    } 
                    else if (options.Equals("asc_severity"))
                    {
                        model.Tweets = model.Tweets.OrderBy(n => n.TotalFlags).ToList();
                    }
                    
                    return View(model);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.ToString());
                    return View(new ResultsModel { Sorting = options, Error = "User not found or access denied.", User = new MainUser { UserInfo = new User { UserId = twitter }} });
                }
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