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
    public class UserManagementController : Controller
    {

        [HttpPost("admin/createUser")]
        public string CreateUserByAdmin([FromBody] RegistrationData data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            throw new NotImplementedException();

            //????
            //return AuthManager.RegisterUser(data, new RootEnum[] { RootEnum.None }); 
        }



    }
}