using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Users;
using Model.Data;
using Model.Files;
using System.Net;
using System;
using System.Linq;

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

            try
            {
                //Read Stream and convert to String     
                AbstractUser user = AuthManager.Instance[token];
                return FileManager.GetFileData(user, type);
            }
            catch(FileException)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
        }

        [Route("manager/getCandidateFile")]
        [HttpGet("{candidateId}/{type}")]
        public FileDataWrapper GetCandidateFiles(int candidateId, string type)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return null;
            }
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Manager))
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

            try
            {
                //Read Stream and convert to String     
                AbstractUser user = AuthManager.Instance[token];
                return FileManager.GetFileData(user, type);
            }
            catch (FileException)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
        }


        [Route("manager/getCandidateFilesInfo")]
        [HttpGet("{candidateId}")]
        public FileData[] GetCandidateFilesInfo(int candidateId)
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return null;
            }
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Manager))
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

            try
            {
                //Read Stream and convert to String     
                AbstractUser user = AuthManager.Instance[token];
                return FileManager.GetFilesData(candidateId).Select(n=>n.Data).ToArray();
            }
            catch (FileException)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
        }

       
        [HttpGet("getCandidateFilesInfo")]
        public FileData[] GetCandidateFilesInfo()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return null;
            }
            if (!UsersManager.GetUser(token).HasRoot(RootEnum.Candidate))
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

            try
            {
                //Read Stream and convert to String     
                AbstractUser user = AuthManager.Instance[token];
                return FileManager.GetUserFilesList(user.Id);
                
            }
            catch (FileException)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
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


            try
            {
                AbstractUser user = AuthManager.Instance[token];
                FileManager.SubmitFile(user, input.Data, input.Bytes);
                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (ArgumentNullException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

        }

    }
}