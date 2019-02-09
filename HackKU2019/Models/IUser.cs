namespace HackKU2019.Models
{
    public interface IUser
    {
        string Name { get; set; }
        string ProfilePictureUrl { get; set; }
        //this would be id or handle
        string UserID { get; set; }
        Platforms Platforms { get; set; }
    }
}