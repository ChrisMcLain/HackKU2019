using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using HackKU2019.Models;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Clauses;
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
            var tweets = Timeline.GetUserTimeline(handle);
            List<TwitterContent> twitterContents = new List<TwitterContent>();
            foreach (var tweet in tweets)
            {
                TwitterContent twitterContent = new TwitterContent
                    {Text = tweet.Text, AuthorName = tweet.CreatedBy.Name};
                twitterContents.Add(twitterContent);
            }

        }
    }
}