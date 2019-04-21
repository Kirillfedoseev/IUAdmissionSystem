﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using Model.Interviews;
using Model.Users;
using System;
using System.Net;


namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    public class InterviewController : Controller
    {

        [HttpGet("interviewer/candidates")]
        public CandidateUser[] GetCandidatesForInterviewer()
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

              if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return null;
            }
            var interviewer = AuthManager.Instance[token];
            return InterviewManager.GetCandidateUserList(interviewer.Id);
        }

        [HttpPost("interviewer/updateGrade")]
        public void UpdateGrade([FromBody] GradeInfoData data) 
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString); //todo check token 
            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }

            InterviewManager.InterviewStatus status;
            switch (data.Grade)
            {
                case "Passed":
                    status = InterviewManager.InterviewStatus.Passed;
                    break;
                case "Fail":
                    status = InterviewManager.InterviewStatus.Fail;
                    break;
                default:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    throw new Exception("Wrong Grade Type. Should be Passed of Fail");
            }
            
            InterviewManager.SetInterviewResults(data.CandidateID, status);
        }

        [HttpGet("manager/interview/candidates")]
        public CandidateUser[] ShowAllCandidatesReadyForInterview() 
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return null;
            }
           

            return InterviewManager.GetCandidateUserList();

        }

        [HttpPost("manager/addInterview")]
        public void AddInterview([FromBody] InterviewInfoData data) 
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }
            


            InterviewManager.CreateInterview(data);

        }

        [HttpPost("manager/editInterview")]
        public void EditInterview([FromBody] InterviewInfoData data) 
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);
            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }
           

            InterviewManager.DeleteInterview(data);
            InterviewManager.CreateInterview(data);
        }

        [HttpPost("manager/deleteInterview")]
        public void DeleteInterview([FromBody] InterviewInfoData data) 
        {
            var tokenString = Request.Headers["Authorization"];
            var token = new TokenData(tokenString);

            if (!AuthManager.ValidateAuthToken(token))
            {
                Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
                return;
            }

            InterviewManager.DeleteInterview(data);
        }


    }
}