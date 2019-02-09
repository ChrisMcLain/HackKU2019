using System;
using System.Collections.Generic;
using Google.Cloud.Vision.V1;
using HackKU2019.Models;
using Remotion.Linq.Clauses;
using Tweetinvi.Core.Public.Models.Enum;

namespace HackKU2019
{
    public class ContentAnalyzer
    {
        public int AnalyzeContent(MainUser user)
        {
            int flags = 0;
            flags += TweetsCheck(user.Tweets,user.userInfo.UserID);
            flags += UserCheck(user.userInfo);
            flags += CheckFollowing(user);
            return flags;
        }

        private int CheckFollowing(MainUser user)
        {
            int flags = 0;
            foreach (var following in user.Following)
            {
                flags += CheckUser(following.UserInfo);
            }

            return flags;
        }
        private int UserCheck(User user)
        {
            int flags = 0;
            flags += CheckUser(user);
            return flags;
        }
        private int TweetsCheck(List<Tweet> content,string IdInQuestion)
        {
            int flags = 0;
            foreach (var tweet in content)
            {


                flags += VulgarWordCheck(tweet.Text);
                if (tweet.MediaUrls.Count > 0)
                {
                    foreach (var mediaUrl in tweet.MediaUrls)
                    {
                        flags += MediaCheck(mediaUrl);
                    }
                }

                if (IdInQuestion != tweet.UserCreateBy.UserID)
                {
                    CheckUser(tweet.UserCreateBy);
                }
            }

            return flags;
        }
        //checks tweet for how many words from vulgar word list they contain and adds flag for each one.
        private int VulgarWordCheck(string text)
        {
            int vulgarWords = 0;
            VulgarWordsList vulgarWordsList = new VulgarWordsList();
            foreach (var word in vulgarWordsList.vulgarWords)
            {
                if (text.ToLower().Contains(word.ToLower()))
                {
                    vulgarWords += 1;
                }
            }

            return vulgarWords;
        }

        //Uses google cloud vision to analyze an image from a post or profile picture for a trigger word
        private int MediaCheck(string url)
        {
            Image image = Image.FromUri(url);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> labels = client.DetectLabels(image);
            int mediaFlags = 0;
            VulgarWordsList vulgarWordsList = new VulgarWordsList();

            foreach (EntityAnnotation label in labels)
            {
                foreach (var badWord in vulgarWordsList.vulgarWords)
                {
                    if (label.Score > .5 && label.Description.ToLower().Contains(badWord.ToLower()))
                        mediaFlags++;
                }
            }

            return mediaFlags;
        }

        //checks the creator of the tweets on timeline for issues with their accounts
        private int CheckUser(User content)
        {
            int flags = 0;
            VulgarWordsList vulgarWordsList = new VulgarWordsList();

            foreach (var word in vulgarWordsList.vulgarWords)
            {
                if (content.Name.ToLower().Contains(word.ToLower()))
                {
                    flags += 1;
                }

                if (content.UserID.ToLower().Contains(word.ToLower()))
                {
                    flags += 1;
                }
                
                if (content.Bio.ToLower().Contains(word.ToLower()))
                {
                    flags += 1;
                }
            }

            if (content.ProfilePictureUrl != null)
            {
                flags += MediaCheck(content.ProfilePictureUrl);
            }

            if (content.BannerPictureUrl != null)
            {
                flags += MediaCheck(content.BannerPictureUrl);
            }
            return flags;
        }
    }
}