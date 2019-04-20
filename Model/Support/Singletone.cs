namespace Model.Support
{
    public class Singletone<T> where T : class, new()
    {

        public static T Instance => _instance ?? (_instance = new T());

        private static T _instance;
                
    }
}