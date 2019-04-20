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

            AuthData authData = new AuthData("test1","test1");

            TokenData tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);

            var user = AuthManager.DoesUserExists(authData);

            Assert.IsNull(user);

            Assert.IsFalse(AuthManager.ValidateAuthToken(tokenData));

        }

        [TestMethod]
        public void LogInCorrect()
        {

            AuthData authData = new AuthData("test2", "test2");

            TokenData tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);

            AuthManager.LogOutUser(tokenData);

            tokenData = AuthManager.AuthUser(authData);

            Assert.IsNull(tokenData);
          
            Assert.IsFalse(AuthManager.ValidateAuthToken(tokenData));

        }

        [TestMethod]
        public void LogOutDone()
        {

            AuthData authData = new AuthData("test2", "test2");

            TokenData tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);

            Assert.IsFalse(AuthManager.ValidateAuthToken(tokenData));

            AuthManager.LogOutUser(tokenData);

            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));
        }
    }
}
