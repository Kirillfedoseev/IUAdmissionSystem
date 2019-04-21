using System.Collections.Generic;
using System.Linq;
using Model.Authentication;
using Model.Data;
using Model.Support;
using Model.Users;

namespace Model.Interviews
{
    public class InterviewManager:Singletone<InterviewManager>
    {
        private readonly List<CandidateUser> _readyCandidates;

        private readonly Dictionary<CandidateUser, InterviewerUser> _interviewPairs;

        private readonly List<CandidateUser> _interviewResults;


        public InterviewManager()
        {
            _interviewPairs = new Dictionary<CandidateUser, InterviewerUser>();
            _readyCandidates = new List<CandidateUser>();
        }

        public static void CreateInterview( int candidateID, int interviewerID)
        {
            CandidateUser candidate = Instance._readyCandidates.SingleOrDefault(n => n.id == candidateID);

            InterviewerUser interviewer = UsersManager.GetUserByID<InterviewerUser>(interviewerID);
            Instance._interviewPairs.Add(candidate,interviewer); //todo checks on existance              
            Instance._readyCandidates.Remove(candidate);
        }

        public static void DeleteInterview(int candidateID, int interviewerID)
        {
            CandidateUser candidate = Instance._interviewPairs.SingleOrDefault(n => n.Key.id == candidateID).Key;
            Instance._interviewPairs.Remove(candidate);
            candidate.Status = CandidateUser.AdmissionStatus.WaitingInterview;
            Instance._readyCandidates.Add(candidate);
        }



        public static void AddCandidateToInterviewQueue(int candidateID)
        {
            var candidate = UsersManager.GetUserByID<CandidateUser>(candidateID);
            candidate.Status = CandidateUser.AdmissionStatus.WaitingInterview;
            Instance._readyCandidates.Add(candidate);
        }

        public static void SetInterviewResults(int candidateID, InterviewStatus status)
        {
            CandidateUser candidate = Instance._interviewPairs.SingleOrDefault(n => n.Value.id == candidateID).Key;
            Instance._interviewPairs.Remove(candidate);
            switch (status)
            {
                case InterviewStatus.Passed:
                    candidate.Status = CandidateUser.AdmissionStatus.Passed;
                    break;
                case InterviewStatus.Fail:
                    candidate.Status = CandidateUser.AdmissionStatus.Rejected;
                    break;
            }

        }


        public static CandidateUser[] GetCandidateUserList() 
            => Instance._readyCandidates.ToArray();

        public static CandidateUser[] GetCandidateUserList(TokenData tokenData)
            => Instance._interviewPairs.Where(n => n.Value.id == AuthManager.Instance[tokenData].id).Select(n => n.Key).ToArray();


        public enum InterviewStatus
        {
            Passed,
            Fail,

        }

    }
}
