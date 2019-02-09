using System.Collections.Generic;

namespace HackKU2019.Models
{
    public interface IUser
    {
        string Name { get; set; }
        string ProfilePictureUrl { get; set; }
        //this would be id or handle
        string UserID { get; set; }
        Platforms Platforms { get; set; }
        List<string> followingUsersNames { get; set; }
        List<string> followingUsersBios { get; set; }
        List<string> followingUsersId { get; set; }
        List<string> followingUsersProfilePictureUrl { get; set; }
        List<string> followingUsersProfileBannerUrl { get; set; }




    }
}