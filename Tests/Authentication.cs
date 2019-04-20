using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Authentication;
using Model.Users;

namespace Tests
{
    [TestClass]
    public class Authentication
    {

        [TestMethod]
        public void RegistrationCompleted()
        {

            AuthData authData = new AuthData("test","test");

            TokenData tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);

            var user = AuthManager.DoesUserExists(authData);

            Assert.IsNotNull(user);

            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));

        }


        [TestMethod]
        public void RegistrationOfExistingUser()
        {

            AuthData authData = new AuthData("test1", "test1");

            TokenData tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);

            try
            {
                authData = new AuthData("test1", "test1");

                tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);
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

            AuthData authData = new AuthData("test2", "test2");

            TokenData tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);

            AuthManager.LogOutUser(tokenData);

            tokenData = AuthManager.AuthUser(authData);

            Assert.IsNotNull(tokenData);
          
            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));

        }


        [TestMethod]
        public void LogInWithIncorrectPassword()
        {

            AuthData authData = new AuthData("test3", "test3");

            TokenData tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);

            AuthManager.LogOutUser(tokenData);

            tokenData = AuthManager.AuthUser(authData);

            Assert.IsNotNull(tokenData);

            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));

        }


        [TestMethod]
        public void LogOutDone()
        {

            AuthData authData = new AuthData("test4", "test4");

            TokenData tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);

            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));

            AuthManager.LogOutUser(tokenData);

            Assert.IsFalse(AuthManager.ValidateAuthToken(tokenData));
        }
    }
}
