using System;

namespace Model.Users
{
    public abstract class AbstractUser
    {
        public readonly int Id;

          
        public UserProfile Profile { get; set; }
        
        
        private RootEnum RootType { get; set; }


        public string PhotoUrl { get; set; } //todo url to photo


        protected AbstractUser(int id, RootEnum rootType)
        {
            Id = id;
            RootType = rootType;
            Profile = new UserProfile();
        }
      
        
        
        public override bool Equals(object obj)
        {
            return obj is AbstractUser user && user.Id == Id;
        }

        public bool HasRoot(RootEnum root)
        {
            return root == RootType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Profile, RootType, PhotoUrl);
        }
    }

    class AdminUser : AbstractUser
    {
        public AdminUser(int id) : base(id, RootEnum.Admin)
        {
        }
    }

    class ManagerUser : AbstractUser
    {
        public ManagerUser(int id) : base(id, RootEnum.Manager)
        {
        }
    }


    public class CandidateUser : AbstractUser
    {

        public AdmissionStatus Status { get; set; }

        public CandidateUser(int id) : base(id, RootEnum.Candidate)
        {
            Status = AdmissionStatus.Registered;
        }
        public enum AdmissionStatus
        {
            Registered,
            PassedTests,
            WaitingInterview,
            PassingInterview,
            Passed,
            Rejected,
        }

       
    }

    public class InterviewerUser : AbstractUser
    {
        public InterviewerUser(int id) : base(id, RootEnum.Interviewer)
        {
        }
    }
}