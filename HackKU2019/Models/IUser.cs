using System.Collections.Generic;
using InstaSharp.Endpoints;

namespace HackKU2019.Models
{
    public interface IUser
    {
        User userInfo { get; set; }
        Platforms Platforms { get; set; }
        List<Followed> Following { get; set; }
        List<Tweet> Tweets { get; set; }

        
    }

    public class User
    {
        string Name { get; set; }
        string ProfilePictureUrl { get; set; }
        //this would be id or handle
        string BannerPictureUrl { get; set; }
        string UserID { get; set; }
        string Bio { get; set; }
    }

    public class Followed
    {
        public User UserInfo{ get; set; }
        public string Issue{ get; set; }


    }
    public class Tweet
    {
        public string Text{ get; set; }
        public List<string> MediaUrls { get; set; }
        public User UserCreateBy{ get; set; }
        public string Issue{ get; set; }

    }
}