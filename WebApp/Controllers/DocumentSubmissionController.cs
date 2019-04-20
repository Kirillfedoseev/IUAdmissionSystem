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
using Model.Data;
using System.Text;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class DocumentSubmissionController : Controller
    {
        #region Photo
        [HttpGet("dashboard/photo")]
        public FileData GetPhoto()
        {

            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);            

            //Read Stream and convert to String
            Stream stream = DataModelFacade.GetFile(token, FileTypes.Photo);
            string fileString = StreamToString(stream);

            //Initialize Result for response
            FileData result = new FileData();
            result.FileName = FileTypes.Photo.ToString();
            result.FileString = fileString;

            return result;
        }

        [HttpPost("dashboard/photo")]
        public void UploadPhoto([FromBody] FileData data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);


            Stream stream = StringToStream(data.FileString);

            //Send file stream to DB
            DataModelFacade.SubmitFile(token, FileTypes.Photo, stream);
        }
        #endregion Photo

        #region MotivationLetter
        [HttpGet("dashboard/motivationLetter")]
        public FileData GetMotivationLetter()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //Read Stream and convert to String
            Stream stream = DataModelFacade.GetFile(token, FileTypes.Letter);
            string fileString = StreamToString(stream);

            //Initialize Result for response
            FileData result = new FileData();
            result.FileName = FileTypes.Letter.ToString();
            result.FileString = fileString;

            return result;
        }

        [HttpPost("dashboard/motivationLetter")]
        public void UploadMotivationLetter([FromBody] FileData data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);


            Stream stream = StringToStream(data.FileString);

            //Send file stream to DB
            DataModelFacade.SubmitFile(token, FileTypes.Letter, stream);


        }
        #endregion

        #region Portfolio
        [HttpGet("dashboard/portfolio")]
        public FileData GetPortfolio()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //Read Stream and convert to String
            Stream stream = DataModelFacade.GetFile(token, FileTypes.CV);
            string fileString = StreamToString(stream);

            //Initialize Result for response
            FileData result = new FileData();
            result.FileName = FileTypes.CV.ToString();
            result.FileString = fileString;

            return result;
        }

        [HttpPost("dashboard/portfolio")]
        public void UploadPortfolio([FromBody] FileData data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);


            Stream stream = StringToStream(data.FileString);

            //Send file stream to DB
            DataModelFacade.SubmitFile(token, FileTypes.CV, stream);
        }
        #endregion

        #region Transcript
        [HttpGet("dashboard/transcript")]
        public FileData GetTranscript()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //Read Stream and convert to String
            Stream stream = DataModelFacade.GetFile(token, FileTypes.Transcripts);
            string fileString = StreamToString(stream);

            //Initialize Result for response
            FileData result = new FileData();
            result.FileName = FileTypes.Transcripts.ToString();
            result.FileString = fileString;

            return result;
        }

        [HttpPost("dashboard/transcript")]
        public void UploadTranscript([FromBody] FileData data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);


            Stream stream = StringToStream(data.FileString);

            //Send file stream to DB
            DataModelFacade.SubmitFile(token, FileTypes.Transcripts, stream);
        }
        #endregion

        #region Passport
        [HttpGet("dashboard/passport")]
        public FileData GetPassport()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            //Read Stream and convert to String
            Stream stream = DataModelFacade.GetFile(token, FileTypes.Passport);
            string fileString = StreamToString(stream);

            //Initialize Result for response
            FileData result = new FileData();
            result.FileName = FileTypes.Passport.ToString();
            result.FileString = fileString;

            return result;
        }

        [HttpPost("dashboard/passport")]
        public void UploadPassports([FromBody] FileData data)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);


            Stream stream = StringToStream(data.FileString);

            //Send file stream to DB
            DataModelFacade.SubmitFile(token, FileTypes.Passport, stream);
        }
        #endregion

        #region Private Methods
        private string StreamToString(Stream stream)
        {
            //Read Stream and convert to String
            StreamReader reader = new StreamReader(stream);
            string fileString = reader.ReadToEnd();
            return fileString;
        }

        private Stream StringToStream(string fileString)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(fileString);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }
        #endregion
    }
}