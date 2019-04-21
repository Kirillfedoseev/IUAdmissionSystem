using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Authentication;
using Model.Data;
using Model.Interviews;
using Model.Support;
using static Model.Users.CandidateUser;

namespace Model.Users
{
    public class UsersManager:Singletone<UsersManager>
    {
        private int LastId { get; set; }

        private readonly Dictionary<Type, List<AbstractUser>> _specificUserLists;


        public UsersManager()
        {
            _specificUserLists = new Dictionary<Type, List<AbstractUser>>();
            LastId = 0;

            var subclassTypes = Assembly.GetAssembly(typeof(AbstractUser)).GetTypes().Where(t => t.IsSubclassOf(typeof(AbstractUser)) && !t.IsAbstract);
            foreach (var subclassType in subclassTypes)
            {
                Type genericListType = typeof(List<>).MakeGenericType(subclassType);
                Activator.CreateInstance(genericListType);
                _specificUserLists.Add(subclassType, new List<AbstractUser>());
            }
        }


        public static AbstractUser CreateUser(RegistrationData regData)
        {
            AbstractUser user;

            switch (regData.RootType)
            {
                case RootEnum.Candidate:
                    user = new CandidateUser(++Instance.LastId);
                    Instance._specificUserLists[typeof(CandidateUser)].Add(user);
                    break;
                case RootEnum.Manager:
                    user = new ManagerUser(++Instance.LastId);
                    Instance._specificUserLists[typeof(ManagerUser)].Add(user);
                    break;
                case RootEnum.Interviewer:
                    user = new InterviewerUser(++Instance.LastId);
                    Instance._specificUserLists[typeof(InterviewerUser)].Add(user);
                    break;
                case RootEnum.Admin:
                    user = new AdminUser(++Instance.LastId);
                    Instance._specificUserLists[typeof(AdminUser)].Add(user);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(regData.RootType), regData.RootType, null);
            }

            user.Profile = regData.Profile;

            return user;
        }
        

        /// <summary>
        /// Get user's profile by token
        /// </summary>
        /// <param name="authToken">Auth token, , which was got witihin authentication or registration</param>
        /// <exception cref=""></exception>
        /// <returns>User profile data</returns>
        public static UserProfile GetUserProfile(TokenData authToken)
            => GetUser(authToken).Profile;

        public static UserProfile GetUserProfile(int id)
            => GetUser(id).Profile;

        public static void SetUserProfile(TokenData authToken, UserProfile profile)
            => GetUser(authToken).Profile = profile;


        public static void SetUserStatus(StatusUpdateData data)
        {
            CandidateUser user = GetUser<CandidateUser>(data.CandidateId);

            if (user == null)
                throw new InterviewException.CandidateDoesntExistsException(data.CandidateId);

            switch (data.Status)
            {
                case AdmissionStatus.Registered:
                    user.Status = AdmissionStatus.Registered;
                    break;
                case AdmissionStatus.PassedTests:
                    user.Status = AdmissionStatus.PassedTests;
                    InterviewManager.AddCandidateToInterviewQueue(data.CandidateId);
                    break;
                case AdmissionStatus.Passed:
                    user.Status = AdmissionStatus.Passed;
                    break;
                case AdmissionStatus.Rejected:
                    user.Status = AdmissionStatus.Rejected;
                    break;
            }

        }


        public static AbstractUser GetUser(TokenData authToken)
            => AuthManager.Instance[authToken];


        public static AbstractUser GetUser(int id)
            => Instance._specificUserLists.Values.SelectMany(n => n).Single(i => i.Id == id);


        public static T GetUser<T>(int id) where T : AbstractUser 
            => Instance._specificUserLists[typeof(T)].SingleOrDefault(i => i.Id == id) as T;


        public static AbstractUser[] GetUsers(int[] ids)
            => Instance._specificUserLists.Values.SelectMany(n => n).Where(i => ids.Contains(i.Id)).ToArray();


        public static T[] GetUsers<T>(int[] ids) where T : AbstractUser
            => Instance._specificUserLists[typeof(T)].Where(i => ids.Contains(i.Id)) as T[];


        public static bool IsUserExists(int id)
            => Instance._specificUserLists.Values.SelectMany(n => n).Any(i => i.Id == id);


        public static bool IsUserExists<T>(int id) where T : AbstractUser
            => Instance._specificUserLists[typeof(T)].Any(i => i.Id == id);


        public static void DeleteUser(int id) 
            => Instance._specificUserLists.Values.ToList().ForEach(n=>n.RemoveAll(i => i.Id == id));
    }
}
