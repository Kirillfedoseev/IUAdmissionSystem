using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Data;
using Microsoft.AspNetCore.Mvc;



namespace WebApp.Controllers
{
    [Route("[controller]")]
    public class DashBoardController : Controller
    {

        // POST dashboard/saveProfile
        [HttpPost("profile")]
        public void SaveProfile([FromBody]string value)
        {
            //TODO Change argument Type to ProfileInfoData (User Info?)
            throw new NotImplementedException("Data Model is not Implemented yet");
        }

        [HttpGet("profile")]
        public void GetProfile([FromBody] TokenData data)
        {
            //TODO Change Return type to ProfileInfoData (UserInfo?)
            throw new NotImplementedException("Data Model is not implemented yet");

        }

        
    }
}
