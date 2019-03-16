using System.Collections.Generic;
using System.Linq;
using DataModel.Data;
using DataModel.Support;
using DataModel.Users;

namespace DataModel
{
    public class DataManager:Singletone<DataManager>
    {
        
        private readonly Dictionary<AbstractUser, List<IData>> _data;
        
        
        public DataManager()
        {
            _data = LoadData() ?? new Dictionary<AbstractUser, List<IData>>();
        }
        
        
        /// <summary>
        /// Add data of any type, which derived from <see cref="IData">
        /// And assign it to user
        /// </summary>
        /// <param name="user">user, which submit data</param>
        /// <param name="data">submitted data</param>
        public static void AddData(AbstractUser user, IData data)
        {
            if(Instance._data.ContainsKey(user))
                Instance._data[user].Add(data);
            else
                Instance._data.Add(user, new List<IData>(){data});
        }

        
        /// <summary>
        /// Get data of particular user of type T,
        /// type T should be derrived from <see cref="IData">
        /// </summary>
        /// <param name="user">user, for which data should be retrived</param>
        /// <typeparam name="T">type of data, such as: AuthData, TestData or IData for all user's Data</typeparam>
        /// <returns>enumerable of retrieved data casted to type T</returns>
        public static IEnumerable<T> GetData<T>(AbstractUser user) where T:IData
        {
            return Instance._data[user].Where(n => n.GetType().IsSubclassOf(typeof(T))).Select(n=>(T)n);
        }
        
        
        /// <summary>
        /// Remove all data of particular user of type T,
        /// type T should be derrived from <see cref="IData">
        /// </summary>
        /// <param name="user">user, for whose data should be deleted</param>
        /// <typeparam name="T">type of data, such as: AuthData, TestData or IData for all user's Data</typeparam>
        public static void RemoveData<T>(AbstractUser user)
        {
           Instance._data[user].RemoveAll(n => n.GetType().IsSubclassOf(typeof(T)));
        }
        
        
        private Dictionary<AbstractUser, List<IData>> LoadData()
        {
            return null;
        }

        
        private void SaveData()
        {
            //todo
        }                
    }
}