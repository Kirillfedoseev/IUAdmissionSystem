using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Users;
using Model.Data;
using Model.Files;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class DocumentSubmissionController : Controller
    {

        [HttpGet("{type}", Name = "dashboard/photo")]
        public (FileData fileData, string bytes) GetFile(string type)
        {

            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //todo exception and check on valid token
            //Read Stream and convert to String     
            AbstractUser user = AuthManager.Instance[token];
            return FileManager.GetFileData(user, type);
        }

        [HttpPost("dashboard/photo")]
        public void UploadPhoto([FromBody] FileData data, string bytes)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //Send file stream to DB
            //todo exceptions and token check
            AbstractUser user = AuthManager.Instance[token];
            FileManager.SubmitFile(user, data, bytes);
        }
        

    }
}