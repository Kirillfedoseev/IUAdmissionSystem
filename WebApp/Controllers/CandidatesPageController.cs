 using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Data;
using System;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class CandidatesPageController : Controller
    {


        [HttpGet("manager/candidatePage")]
        public string GetCandidatePageForManager([FromBody] string data)//todo change to candidateID data
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            throw new NotImplementedException();

            //????
            //return AuthManager.RegisterUser(data, new RootEnum[] { RootEnum.None }); 
        }

        [HttpPost("manager/candidateGrade")]
        public string SubmitSolutionForCandidate([FromBody] string data)//todo change to candidateSolution data
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            throw new NotImplementedException();
            //????
            //return AuthManager.RegisterUser(data, new RootEnum[] { RootEnum.None }); 
        }
    }
}