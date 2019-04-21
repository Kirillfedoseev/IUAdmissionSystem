using System;
using Model.Data;

namespace Model.Authentication
{
    public class InterviewException:Exception
    {
        public InterviewException() : base("Something went wrong with FileManager") { }

        protected InterviewException(string message) : base(message) { }




        public class CandidateDoesntExistsException : InterviewException
        {
            public CandidateDoesntExistsException(int candidateId) : base($"The Candidate with {candidateId} doesn't Exists!"){}
        }

        public class InterviewerDoesntExistsException : InterviewException
        {
            public InterviewerDoesntExistsException(int interviewerId) : base($"The Interviewer with {interviewerId} doesn't Exists!") { }
        }

        public class CandidateNotReadyForInterviewException : InterviewException
        {
            public CandidateNotReadyForInterviewException(int candidateId) : base($"The Candidate with {candidateId} isn't ready for interview!") { }
        }

    }

   
}