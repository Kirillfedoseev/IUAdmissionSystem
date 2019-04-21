using System;
using System.Collections.Generic;
using System.Linq;
using Model.Data;
using Model.Support;
using Model.Users;
using static Model.Authentication.InterviewException;
using static Model.Users.CandidateUser.AdmissionStatus;

namespace Model.Interviews
{
    public class InterviewManager:Singletone<InterviewManager>
    {

        /// <summary>
        /// The Candidates' ids, which are ready for interview
        /// </summary>
        private readonly List<int> _readyCandidates;

        /// <summary>
        /// The Interview Info for candidate and interviewer, which scheduled for interview
        /// </summary>
        private readonly List<InterviewInfoData> _interviews;

        /// <summary>
        /// Constructor for Signletone
        /// </summary>
        public InterviewManager()
        {
            _interviews = new List<InterviewInfoData>();
            _readyCandidates = new List<int>();
        }
        
        /// <summary>
        /// Get all Candidate, which are ready for interview
        /// </summary>
        /// <returns>Array of Candidates</returns>
        public static CandidateUser[] GetCandidateUserList()
            => UsersManager.GetUsersByIDs<CandidateUser>(Instance._readyCandidates.ToArray());

        /// <summary>
        /// Get all Candidate, which are assigned for particular interviewer
        /// </summary>
        /// <param name="interviewerId">Id of Interviewer User</param>
        /// <returns>Array of Candidates</returns>
        public static CandidateUser[] GetCandidateUserList(int interviewerId)
            => UsersManager.GetUsersByIDs<CandidateUser>(Instance._interviews.Where(n => n.InterviewerID == interviewerId).Select(n => n.CandidateID).ToArray());


        /// <summary>
        /// Create interview with such Interview Info
        /// </summary>
        /// <param name="info">Consists of candidate id, interview id and also time of interview</param>
        /// <exception cref="CandidateNotReadyForInterviewException">if candidate isn't ready for interview</exception>
        /// <exception cref="InterviewerDoesntExistsException">if interviewer id doesn't exists</exception>
        /// <exception cref="CandidateDoesntExistsException">if candidate id doesn't exists</exception>
        public static void CreateInterview(InterviewInfoData info)
        {
            if(!UsersManager.IsUserExistsByID<CandidateUser>(info.CandidateID))
                throw new CandidateDoesntExistsException(info.CandidateID);

            if (!UsersManager.IsUserExistsByID<InterviewerUser>(info.InterviewerID))
                throw new InterviewerDoesntExistsException(info.InterviewerID);

            if (Instance._readyCandidates.Any(n => n == info.CandidateID))
            {
                UsersManager.GetUserByID<CandidateUser>(info.CandidateID).Status =
                    PassingInterview;

                Instance._interviews.Add(info);
                Instance._readyCandidates.Remove(info.CandidateID);
            }
            else
            {
                if (UsersManager.GetUserByID<CandidateUser>(info.CandidateID).Status ==
                    WaitingInterview)
                {
                    UsersManager.GetUserByID<CandidateUser>(info.CandidateID).Status =
                        PassingInterview;

                    Instance._interviews.Add(info);
                }
                else
                {
                    throw new CandidateNotReadyForInterviewException(info.CandidateID);
                }
            }

            
        }


        /// <summary>
        /// Delete interview with the same parameters as was passed
        /// </summary>
        /// <param name="info">Consists of candidate id, interview id and also time of interview</param>
        /// <exception cref="InterviewerDoesntExistsException">if interviewer id doesn't exists</exception>
        /// <exception cref="CandidateDoesntExistsException">if candidate id doesn't exists</exception>
        public static void DeleteInterview(InterviewInfoData info)
        {
            if (!UsersManager.IsUserExistsByID<CandidateUser>(info.CandidateID))
                throw new CandidateDoesntExistsException(info.CandidateID);

            if (!UsersManager.IsUserExistsByID<InterviewerUser>(info.InterviewerID))
                throw new InterviewerDoesntExistsException(info.InterviewerID);


            if (!Instance._interviews.Remove(info)) return;

            UsersManager.GetUserByID<CandidateUser>(info.CandidateID).Status = WaitingInterview;
            Instance._readyCandidates.Add(info.CandidateID);
        }


        /// <summary>
        /// Add candidate to queue, where all ready candidates for interview should be
        /// </summary>
        /// <param name="candidateId">Id of Candidate User</param>
        /// <exception cref="CandidateDoesntExistsException">if candidate id doesn't exists</exception>
        public static void AddCandidateToInterviewQueue(int candidateId)
        {
            var candidate = UsersManager.GetUserByID<CandidateUser>(candidateId);

            if (candidate == null)
                throw new CandidateDoesntExistsException(candidateId);

            candidate.Status = WaitingInterview;

            Instance._readyCandidates.Add(candidateId);
        }


        /// <summary>
        /// Set interview results and delete interview from list
        /// Also set to Candidate SUer status Passed or Rejected of Admission program
        /// </summary>
        /// <param name="candidateId">Id of Candidate User</param>
        /// <param name="status">Result of interview</param>
        /// <exception cref="CandidateDoesntExistsException">if candidate id doesn't exists</exception>
        public static void SetInterviewResults(int candidateId, InterviewStatus status)
        {
            CandidateUser candidate = UsersManager.GetUserByID<CandidateUser>(candidateId);

            if (candidate == null)
                throw new CandidateDoesntExistsException(candidateId);

            Instance._interviews.RemoveAll(n => n.CandidateID == candidateId);

            switch (status)
            {
                case InterviewStatus.Passed:
                    candidate.Status = Passed;
                    break;
                case InterviewStatus.Fail:
                    candidate.Status = Rejected;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

       
        /// <summary>
        /// Statuses of interview as result of it
        /// </summary>
        public enum InterviewStatus
        {
            Passed,
            Fail,
        }

    }
}
