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
            var userCredentials = Auth.CreateCredentials("CONSUMER_KEY", "CONSUMER_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");
            return User.GetAuthenticatedUser(userCredentials);
        }
        
        public async void pullTweets()
        {
            var authUser = getUser();
        }
    }
}