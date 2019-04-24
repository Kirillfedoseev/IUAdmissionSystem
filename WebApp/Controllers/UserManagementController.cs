using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using Model.Users;
using System.Net;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class UserManagementController : Controller
    {

        [HttpPost("admin/createUser")]
        public void CreateUserByAdmin([FromBody] RegistrationData data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Admin))
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            var tokenNewUser = AuthManager.RegisterUser(data);
            AuthManager.LogOutUser(tokenNewUser);

        }

        [HttpPost("admin/createManager")]
        public void CreateManagerByAdmin([FromBody] RegistrationData data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Admin))
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }


            var tokenNewUser = AuthManager.RegisterUser(new RegistrationData(data.Login, data.Password)
            {
                RootType = RootEnum.Manager
            });
            AuthManager.LogOutUser(tokenNewUser);
        }

        [HttpPost("admin/createInterviewer")]
        public void CreateInterviewerByAdmin([FromBody] RegistrationData data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Admin))
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }


            var tokenNewUser = AuthManager.RegisterUser(new RegistrationData(data.Login, data.Password)
            {
                RootType = RootEnum.Interviewer
            });
            AuthManager.LogOutUser(tokenNewUser);
        }

    }
}