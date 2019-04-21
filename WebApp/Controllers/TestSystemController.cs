using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using Model.Users;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class TestSystemController : Controller
    {

        [HttpPost("test/create")]
        public void CreateTest(TestData data)
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


            throw new NotImplementedException();
           
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
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Candidate)) //TODO: Check!
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

            throw new NotImplementedException();

            
        }

        [HttpPost("test/submit")]
        public void SubmitAnswers(string data)//TODO: Change to  AnswersData
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return null;
            }
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Candidate)) //TODO: Check!
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

            throw new NotImplementedException();
        }






    }
}