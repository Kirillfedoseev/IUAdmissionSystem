using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Authentication;
using Model.Data;
using System;
using System.Linq;
using Model.Programs;
using Model.Tests;
using Model.Users;

namespace WebApp.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet("test")]
        public void Test()
        {
            //Default Test User Creation
            {
                var token = AuthManager.RegisterUser(new RegistrationData("user", "password")
                {
                    RootType = RootEnum.Candidate
                });

                TestsManager.CreateTest(new TestData
                {
                    Name = "English",
                    Questions = new TestData.Question[2]
                    {
                    new TestData.Question
                    {
                        Answers = new TestData.Question.Answer[3]
                        {
                            new TestData.Question.Answer
                            {
                                AnswerText = "A or b",
                                IsCorrect = true
                            },
                            new TestData.Question.Answer
                            {
                                AnswerText = "nor A , nor b",
                                IsCorrect = true
                            },
                            new TestData.Question.Answer
                            {
                                AnswerText = "A and B",
                                IsCorrect = false
                            }

                        },
                        QuestionText = "A or B is good letters?"

                    },
                    new TestData.Question
                    {
                        Answers = new TestData.Question.Answer[3]
                        {
                            new TestData.Question.Answer
                            {
                                AnswerText = "A or C",
                                IsCorrect = true
                            },
                            new TestData.Question.Answer
                            {
                                AnswerText = "nor A , nor C",
                                IsCorrect = true
                            },
                            new TestData.Question.Answer
                            {
                                AnswerText = "A and C",
                                IsCorrect = false
                            }

                        },
                        QuestionText = "A or C is good letters?"

                    },
                    }
                });

                ProgramsManager.CreateProgram(new Model.Programs.ProgramData()
                {
                    Name = "Bachelor 1st year",
                    Course = "Bachelor",
                });

                ProgramsManager.AssignTestToProgram(new TestAssigningData()
                {
                    ProgramId = ProgramsManager.Instance._programs.First().Id,
                    TestId = TestsManager.Instance.Tests.First().TestId,
                });

                token = AuthManager.AuthUser(new AuthData("user", "password"));

                ProgramsManager.AssignCandidateToProgram(new UserAssigningData()
                {
                    CandidateId = UsersManager.GetUser(token).Id,
                    ProgramId = ProgramsManager.Instance._programs.First().Id,
                });

                AuthManager.LogOutUser(token);

            }

            
            //Admin Creation
            {
                var token = AuthManager.RegisterUser(new RegistrationData("admin", "admin")
                {
                    RootType = RootEnum.Admin
                });

                AuthManager.LogOutUser(token);
            }

        }


    }
}
