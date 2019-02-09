using System.Collections.Generic;

namespace HackKU2019.Models
{
    public interface IContent
    {
        string Text { get; set; }
        string AuthorName { get; set; }
        List<string> MediaUrls { get; set; }
    }
}