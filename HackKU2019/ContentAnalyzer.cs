using HackKU2019.Models;

namespace HackKU2019
{
    public class ContentAnalyzer
    {
        public int analyzeContent(IContent content)
        {
            int flags = 0;
            flags += vulgarWordCheck(content.Text);
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

        private int mediaCheck()
        {
            return 0;
        }
    }
}