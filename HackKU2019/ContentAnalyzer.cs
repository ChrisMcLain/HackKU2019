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
            TweetsCheck(user.Tweets,user.UserInfo.UserId);
            IssueFlagsReturnObj obj = CheckUser(user.UserInfo);
            user.Issues += obj.Issue;
            user.TotalFlags += obj.Flags;
            //updates all following with issues
               
            CheckFollowing(user);
            return flags;
        }

        private void CheckFollowing(MainUser user)
        {
            foreach (var following in user.Following)
            {
                 IssueFlagsReturnObj obj= CheckUser(following.UserInfo);
                 following.TotalFlags += obj.Flags;
                 following.Issue += obj.Issue;
            }

        }
        private int UserCheck(User user)
        {
            int flags = 0;
           IssueFlagsReturnObj returnObj=CheckUser(user);
           flags += returnObj.Flags;
            return flags;
        }
        private void TweetsCheck(List<Tweets> content,string idInQuestion)
        {
            foreach (var tweet in content)
            {

                IssueFlagsReturnObj returnObj =VulgarWordCheck(tweet.Text);
                tweet.TotalFlags += returnObj.Flags;
                tweet.Issue += returnObj.Issue;

                if (tweet.MediaUrls.Count > 0)
                {
                    foreach (var mediaUrl in tweet.MediaUrls)
                    {
                        IssueFlagsReturnObj obj = MediaCheck(mediaUrl);
                        tweet.TotalFlags += obj.Flags;
                        tweet.Issue += obj.Issue;
                    }
                }

                if (idInQuestion != tweet.UserCreateBy.UserId)
                {
                    CheckUser(tweet.UserCreateBy);
                }
            }

        }
        //checks tweet for how many words from vulgar word list they contain and adds flag for each one.
        private IssueFlagsReturnObj VulgarWordCheck(string text)
        {   
            int flags = 0;
            string issues = "";
            VulgarWordsList vulgarWordsList = new VulgarWordsList();
            foreach (var word in vulgarWordsList.VulgarWords)
            {
                if (text.ToLower().Contains(word.ToLower()))
                {
                    flags += 1;
                }
            }

            IssueFlagsReturnObj returnObj = new IssueFlagsReturnObj {Flags = flags, Issue = issues};
            return returnObj;        }

        //Uses google cloud vision to analyze an image from a post or profile picture for a trigger word
        private IssueFlagsReturnObj MediaCheck(string url)
        {
            int flags = 0;
            string issues = "";
            Image image = Image.FromUri(url);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> labels = client.DetectLabels(image);
            VulgarWordsList vulgarWordsList = new VulgarWordsList();

            foreach (EntityAnnotation label in labels)
            {
                foreach (var badWord in vulgarWordsList.VulgarWords)
                {
                    if (label.Score > .5 && label.Description.ToLower().Contains(badWord.ToLower()))
                        flags++;
                }
            }

            IssueFlagsReturnObj returnObj = new IssueFlagsReturnObj {Flags = flags, Issue = issues};
            return returnObj;
            
        }

        //checks the creator of the tweets on timeline for issues with their accounts
        private IssueFlagsReturnObj CheckUser(User content)
        {
            int flags = 0;
            string issues = "";
            VulgarWordsList vulgarWordsList = new VulgarWordsList();

            foreach (var word in vulgarWordsList.VulgarWords)
            {
                if (content.Name.ToLower().Contains(word.ToLower()))
                {
                    flags += 1;
                }

                if (content.UserId.ToLower().Contains(word.ToLower()))
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
                IssueFlagsReturnObj obj= MediaCheck(content.ProfilePictureUrl);
                flags += obj.Flags;
                issues += obj.Issue;

            }

            if (content.BannerPictureUrl != null)
            {
                IssueFlagsReturnObj obj= MediaCheck(content.BannerPictureUrl);
                flags += obj.Flags;
                issues += obj.Issue;            }

            IssueFlagsReturnObj returnObj = new IssueFlagsReturnObj {Flags = flags, Issue = issues};
            return returnObj;
        }
    }

    class IssueFlagsReturnObj
    {
        public int Flags { get; set; }
        public string Issue { get; set; }
    }
}