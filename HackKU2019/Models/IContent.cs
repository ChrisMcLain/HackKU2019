using System;
using System.Collections.Generic;

namespace HackKU2019.Models
{
    public enum Platforms
    {
        Twitter,Facebook,Instagram
    }
    public interface IContent
    {
        string Text { get; set; }
        string AuthorName { get; set; }
        List<string> MediaUrls { get; set; }
        string CreatorUserName { get; set; }
        string CreatorProfilePictureURL { get; set; }
        //handle or userid
        string CreatorUserId { get; set; }
        Platforms Platform { get; set; } 
    }
}