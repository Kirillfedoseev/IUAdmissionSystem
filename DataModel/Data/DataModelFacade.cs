using DataModel.Authentication;
using DataModel.Users;

namespace DataModel.Data
{
    public class DataModelFacade
    {
        
        /// <summary>
        /// Get user's profile by token
        /// </summary>
        /// <param name="authToken">Auth token, , which was got witihin authentication or registration</param>
        /// <exception cref=""></exception>
        /// <returns>User profile data</returns>
        public static UserProfile GetUserProfile(string authToken) 
            => GetUser(authToken).Profile;

     
        public static void SetUserProfile(string authToken, UserProfile profile) 
            => GetUser(authToken).Profile = profile;
        
        
        
        private static AbstractUser GetUser(string authToken) 
            => AuthManager.Instance[authToken];
        
    }
}
