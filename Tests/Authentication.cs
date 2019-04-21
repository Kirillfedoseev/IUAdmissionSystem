using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Authentication;
using Model.Data;

namespace Tests
{
    [TestClass]
    public class Authentication
    {

        [TestMethod]
        public void RegistrationCompleted()
        {

            RegistrationData authData = new RegistrationData("test","test");

            TokenData tokenData = AuthManager.RegisterUser(authData);

            var user = AuthManager.DoesUserExists(authData);

            Assert.IsNotNull(user);

            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));

        }


        [TestMethod]
        public void RegistrationOfExistingUser()
        {

            RegistrationData authData = new RegistrationData("test1", "test1");

            TokenData tokenData = AuthManager.RegisterUser(authData);

            try
            {
                authData = new RegistrationData("test1", "test1");

                tokenData = AuthManager.RegisterUser(authData);
                Assert.Fail("The same user was added!");
            }
            catch (AuthExceptions.UserAlreadyExists e)
            {
                Assert.IsNotNull(e);
            }         
        }


        [TestMethod]
        public void LogInCorrect()
        {

            RegistrationData authData = new RegistrationData("test2", "test2");

            TokenData tokenData = AuthManager.RegisterUser(authData);

            AuthManager.LogOutUser(tokenData);

            tokenData = AuthManager.AuthUser(authData);

            Assert.IsNotNull(tokenData);
          
            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));

        }


        [TestMethod]
        public void LogInWithIncorrectPassword()
        {

            RegistrationData authData = new RegistrationData("test3", "test3");

            TokenData tokenData = AuthManager.RegisterUser(authData);

            AuthManager.LogOutUser(tokenData);

            tokenData = AuthManager.AuthUser(authData);

            Assert.IsNotNull(tokenData);

            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));

        }


        [TestMethod]
        public void LogOutDone()
        {

            RegistrationData authData = new RegistrationData("test4", "test4");

            TokenData tokenData = AuthManager.RegisterUser(authData);

            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));

            AuthManager.LogOutUser(tokenData);

            Assert.IsFalse(AuthManager.ValidateAuthToken(tokenData));
        }
    }
}
