using System;
using System.Collections.Generic;
using Google.Cloud.Vision.V1;
using HackKU2019.Models;

namespace HackKU2019
{
    public class ContentAnalyzer
    {
        public int AnalyzeContent(IContent content)
        {
            int flags = 0;
            flags += vulgarWordCheck(content.Text);
            if (content.MediaUrls.Count > 0)
            {
                foreach (var mediaUrl in content.MediaUrls)
                {
                    flags += mediaCheck(mediaUrl);
                }
            }

            checkUser(content);
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
                flags += mediaCheck(content.CreatorProfilePictureURL);
            }

            if (content.CreatorBackgroundPictureURL != null)
            {
                flags += mediaCheck(content.CreatorBackgroundPictureURL);

            }

            return flags;
        }
    }
}