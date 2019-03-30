using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using Model.Users;


namespace WebApp.Controllers
{
    [Route("[controller]")]
    public class DashBoardController : Controller
    {


        // POST dashboard/saveProfile
        [EnableCors]
        [HttpPost("profile")]
        public string SaveProfile([FromBody] UserProfile data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            DataModelFacade.SetUserProfile(token, data);
            
            //TODO: Change Data return type to void and delete after test:
            return "success";
        }

        [EnableCors]
        [HttpPost("profileget")]
        public UserProfile GetProfile()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            return DataModelFacade.GetUserProfile(token);
           
        }

        

        
    }
}
