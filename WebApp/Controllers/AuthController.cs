using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using Model.Users;
using System;
using System.Net;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("[controller]")]
    public class AuthController : Controller
    {


        // POST User Authorization
        [HttpPost]
        public TokenData Authorization([FromBody]AuthData data)
        {
            try
            {
                return AuthManager.AuthUser(data);
            }
            catch (ArgumentException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
            catch (AuthExceptions.UserDoesNotExists)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
            catch (AuthExceptions.IncorrectPassword)
            {
                Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
                return null;
            }
        }

        [HttpPost("registration")]
        public TokenData Registration([FromBody] RegistrationData data)
        {
            try
            {
                return AuthManager.RegisterUser(data, RootEnum.Candidate);
            }
            catch (AuthExceptions.RegistrationException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
            catch (AuthExceptions.UserAlreadyExists)
            {
                Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
                return null;
            }
        }

               
        [HttpPost("logout")]
        public void LogOut([FromBody] TokenData token)
        {
            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }
            try
            {
                AuthManager.LogOutUser(token);
                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (ArgumentException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

        }

    }
}
