using System;
using System.Collections.Generic;
using Google.Cloud.Vision.V1;
using HackKU2019.Models;
using Tweetinvi.Core.Public.Models.Enum;

namespace HackKU2019
{
    public class ContentAnalyzer
    {
        public int AnalyzeContent(IContent content,IUser user)
        {
            int flags = 0;
            flags += TweetsCheck(content,user.UserID);
            flags += UserCheck(user);
            return flags;
        }

        private int UserCheck(IUser user)
        {
            int flags = 0;
            flags += CheckUserVulgarWords(user);
            flags += CheckUserNaughtyImages(user);

            return flags;
        }

        private int CheckUserNaughtyImages(IUser user)
        {
            int flags = 0;
            flags += MediaCheck(user.BannerPictureUrl);
            flags += MediaCheck(user.ProfilePictureUrl);
            foreach (var following in user.Following)
            {
                flags += MediaCheck(following.followingUserProfilePictureUrl);
                flags += MediaCheck(following.followingUsersProfileBannerUrl);
            }

            return flags;
        }
        private int CheckUserVulgarWords(IUser user)
        {
            int flags = 0;
            
            VulgarWordsList vulgarWordsList = new VulgarWordsList();
            foreach (var word in vulgarWordsList.vulgarWords)
            {
                if (user.Name.ToLower().Contains(word.ToLower()))
                {
                    flags += 1;
                }
                if (user.Bio.ToLower().Contains(word.ToLower()))
                {
                    flags += 1;
                }
                if (user.UserID.ToLower().Contains(word.ToLower()))
                {
                    flags += 1;
                }
                //checks all of the people the user is following for issues with their accounts
                
                foreach (var followed in user.Following)
                {
                    if (followed.followingName.ToLower().Contains(word.ToLower()))
                    {
                        flags += 1;
                    }
                    if (followed.followingUserId.ToLower().Contains(word.ToLower()))
                    {
                        flags += 1;
                    }
                    if (followed.followingUserBio.ToLower().Contains(word.ToLower()))
                    {
                        flags += 1;
                    }
                }
            }

            return flags;
        }
        private int TweetsCheck(IContent content,string IdInQuestion)
        {
            int flags = 0;
            flags += VulgarWordCheck(content.Text);
            if (content.MediaUrls.Count > 0)
            {
                foreach (var mediaUrl in content.MediaUrls)
                {
                    flags += MediaCheck(mediaUrl);
                }
            }

            if (IdInQuestion != content.CreatorUserId)
            {
                CheckUser(content);
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
        private int CheckUser(IContent content)
        {
            int flags = 0;
            VulgarWordsList vulgarWordsList = new VulgarWordsList();

            foreach (var word in vulgarWordsList.vulgarWords)
            {
                if (content.CreatorUserName.ToLower().Contains(word.ToLower()))
                {
                    flags += 1;
                }

                if (content.CreatorUserId.ToLower().Contains(word.ToLower()))
                {
                    flags += 1;
                }
            }

            if (content.CreatorProfilePictureURL != null)
            {
                flags += MediaCheck(content.CreatorProfilePictureURL);
            }

            if (content.CreatorBackgroundPictureURL != null)
            {
                flags += MediaCheck(content.CreatorBackgroundPictureURL);

            }

            return flags;
        }
    }
}