using System;

namespace DataModel.Authentication
{
    public class AuthExceptions:Exception
    {
        public AuthExceptions(string exception):base(exception){}
        
        public class RegistrationException : Exception
        {
            
            public RegistrationException(AuthData authData) : base()
            {
            }
        }

        public class UserDidNotExists : AuthExceptions
        {
            public UserDidNotExists(AuthData authData) : base("The user doent's exists!")
            {
                
            }
        }
        
        
        
    }
}