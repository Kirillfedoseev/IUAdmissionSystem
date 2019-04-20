using System.Collections.Generic;
using Model.Support;
using Model.Users;

namespace Model.Interviews
{
    public class InterviewManager:Singletone<InterviewManager>
    {
        private readonly Dictionary<InterviewerUser, CandidateUser> _interviewPairs;


        public InterviewManager()
        {
            _interviewPairs = new Dictionary<InterviewerUser, CandidateUser>();

        }

        public static void CreateInterview(int interviewer_id, int candidate_id)
        {

            InterviewerUser interviewer = UsersManager.GetUserByID<InterviewerUser>(interviewer_id);
            CandidateUser candidate = UsersManager.GetUserByID<CandidateUser>(candidate_id);
            Instance._interviewPairs.Add(interviewer,candidate); //todo checks on existance
                

        }


        //public static void SetCandidateStatus()

        //public static CandidateUser GetCandidate(int id) => Instance._candidates.Single(n => n.id == id);



    }
}
