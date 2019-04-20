using System.Collections.Generic;

namespace Model.Users
{
    public abstract class AbstractUser
    {
        public readonly int id;

          
        public UserProfile Profile { get; internal set; }
        
        
        private List<RootEnum> Roots { get; set; }


        public string PhotoUrl { get; set; } //todo url to photo


        protected AbstractUser(RootEnum[] roots)
        {
            id = 1; //todo genrate id
        }
      
        
        
        public override bool Equals(object obj)
        {
            return obj is AbstractUser user && user.id == id;
        }

        public bool HasRoot(RootEnum root)
        {
            return Roots.Contains(root);
        }
        
    }

    class AdminUser : AbstractUser
    {
        public AdminUser() : base(new RootEnum[0])
        {
        }
    }

    class ManagerUser : AbstractUser
    {
        public ManagerUser(RootEnum[] roots) : base(new RootEnum[0])
        {
        }
    }


    public class CandidateUser : AbstractUser
    {

        public AdmissionStatus Status { get; set; }

        public CandidateUser() : base(new RootEnum[0])
        {
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
        public InterviewerUser() : base(new RootEnum[0])
        {
        }
    }

    public class TestUser : AbstractUser
    {
        public TestUser(RootEnum[] roots) : base(roots)
        {

        }
    }
}