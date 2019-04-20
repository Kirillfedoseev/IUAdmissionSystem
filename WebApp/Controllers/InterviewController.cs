using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Data;
using Model.Interviews;
using Model.Users;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class InterviewController : Controller
    {

        [HttpGet("interviewer/candidates")]
        public CandidateUser[] GetCandidatesForInterviewer()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            //todo interviewer id
            return InterviewManager.GetCandidateUserList(token);
        }

        [HttpPost("interviewer/updateGrade")]
        public void UpdateGrade([FromBody] string someData) //TODO: Change to GradeInfo Data
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //todo give status of interview
            InterviewManager.SetInterviewResults(1, InterviewManager.InterviewStatus.Fail);
        }

        [HttpGet("manager/interview/candidates")]
        public CandidateUser[] ShowAllCandidatesReadyForInterview() 
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            //todo checks and token validation
            return InterviewManager.GetCandidateUserList();

        }

        [HttpPost("manager/addInterview")]
        public void AddInterview([FromBody] string someData) //TODO: Change to InterviewInfo Data
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //todo set ids of interviewer and candidate
            //todo checks
            InterviewManager.CreateInterview(1,1);
        }

        [HttpPost("manager/editInterview")]
        public void EditInterview([FromBody] string someData) //TODO: Change to InterviewIdentification Data
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //todo checks
            InterviewManager.DeleteInterview(1,1);
            InterviewManager.CreateInterview(2, 1);
        }

        [HttpPost("manager/deleteInterview")]
        public void DeleteInterview([FromBody] string someData) //TODO: Change to InterviewIdentification Data
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //todo checks
            InterviewManager.DeleteInterview(1, 1);
        }


    }
}