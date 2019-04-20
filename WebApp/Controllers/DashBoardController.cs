using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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


        //TODO: DELETE DEPRICATED
        [EnableCors]
        [HttpPost("uploadFile")]
        public string UploadFile([FromBody] FileData data)
        {
            var tokenString = data.Token;
            var token = new TokenData(tokenString);

            /////FOR TESTING!!! TODO: DELETE LATER
            //var byteArray = System.IO.File.ReadAllBytes("FileName.pdf");
            //data.FileString = Convert.ToBase64String(byteArray);
            ///// 


            var fileName = data.FileName;
            var fileString = data.FileString;

            var byteArray = Convert.FromBase64String(fileString);
            var stream = new MemoryStream(byteArray);

           

            ///FOR TESTING!!! TODO: DELETE LATER
            //using (System.IO.FileStream output = new System.IO.FileStream(fileName, FileMode.Create))
            //{
            //    stream.CopyTo(output);
            //}
            ///

            //TODO: Change Data return type to void and delete after test:
            return "success";
        }





    }
}
