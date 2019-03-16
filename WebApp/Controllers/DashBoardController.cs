using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Users;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace WebApp.Controllers
{
    [Route("[controller]")]
    public class DashBoardController : Controller
    {

        // POST dashboard/saveProfile
        [HttpPost("profile")]
        public void SaveProfile([FromBody]UserProfile value)
        {
            
        }

        [HttpGet("profile")]
        public void GetProfile([FromBody] string data)
        {
            //TODO Change Return type to ProfileInfoData (UserInfo?)
            throw new NotImplementedException("Data Model is not implemented yet");

        }

        
    }
}
