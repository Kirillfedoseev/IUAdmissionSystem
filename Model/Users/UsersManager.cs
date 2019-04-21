using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Support;

namespace Model.Users
{
    public class UsersManager:Singletone<UsersManager>
    {

        private Dictionary<Type, List<AbstractUser>> _specificUserLists;


        public UsersManager()
        {

            _specificUserLists = new Dictionary<Type, List<AbstractUser>>();

            var subclassTypes = Assembly.GetAssembly(typeof(AbstractUser)).GetTypes().Where(t => t.IsSubclassOf(typeof(AbstractUser)) && !t.IsAbstract);
            foreach (var subclassType in subclassTypes)
            {

                Type genericListType = typeof(List<>).MakeGenericType(subclassType);
                Activator.CreateInstance(genericListType);
                //todo _specificUserLists.Add(subclassType, (List<AbstractUser>) Activator.CreateInstance(genericListType));
            }
        }

        public static AbstractUser CreateUser(RootEnum[] roots)
        {
            //todo logic of understanding roots
            CandidateUser user = new CandidateUser();
            //todo Instance._specificUserLists[user.GetType()].Add(user);
            return user;
        }


        public static AbstractUser GetUserByID(int id) 
            => Instance._specificUserLists.Values.SelectMany(n => n).Single(i => i.id == id);


        public static T GetUserByID<T>(int id) where T : AbstractUser 
            => Instance._specificUserLists[typeof(T)].SingleOrDefault(i => i.id == id) as T;

        public static AbstractUser[] GetUsersByIDs(int[] ids)
            => Instance._specificUserLists.Values.SelectMany(n => n).Where(i => ids.Contains(i.id)).ToArray();


        public static T[] GetUsersByIDs<T>(int[] ids) where T : AbstractUser
            => Instance._specificUserLists[typeof(T)].Where(i => ids.Contains(i.id)) as T[];


        public static bool IsUserExistsByID(int id)
            => Instance._specificUserLists.Values.SelectMany(n => n).Any(i => i.id == id);


        public static bool IsUserExistsByID<T>(int id) where T : AbstractUser
            => Instance._specificUserLists[typeof(T)].Any(i => i.id == id);


        public static void DeleteUserById(int id)
        {
            foreach (var list in Instance._specificUserLists.Values)
            {
                if(list.RemoveAll(n=> n.id == id) > 0)
                    break;
            }
        }
    }
}
