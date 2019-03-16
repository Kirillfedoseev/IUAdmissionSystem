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
        public string SaveProfile([FromBody]TokenData token, UserProfile userProfile)
        {
            DataModelFacade.SetUserProfile(token, userProfile);
            //TODO: Change Data return type to void and delete after test:
            return "success";
        }

        [HttpGet("profile")]
        public void GetProfile([FromBody] TokenData token)
        {
            DataModelFacade.GetUserProfile(token);
        }

        
    }
}
