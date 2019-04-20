using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using Model.Users;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {


        // POST User Authorization
        [EnableCors]
        [HttpPost]
        public TokenData Authorization([FromBody]AuthData data)
        {
           return AuthManager.AuthUser(data);
        }

        [EnableCors]
        [HttpPost("registration")]
        public TokenData Registration([FromBody] RegistrationData data)
        {
            return AuthManager.RegisterUser(data,new RootEnum[]{RootEnum.None});
        }

               
        [EnableCors]
        [HttpPost("logout")]
        public void LogOut([FromBody] TokenData token)
        {
            AuthManager.LogOutUser(token);
        }

    }
}
