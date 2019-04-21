using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Support;

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
                _specificUserLists.Add(subclassType, (List<AbstractUser>) Activator.CreateInstance(genericListType));
            }
        }

        public static AbstractUser CreateUser(RootEnum rootsType)
        {
            AbstractUser user;

            switch (rootsType)
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
                    throw new ArgumentOutOfRangeException(nameof(rootsType), rootsType, null);
            }

            return user;
        }


        public static AbstractUser GetUserById(int id) 
            => Instance._specificUserLists.Values.SelectMany(n => n).Single(i => i.Id == id);


        public static T GetUserById<T>(int id) where T : AbstractUser 
            => Instance._specificUserLists[typeof(T)].SingleOrDefault(i => i.Id == id) as T;

        public static AbstractUser[] GetUsersByIds(int[] ids)
            => Instance._specificUserLists.Values.SelectMany(n => n).Where(i => ids.Contains(i.Id)).ToArray();


        public static T[] GetUsersByIDs<T>(int[] ids) where T : AbstractUser
            => Instance._specificUserLists[typeof(T)].Where(i => ids.Contains(i.Id)) as T[];


        public static bool IsUserExistsById(int id)
            => Instance._specificUserLists.Values.SelectMany(n => n).Any(i => i.Id == id);


        public static bool IsUserExistsById<T>(int id) where T : AbstractUser
            => Instance._specificUserLists[typeof(T)].Any(i => i.Id == id);


        public static void DeleteUserById(int id)
        {
            foreach (var list in Instance._specificUserLists.Values)
            {
                if(list.RemoveAll(n=> n.Id == id) > 0)
                    break;
            }
        }
    }
}
