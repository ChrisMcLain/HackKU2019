using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi;
using Tweetinvi.Models;

namespace HackKU2019
{
    public class GetTweets
    {

        
        public void pullTweets(string handle)
        {
            Keys keys = new Keys();
            Auth.SetUserCredentials(keys.ConsumerKey, keys.ConsumerSecret, keys.TokenKey, keys.TokenSecret);
            var tweets = Timeline.GetUserTimeline("bracciata");
            Console.WriteLine(tweets);

        }
    }
}