using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Authentication;
using Model.Data;
using Model.Users;


namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class DashBoardController : Controller
    {


        // POST dashboard/saveProfile
        [HttpPost("dashboard/profile")]
        public string SaveProfile([FromBody] UserProfile data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            DataModelFacade.SetUserProfile(token, data);
            
            //TODO: Change Data return type to void and delete after test:
            return "success";
        }


    
        [HttpGet("dashboard/profile")]
        public UserProfile GetProfile()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            return DataModelFacade.GetUserProfile(token);
           
        }

        [HttpPost("manager/candidateStatus")]
        public UserProfile SetCandidateStatus([FromBody] string data) //TODO: Change to CanditateStatus Data
        {   
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            throw new NotImplementedException();

        }


    }
}
