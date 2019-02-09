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
        IAuthenticatedUser getUser()
        {
            Keys keys = new Keys();
            var userCredentials = Auth.CreateCredentials(keys.ConsumerKey, keys.ConsumerSecret, keys.TokenKey, keys.TokenSecret);
            return User.GetAuthenticatedUser(userCredentials);
        }
        
        public void pullTweets(string handle)
        {
            var authUser = getUser();
            var tweets = Timeline.GetUserTimeline("bracciata");
            Console.WriteLine(tweets);

        }
    }
}