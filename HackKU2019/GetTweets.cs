using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Google.Cloud.Vision.V1;
using HackKU2019.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC;
using Remotion.Linq.Clauses;
using Tweetinvi;
using Tweetinvi.Core.Public.Models.Enum;
using Tweetinvi.Models;
using IUser = Tweetinvi.Models.IUser;
using Tweet = Tweetinvi.Tweet;
using User = Tweetinvi.User;
using Google.Cloud.Translation.V2;

namespace HackKU2019
{
    public class GetTweets
    {
        public ResultsModel PullTweets(string handle)
        {
            Keys keys = new Keys();
            Auth.SetUserCredentials(keys.ConsumerKey, keys.ConsumerSecret, keys.TokenKey, keys.TokenSecret);
            FormatHandle(handle);
            
            if (CheckUserExists(handle))
            {
                var user = User.GetUserFromScreenName(handle);
                List<Followed> following = new List<Followed>();

                if (user.Friends != null)
                {
                    foreach (var followed in user.Friends)
                    {
                        Followed followedObj = new Followed
                        {
                            UserInfo = new Models.User
                            {
                                BannerPictureUrl = followed.ProfileBannerURL, Bio = followed.Description,
                                Name = followed.Name, ProfilePictureUrl = followed.ProfileImageUrl,
                                UserId = followed.UserIdentifier.ToString()
                            },
                            Issue = ""
                        };
                        following.Add(followedObj);
                    }
                }

                //we will return this
                var tweets = Timeline.GetUserTimeline(handle);
                List<Tweets> twitterContents = new List<Tweets>();

                if (tweets != null)
                {
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
                        var thisTweet = new Tweets
                        {
                            UserCreateBy = new Models.User
                            {
                                UserId = tweet.CreatedBy.UserIdentifier.ToString(),
                                BannerPictureUrl = tweet.CreatedBy.ProfileBannerURL,
                                Bio = tweet.CreatedBy.Description, Name = tweet.CreatedBy.Name,
                                ProfilePictureUrl = tweet.CreatedBy.ProfileImageUrl
                            },
                            Text = tweet.Text, Issue = ""
                        };

                        twitterContents.Add(thisTweet);
                    }
                }

                //We will return twitter contents here along with the user class from above to be analyzed
                MainUser checkedUser = new MainUser
                {
                    UserInfo = new Models.User
                    {
                        UserId = user.UserIdentifier.ToString(), BannerPictureUrl = user.ProfileBannerURL,Bio=user.Description,Name=user.Name,ProfilePictureUrl = user.ProfileImageUrl
                    }, 
                    Tweets = twitterContents,Following = following
                };

                return new ResultsModel
                {
                    User = checkedUser,
                    Tweets = twitterContents
                };
            }

            return null;
        }

        public string DetectLanguage(string tweetText)
        {
            TranslationClient client = TranslationClient.Create();
            var detection = client.DetectLanguage(text: tweetText);
            return detection.Language;
        }

        //Assuming they did not add the @ symbol adds it for them
        public String FormatHandle(string handle)
        {
            if (handle[0] != '@')
            {
                handle = handle.Insert(0, "@");
            }

            return handle;
        }

        //Checks if a user exits by checking if the return for their name is null
        public bool CheckUserExists(string handle)
        {
            var user = User.GetUserFromScreenName(handle);
            return user != null;
        }
    }
}