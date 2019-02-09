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

            return flags;
        }

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
    }
}