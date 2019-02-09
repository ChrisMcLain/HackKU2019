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
        string Issues{get;set;}

        
    }

    public class MainUser
    {
       public User userInfo { get; set; }
       public Platforms Platforms { get; set; }
       public List<Followed> Following { get; set; }
       public List<Tweet> Tweets { get; set; }
       public string Issues{get;set;}
    }
    public class User
    {
        public string Name { get; set; }
        public string ProfilePictureUrl { get; set; }
        //this would be id or handle
        public string BannerPictureUrl { get; set; }
        public string UserID { get; set; }
        public string Bio { get; set; }
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