using System;
using System.Collections.Generic;
using System.Linq;
using Model.Data;
using Model.Support;
using Model.Users;

namespace Model.Interviews
{
    public class InterviewManager:Singletone<InterviewManager>
    {
        private readonly List<int> _readyCandidates;

        private readonly List<InterviewInfoData> _interviews;

        public InterviewManager()
        {
            _interviews = new List<InterviewInfoData>();
            _readyCandidates = new List<int>();
        }

        public static void CreateInterview(InterviewInfoData info)
        {
            if(!UsersManager.IsUserExistsByID<CandidateUser>(info.CandidateID))
                throw new Exception("Candidate doesn't exists!");

            if (!UsersManager.IsUserExistsByID<InterviewerUser>(info.InterviewerID))
                throw new Exception("Interviewer doesn't exists!");

            if (Instance._readyCandidates.Any(n => n == info.CandidateID))
            {
                UsersManager.GetUserByID<CandidateUser>(info.CandidateID).Status =
                    CandidateUser.AdmissionStatus.PassingInterview;

                Instance._interviews.Add(info);
                Instance._readyCandidates.Remove(info.CandidateID);
            }
            else
            {
                if (UsersManager.GetUserByID<CandidateUser>(info.CandidateID).Status ==
                    CandidateUser.AdmissionStatus.WaitingInterview)
                {
                    Instance._interviews.Add(info);
                }
                else
                {
                    throw new Exception("The Candidate doesn't ready for interview!");
                }
            }

            
        }

        public static void DeleteInterview(InterviewInfoData info)
        {
            if (Instance._interviews.Remove(info))
            {
                UsersManager.GetUserByID<CandidateUser>(info.CandidateID).Status = CandidateUser.AdmissionStatus.WaitingInterview;
                Instance._readyCandidates.Add(info.CandidateID);
            }          
        }


        public static void AddCandidateToInterviewQueue(int candidateID)
        {
            if (!UsersManager.IsUserExistsByID<CandidateUser>(candidateID))
                throw new Exception("Candidate doesn't exists!");

            UsersManager.GetUserByID<CandidateUser>(candidateID).Status = CandidateUser.AdmissionStatus.WaitingInterview;

            Instance._readyCandidates.Add(candidateID);
        }

        public static void SetInterviewResults(int candidateID, InterviewStatus status)
        {
            CandidateUser candidate = UsersManager.GetUserByID<CandidateUser>(candidateID);

            if (candidate == null)
                throw new Exception("Candidate doesn't exists!");

            Instance._interviews.RemoveAll(n => n.CandidateID == candidateID);

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
            => UsersManager.GetUsersByIDs<CandidateUser>(Instance._readyCandidates.ToArray());

        public static CandidateUser[] GetCandidateUserList(int interviewerId)
            => UsersManager.GetUsersByIDs<CandidateUser>(Instance._interviews.Where(n => n.InterviewerID == interviewerId).Select(n=>n.CandidateID).ToArray());


        public enum InterviewStatus
        {
            Passed,
            Fail,
        }

    }
}
