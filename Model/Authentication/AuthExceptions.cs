using System;
using Model.Data;

namespace Model.Authentication
{
    public class AuthExceptions:Exception
    {
        private AuthExceptions(AuthData authData, string exception) : base(exception) => _authData = authData;


        private AuthExceptions(string exception) : base(exception) { }


        private AuthData _authData;
        
        
        public class RegistrationException : AuthExceptions
        {
            public RegistrationException(AuthData authData) : base("Registration failed for unknown reason!") 
                => _authData = authData;
        }

        public class IncorrectPassword : AuthExceptions
        {
            public IncorrectPassword(AuthData authData) : base("Incorrect password!") => _authData = authData;
        } 
        
        public class UserDoesNotExists : AuthExceptions
        {
            public UserDoesNotExists(AuthData authData) : base("The user doent's exists!") => _authData = authData;
        }

        public class UserAlreadyExists : AuthExceptions
        {
            public UserAlreadyExists(AuthData authData) : base("The user already exists!") => _authData = authData;
        }
    }

    public class TokenExceptions : Exception
    {
        private TokenExceptions(TokenData authData, string exception) : base(exception) => _tokenData = authData;

        private TokenData _tokenData;


        private TokenExceptions(string exception) : base(exception) { }
        
        public class TokenDoesNotExists : TokenExceptions
        {
            public TokenDoesNotExists(TokenData tokenData) : base("Incorrect Token!") 
                => _tokenData = tokenData;
        }   
        
        
        
        public class TokenExpired : TokenExceptions
        {
            public TokenExpired(TokenData tokenData) : base("Token expired, please authenticate again.") 
                => _tokenData = tokenData;
        }   
    }
}