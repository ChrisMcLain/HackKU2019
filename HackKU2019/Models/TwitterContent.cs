namespace HackKU2019.Models
{
    public class TwitterContent : IContent
    {
        public string Text { get; set; }
    }

    public class TwitterUser : IUser
    {
        public string Name { get; set; }
    }
}