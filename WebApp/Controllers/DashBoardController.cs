
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using Model.Users;


namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class DashBoardController : Controller
    {

        [HttpPost("dashboard/profile")]
        public void SaveProfile([FromBody] UserProfile data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);


            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }


            //todo root validation


            UsersManager.SetUserProfile(token, data);
        }


        [HttpGet("dashboard/profile")]
        public UserProfile GetProfile()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);


            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return new UserProfile();
            }

            //todo root validation
            return UsersManager.GetUserProfile(token);

        }


        [HttpPost("manager/candidateStatus")]

        public void SetCandidateStatus([FromBody] StatusUpdateData status) //TODO: Change to CanditateStatus Data
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }

            //todo root validation

            UsersManager.SetUserStatus(status);

            

        }

    }
}
