using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Users;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class DocumentSubmissionController : Controller
    {
        #region Photo
        [HttpGet("dashboard/photo")]
        public string GetPhoto()
        {
            throw new NotImplementedException();
        }

        [HttpPost("dashboard/photo")]
        public string UploadPhoto([FromBody] FileData data)
        {
            var tokenString = data.Token;
            var token = new TokenData(tokenString);

          

            var fileName = data.FileName;
            var fileString = data.FileString;


            var byteArray = Convert.FromBase64String(fileString);
            var stream = new MemoryStream(byteArray);
            throw new NotImplementedException();

            return "success";
        }
        #endregion Photo

        #region MotivationLetter
        [HttpGet("dashboard/motivationLetter")]
        public string GetMotivationLetter()
        {
            throw new NotImplementedException();
        }

        [HttpPost("dashboard/motivationLetter")]
        public string UploadMotivationLetter([FromBody] FileData data)
        {
            var tokenString = data.Token;
            var token = new TokenData(tokenString);



            var fileName = data.FileName;
            var fileString = data.FileString;


            var byteArray = Convert.FromBase64String(fileString);
            var stream = new MemoryStream(byteArray);
            throw new NotImplementedException();

            return "success";
        }
        #endregion

        #region Portfolio
        [HttpGet("dashboard/portfolio")]
        public string GetPortfolio()
        {
            throw new NotImplementedException();
        }

        [HttpPost("dashboard/portfolio")]
        public string UploadPortfolio([FromBody] FileData data)
        {
            var tokenString = data.Token;
            var token = new TokenData(tokenString);



            var fileName = data.FileName;
            var fileString = data.FileString;


            var byteArray = Convert.FromBase64String(fileString);
            var stream = new MemoryStream(byteArray);
            throw new NotImplementedException();

            return "success";
        }
        #endregion

        #region Transcript
        [HttpGet("dashboard/transcript")]
        public string GetTranscript()
        {
            throw new NotImplementedException();
        }

        [HttpPost("dashboard/transcript")]
        public string UploadTranscript([FromBody] FileData data)
        {
            var tokenString = data.Token;
            var token = new TokenData(tokenString);



            var fileName = data.FileName;
            var fileString = data.FileString;


            var byteArray = Convert.FromBase64String(fileString);
            var stream = new MemoryStream(byteArray);
            throw new NotImplementedException();

            return "success";
        }
        #endregion

        #region Passport
        [HttpGet("dashboard/passport")]
        public string GetPassport()
        {
            throw new NotImplementedException();
        }

        [HttpPost("dashboard/transcript")]
        public string GetTranscript([FromBody] FileData data)
        {
            var tokenString = data.Token;
            var token = new TokenData(tokenString);



            var fileName = data.FileName;
            var fileString = data.FileString;


            var byteArray = Convert.FromBase64String(fileString);
            var stream = new MemoryStream(byteArray);
            throw new NotImplementedException();

            return "success";
        }
        #endregion
    }
}