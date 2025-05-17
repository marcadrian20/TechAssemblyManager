
namespace TechAssemblyManager.DAL.FirebaseHelper
{
    //This class is used to hold the user data so it's accessible from any form.
    public static class SessionManager
    {
        public static TechAssemblyManager.Models.User LoggedInUser { get; set; }
    }
}
