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
        Platforms Platform { get; set; } 
        String TargetUsername { get; set; }
    }
}