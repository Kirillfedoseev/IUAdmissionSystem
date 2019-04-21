using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Authentication;
using Model.Data;
using Model.Users;

namespace Tests
{
    [TestClass]
    public class InterviewTests
    {

        [TestMethod]
        public void CandidateAddedToReadyList()
        {

            RegistrationData authData = new RegistrationData("test","test");

            TokenData tokenData = AuthManager.RegisterUser(authData);

            var user = AuthManager.DoesUserExists(authData);


            Assert.IsNotNull(user);

            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));

        }

    }
}
