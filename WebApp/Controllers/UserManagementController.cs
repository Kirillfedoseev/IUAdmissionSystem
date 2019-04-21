using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;

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

            //todo token validation
            //todo root validation
            var tokenNewUser = AuthManager.RegisterUser(data);
            AuthManager.LogOutUser(tokenNewUser);

        }

    }
}