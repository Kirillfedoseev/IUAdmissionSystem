using System;
using System.Data;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Data;
using Model.Users;


namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class DashBoardController : Controller
    {


        // POST dashboard/saveProfile
        [HttpPost("dashboard/profile")]
        public void SaveProfile([FromBody] UserProfile data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            //todo token validation
            //todo root validation

            UsersManager.SetUserProfile(token, data);
        }


    
        [HttpGet("dashboard/profile")]
        public UserProfile GetProfile()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            //todo token validation
            //todo root validation
            return UsersManager.GetUserProfile(token);          
        }

        [HttpPost("manager/candidateStatus")]
        public CandidateUser SetCandidateStatus([FromBody] StatusUpdateData status) //TODO: Change to CanditateStatus Data
        {   
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            //todo token validation
            //todo root validation

            UsersManager.SetUserStatus(status);

            return UsersManager.GetUserById<CandidateUser>(status.CandidateId);

        }


    }
}
