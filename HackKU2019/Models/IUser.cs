using System;
using System.Collections.Generic;
using System.Linq;
using InstaSharp.Endpoints;
using Tweetinvi.Core.Extensions;

namespace HackKU2019.Models
{
    public class MainUser
    {
       public User UserInfo { get; set; }
       public Platforms Platforms { get; set; }
       public List<Followed> Following { get; set; }
       public List<Tweets> Tweets { get; set; }
       public string Issues { get; set; }
       public List<string> IssueList => Issues.Split(". ").Distinct().Where(n => n.Length > 0).ToList();
       public int TotalFlags => IssueList.Count;
    }
    public class User
    {
        public string Name { get; set; }

        public string ProfilePictureUrl { get; set; }
        /*{
            get => ProfilePictureUrl.Replace("_normal", "");
            set => ProfilePictureUrl = value;
        }*/

        //this would be id or handle
        public string BannerPictureUrl { get; set; }
        public string UserId { get; set; }
        public string Bio { get; set; }
    }

    public class Followed
    {
        public User UserInfo{ get; set; }
        public string Issue{ get; set; }
        public int TotalFlags { get; set; }



    }
    public class Tweets
    {
        public string Text{ get; set; }
        public List<string> MediaUrls { get; set; }
        public User UserCreateBy{ get; set; }
        public string Issue{ get; set; }
        public List<string> IssueList => Issue.Split(". ").Distinct().Where(n => n.Length > 0).ToList();
        public int TotalFlags => IssueList.Count;
        public long Id { get; set; }
        public bool Translated{get;set;}
    }
}