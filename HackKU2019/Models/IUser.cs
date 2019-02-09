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
        List<Followed> Following { get; set; }

    }

    public class Followed
    {
        public string followingName { get; set; }
        public string followingUserId { get; set; }
        public string followingUserBio { get; set; }
        public string followingUserProfilePictureUrl { get; set; }
        public string followingUsersProfileBannerUrl { get; set; }

    }
}