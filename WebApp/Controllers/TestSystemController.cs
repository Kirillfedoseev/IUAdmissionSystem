using System;
using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using Model.Users;
using Model.Tests;
using Model.Programs;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class TestSystemController : Controller
    {

        [HttpPost("test/create")]
        public void CreateTest(TestData testData)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Manager))
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            TestsManager.CreateTest(testData);
        }

        [HttpGet("test/getTests")]
        public TestData[] GetTests()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return null;
            }
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Candidate)) 
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

            var candidateId = UsersManager.GetUser(token).Id;
            return ProgramsManager.GetCandidateTests(candidateId);
        }

        [HttpPost("test/submit")]
        public void SubmitAnswers(TestResultsData testResults)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Candidate)) 
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            TestsManager.SubmitTestResult(UsersManager.GetUser(token).Id, testResults);
        }






    }
}