using System.Collections.Generic;

namespace HackKU2019.Models
{

    public class TwitterContent : IContent
    {
        public string Text { get; set; }
        public string AuthorName { get; set; }
        public List<string> MediaUrls { get; set; }
        public string CreatorUserName { get; set; }
        public string CreatorProfilePictureURL { get; set; }
        public string CreatorBackgroundPictureURL { get; set; }

        public string CreatorBio { get; set; }
        public string CreatorUserId { get; set; }
        public Platforms Platform { get; set; }
        public string TargetUsername { get; set; }
    }

    public class TwitterUser : IUser
    {
        public string Name { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string UserID { get; set; }
        public Platforms Platforms { get; set; }
        public List<Followed> Following { get; set; }
    }

}