using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using Model.Users;



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
           return AuthManager.AuthUser(data);
        }

        [HttpPost("registration")]
        public TokenData Registration([FromBody] RegistrationData data)
        {
            return AuthManager.RegisterUser(data,new RootEnum[]{RootEnum.None});
        }

               
        [HttpPost("logout")]
        public void LogOut([FromBody] TokenData token)
        {
            AuthManager.LogOutUser(token);
        }


        protected override void Dispose(bool disposing)
        {
            AuthManager.Instance.Dispose();
        }
    }
}
