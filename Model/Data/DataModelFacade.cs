using Model.Authentication;
using Model.Users;

namespace Model.Data
{
    public class DataModelFacade
    {
        
        /// <summary>
        /// Get user's profile by token
        /// </summary>
        /// <param name="authToken">Auth token, , which was got witihin authentication or registration</param>
        /// <exception cref=""></exception>
        /// <returns>User profile data</returns>
        public static UserProfile GetUserProfile(TokenData authToken) 
            => GetUser(authToken).Profile;

     
        public static void SetUserProfile(TokenData authToken, UserProfile profile) 
            => GetUser(authToken).Profile = profile;
        
        
        
        private static AbstractUser GetUser(TokenData authToken) 
            => AuthManager.Instance[authToken];
        
    }
}
