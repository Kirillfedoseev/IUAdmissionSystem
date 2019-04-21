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
        [Route("dashboard/photo")]
        [HttpGet("{type}")]
        public FileInput  GetFile(string type)
        {

            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //todo exception and check on valid token
            //Read Stream and convert to String     
            AbstractUser user = AuthManager.Instance[token];
            var result = new FileInput();
            var output = FileManager.GetFileData(user, type);
            result.Bytes = output.fileStream;
            result.Data = output.fileData;
            return result;
        }

        [HttpPost("dashboard/photo")]
        public void UploadPhoto([FromBody] FileInput input)
        {
            
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //Send file stream to DB
            //todo exceptions and token check
            AbstractUser user = AuthManager.Instance[token];
            FileManager.SubmitFile(user, input.Data, input.Bytes);
        }
        
        public struct FileInput
        {
            public FileData Data { get; set; }
            public string Bytes { get; set; }
        }

    }
}