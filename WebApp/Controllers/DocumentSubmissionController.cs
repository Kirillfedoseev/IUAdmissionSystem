using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Users;
using Model.Data;
using Model.Files;
using System.Net;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class DocumentSubmissionController : Controller
    {
        [Route("dashboard/photo")]
        [HttpGet("{type}")]
        public FileDataWrapper GetFile(string type)
        {

            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return null;
            }

            //todo exception and check on valid token
            //Read Stream and convert to String     
            AbstractUser user = AuthManager.Instance[token];
            return FileManager.GetFileData(user, type);
        }

        [HttpPost("dashboard/photo")]
        public void UploadPhoto([FromBody] FileDataWrapper input)
        {
            
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);


            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }

            //Send file stream to DB
            //todo exceptions and token check

            try
            {
                AbstractUser user = AuthManager.Instance[token];
                FileManager.SubmitFile(user, input.Data, input.Bytes);
                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch ()
            {

            }
        }
        
        

    }
}