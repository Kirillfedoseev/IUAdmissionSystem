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

            AuthData authData = new AuthData("test","test");

            TokenData tokenData = AuthManager.RegisterUser(authData, new RootEnum[0]);

            var user = AuthManager.DoesUserExists(authData);


            Assert.IsNotNull(user);

            Assert.IsTrue(AuthManager.ValidateAuthToken(tokenData));

        }
       
    }
}
