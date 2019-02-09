using System.Collections.Generic;

namespace HackKU2019.Models
{

    public class TwitterContent : IContent
    {
        public string Text { get; set; }
        public string AuthorName { get; set; }
        public List<string> MediaUrls { get; set; }
        public string CreatorUserName { get; set; }
        public Platforms Platform { get; set; }
        public string TargetUsername { get; set; }
    }

    public class TwitterUser : IUser
    {
        public string Name { get; set; }
    }
}