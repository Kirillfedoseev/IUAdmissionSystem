using DataModel.Data;
using DataModel.Support;
using DataModel.Users;

namespace DataModel
{
    public class AuthManager:Singletone<AuthManager>
    {
     
                    
        
        /// <summary>
        /// Authenticate user to the system
        /// After auth, user gets unique auth token, with which only one can access to the system
        /// After session user must logout
        /// </summary>
        /// <param name="login">login of the user (email)</param>
        /// <param name="password">password of the user (not encrypted)</param>
        /// <returns>auth token for the user</returns>
        public static TokenData AuthUser(string login, string password)
        {
            //IData data;
            //return "osfjsngjksngjsrngjrmgrgrinrinerig"; //todo return auth token

            //TODO: Check that solution
            var tokenData = new TokenData
            {
                token = "osfjsngjksngjsrngjrmgrgrinrinerig"
            };

            return tokenData;

        }

        
        /// <summary>
        /// Register new user with login and password
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static TokenData RegisterUser(string login, string password, RootEnum[] roots)
        {
            //TODO: Check that solution
            var tokenData = new TokenData
            {
                token = "osfjsngjksngjsrngjrmgrgrinrinerig"
            };

            return tokenData;
        }
        
        
        /// <summary>
        /// Logout user by deletion authtoekn from register
        /// Helps to make sure, that no body use the auth token
        /// Needs for security purposes
        /// </summary>
        /// <param name="authToken">auth token, which was recieved by registration or authentication</param>
        /// <returns>notihng if success, otherwise errors</returns>
        public static void LogOutUser(string authToken)
        {
            
        }


        
        
        /// <summary>
        /// Auth tokens has TTL, and after validating TTL updates, 
        /// If register doesn't contain authToken, then you must Auth again 
        /// </summary>
        /// <param name="authtoken">authenticate token, which was given with authentication</param>
        /// <returns>true if authToken valid, false if not</returns>
        public static bool ValidateAuthToken(string authtoken)
        {
            return "osfjsngjksngjsrngjrmgrgrinrinerig" == authtoken;
        }
        
    }
}