using System;
using System.Collections.Generic;
using Google.Cloud.Vision.V1;
using HackKU2019.Models;

namespace HackKU2019
{
    public class ContentAnalyzer
    {
        public int analyzeContent(IContent content)
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
        private int vulgarWordCheck(string text)
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
        private int mediaCheck(string url)
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

        private int checkUser(IContent content)
        {
            int flags = 0;
            VulgarWordsList vulgarWordsList = new VulgarWordsList();

            foreach (var word in vulgarWordsList.vulgarWords)
            {
                if (content.AuthorName.ToLower().Contains(word))
                {
                    flags += 1;
                }
            }

            return flags;
        }
    }
}