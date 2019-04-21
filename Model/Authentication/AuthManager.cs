using System;
using System.Collections.Generic;
using System.Linq;
using Model.Data;
using Model.Support;
using Model.Users;

namespace Model.Authentication
{
    public class AuthManager:Singletone<AuthManager>
    {
        private readonly Dictionary<AuthData, AbstractUser> _usersAuthData;

        private readonly TokensRegister _register;
        
        /// <summary>
        /// Get AbstractUser by authToken
        /// </summary>
        /// <param name="authToken">authenticated token</param>
        public AbstractUser this[TokenData authToken] => _register.ValidateAuthToken(authToken);
            
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
        /// <param name="authData">login of the user (email) and password of the user (not encrypted)</param>
        /// <exception cref="ArgumentException">Check on null arguments</exception>
        /// <exception cref="AuthExceptions.UserDoesNotExists">If user's login doesnt' exists in DB</exception>
        /// <exception cref="AuthExceptions.IncorrectPassword">If password incorrect</exception>
        /// <returns>auth token for the user</returns>
        public static TokenData AuthUser(AuthData authData)
        {
            if (authData == null) 
                throw new ArgumentException("Passed parameter was null",nameof(authData));
            
            var user = DoesUserExists(authData);
            
            return Instance._register.GetAuthToken(user);
        }


        /// <summary>
        /// Register new user with login and password
        /// Also authenticate user
        /// </summary>
        /// <param name="regData">login of the user (email) and password of the user (not encrypted)</param>
        /// <exception cref="AuthExceptions.RegistrationException">If something went wrong</exception>
        /// <returns>Authenticated Token</returns>
        public static TokenData RegisterUser(RegistrationData regData)
        {
            if(Instance._usersAuthData.Count(n => n.Key.Equals(regData)) != 0) 
                throw  new AuthExceptions.UserAlreadyExists(regData);

            AbstractUser user = UsersManager.CreateUser(regData.RootType);

            Instance._usersAuthData.Add(regData, user);
            try
            {
                return AuthUser(regData);
            }

            catch (AuthExceptions)

            {
                throw new AuthExceptions.RegistrationException(regData);
            }
        }
        
        
        /// <summary>
        /// Logout user by deletion authtoekn from register
        /// Helps to make sure, that no body use the auth token
        /// Needs for security purposes
        /// </summary>
        /// <param name="authToken">auth token, which was recieved by registration or authentication</param>
        /// <returns>nothing if success, otherwise errors</returns>
        public static void LogOutUser(TokenData authToken)
        {
            Instance._register.FreeToken(authToken);
        }


        /// <summary>
        /// Auth tokens has TTL, and after validating TTL updates, 
        /// If register doesn't contain authToken, then you must Auth again 
        /// </summary>
        /// <param name="authToken">authenticate token, which was given with authentication</param>
        /// <exception cref="TokenExceptions.TokenDoesNotExists">If register doesn't have such token, return false</exception>
        /// <exception cref="TokenExceptions.TokenExpired">If token in register, but expired, return false</exception>
        /// <returns>true if authToken valid, false if not</returns>
        public static bool ValidateAuthToken(TokenData authToken)
        {
            try
            {
                return Instance._register.ValidateAuthToken(authToken) != null;
            }

            catch (TokenExceptions.TokenDoesNotExists)
            {
                return false;
            }
            catch (TokenExceptions.TokenExpired)
            {
                return false;
            }
        }

        /// <summary>
        /// Check on user existence
        /// </summary>
        /// <param name="authData">authentication data of some user</param>
        /// <returns>User with such auth data</returns>
        /// <exception cref="AuthExceptions.UserDoesNotExists">If user's login doesn't exists in DB</exception>
        /// <exception cref="AuthExceptions.IncorrectPassword">If password incorrect</exception>
        public static AbstractUser DoesUserExists(AuthData authData)
        {
            var pair = Instance._usersAuthData.FirstOrDefault(n => n.Key.Login.Equals(authData.Login)); 
            
            if (pair.Key == null) 
                throw new AuthExceptions.UserDoesNotExists(authData);
            if (!pair.Key.Password.Equals(authData.Password)) 
                throw  new AuthExceptions.IncorrectPassword(authData);
            
            return pair.Value;
        }    
    }
}