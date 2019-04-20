using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class InterviewController : Controller
    {

        [HttpGet("interviewer/candidates")]
        public string GetCandidatesForInterviewer()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            throw new NotImplementedException();

        }

        [HttpPost("interviewer/updateGrade")]
        public string UpdateGrade([FromBody] string someData) //TODO: Change to GradeInfo Data
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            throw new NotImplementedException();

        }

        [HttpGet("manager/interview/candidates")]
        public string ShowAllCandidatesReadyForInterview() 
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            throw new NotImplementedException();

        }

        [HttpPost("manager/addInterview")]
        public string AddInterview([FromBody] string someData) //TODO: Change to InterviewInfo Data
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            throw new NotImplementedException();

        }

        [HttpPost("manager/editInterview")]
        public string EditInterview([FromBody] string someData) //TODO: Change to InterviewIdentification Data
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            throw new NotImplementedException();

        }

        [HttpPost("manager/deleteInterview")]
        public string DeleteInterview([FromBody] string someData) //TODO: Change to InterviewIdentification Data
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            throw new NotImplementedException();

        }


    }
}