using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Users;
using DataModel;
using DataModel.Authentication;
using DataModel.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace WebApp.Controllers
{
    [Route("[controller]")]
    public class DashBoardController : Controller
    {

        // POST dashboard/saveProfile
        [HttpPost("profile")]
        public string SaveProfile([FromBody] UserProfile data)
        {
            var tokenString = Request.Headers["token"];
            var token = new TokenData(tokenString);
            DataModelFacade.SetUserProfile(token, data);
            
            //TODO: Change Data return type to void and delete after test:
            return "success";
        }

        [HttpGet("profile")]
        public UserProfile GetProfile()
        {
            var tokenString = Request.Headers["token"];
            var token = new TokenData(tokenString);
            return DataModelFacade.GetUserProfile(token);
           
        }

        
    }
}
