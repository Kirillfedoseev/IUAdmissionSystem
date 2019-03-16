using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Support;
using DataModel.Users;

namespace DataModel.Authentication
{
    public class AuthManager:Singletone<AuthManager>
    {
        private Dictionary<AuthData, AbstractUser> _usersAuthData;

        private TokensRegister _register;
        
        public AbstractUser this[string authToken] => _register.ValidateAuthToken(authToken);

        public AuthManager()
        {
            _register = new TokensRegister();
            _usersAuthData = new Dictionary<AuthData, AbstractUser>();
        }
        
        
        /// <summary>
        /// Authenticate user to the system
        /// After auth, user gets unique auth token, with which only one can access to the system
        /// After session user must logout
        /// </summary>
        /// <param name="login">login of the user (email)</param>
        /// <param name="password">password of the user (not encrypted)</param>
        /// <returns>auth token for the user</returns>
        public static string AuthUser(AuthData authData)
        {
            if (authData == null) throw new NullReferenceException("AuthData was null!");
            var user = Instance.DoesUserExists(authData);
            
            return Instance._register.GetAuthToken(user).Token;
        }
        
        
        /// <summary>
        /// Register new user with login and password
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string RegisterUser(AuthData authData, RootEnum[] roots)
        {
            AbstractUser user = new TestUser(roots); //todo factory of creating users 
            Instance._usersAuthData.Add(authData, user);
            return AuthUser(authData);
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
            Instance._register.FreeToken(authToken);
        }
     
        
        /// <summary>
        /// Auth tokens has TTL, and after validating TTL updates, 
        /// If register doesn't contain authToken, then you must Auth again 
        /// </summary>
        /// <param name="authtoken">authenticate token, which was given with authentication</param>
        /// <returns>true if authToken valid, false if not</returns>
        public static bool ValidateAuthToken(string authtoken)
        {
            return Instance._register.ValidateAuthToken(authtoken) != null;
        }


        private AbstractUser DoesUserExists(AuthData authData)
        {
            var pair = _usersAuthData.FirstOrDefault(n => n.Key.Login.Equals(authData.Login)); 
            
            if (pair.Key == null) throw new Exception("The user doent's exists!");
            if (!pair.Key.Password.Equals(authData.Password)) throw new Exception("Incorrect password!");
            
            return pair.Value;
        }    
    }
}