 using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using Model.Users;
using System;
using System.Net;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class CandidatesPageController : Controller
    {


      
        [Route("manager/candidatePage")]
        [HttpGet("{candidateId}")]
        public UserProfile GetCandidatePageForManager(int candidateID)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return new UserProfile();
            }
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Manager))
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new UserProfile();
            }

            return UsersManager.GetUserProfile(candidateID);
        }

        [HttpPost("manager/candidateGrade")]
        public void SubmitSolutionForCandidate(StatusUpdateData data)
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

            UsersManager.SetUserStatus(data);
        }
    }
}