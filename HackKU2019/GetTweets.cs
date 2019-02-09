using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Google.Cloud.Vision.V1;
using HackKU2019.Models;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Clauses;
using Tweetinvi;
using Tweetinvi.Core.Public.Models.Enum;
using Tweetinvi.Models;

namespace HackKU2019
{
    public class GetTweets
    {
        public void pullTweets(string handle)
        {
            Keys keys = new Keys();
            Auth.SetUserCredentials(keys.ConsumerKey, keys.ConsumerSecret, keys.TokenKey, keys.TokenSecret);
            formatHandle(handle);
            if (checkUserExists(handle))
            {
                List<string> followingNames = new List<string>();
                List<string> followingUsersBios=new List<string>();
                List<string> followingUsersProfPics=new List<string>();
                List<string> followingUsersBannerPics=new List<string>();
                List<string> followingUsersIds=new List<string>();
                var user = User.GetUserFromScreenName(handle);
                foreach (var following in user.Friends)
                {
                    followingNames.Add(following.Name);
                    followingUsersBios.Add(following.Description);
                    followingUsersProfPics.Add(following.ProfileImageUrl);
                    followingUsersBannerPics.Add(following.ProfileBannerURL);
                    followingUsersIds.Add(following.UserIdentifier.ToString());
                }
                var tweets = Timeline.GetUserTimeline(handle);
                List<TwitterContent> twitterContents = new List<TwitterContent>();
                
                foreach (var tweet in tweets)
                {
                    List<string> mediaUrls = new List<string>();
                    try
                    {
                        foreach (var media in tweet.Media)
                        {
                            //can't analyze videos using google cloud vision
                            if (media.MediaType != MediaType.VideoMp4.ToString())
                            {
                                //Adds the links of all tweets images
                                mediaUrls.Add(media.MediaURL);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    //Adds tweets to list from above
                    TwitterContent twitterContent = new TwitterContent
                    {
                        TargetUsername = handle.Substring(1),
                        Text = tweet.Text,
                        AuthorName = tweet.CreatedBy.Name,
                        MediaUrls = mediaUrls,
                        Platform = Platforms.Twitter,
                        CreatorUserName = tweet.CreatedBy.Name,
                        CreatorBio = tweet.CreatedBy.Description,
                        CreatorBackgroundPictureURL = tweet.CreatedBy.ProfileBannerURL
                        ,CreatorUserId = tweet.CreatedBy.UserIdentifier.ToString()
                    };

                    twitterContents.Add(twitterContent);

                }
            }
        }

        //Assuming they did not add the @ symbol adds it for them
        public String formatHandle(string handle)
        {
            if (handle[0] != '@')
            {
                handle = handle.Insert(0, "@");
            }

            return handle;
        }

        //Checks if a user exits by checking if the return for their name is null
        public bool checkUserExists(string handle)
        {
            var user = User.GetUserFromScreenName(handle);
            return user != null;
        }
        
        
    }
}