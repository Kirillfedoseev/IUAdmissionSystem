using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Authentication;
using Model.Users;

namespace Tests
{
    [TestClass]
    class FilesStoringTests
    {
        [TestMethod]
        public void RegistrationCompleted()
        {

            AuthData authData = new AuthData("test", "test");

            TokenData tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);

            var user = AuthManager.DoesUserExists(authData);

            Assert.IsNotNull(user);

            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));

        }

    }
}
