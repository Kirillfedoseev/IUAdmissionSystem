using Newtonsoft.Json.Linq;

namespace Model.Data
{
#pragma warning disable CS0659 // "AuthData" переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode().
    public class AuthData:IData
#pragma warning restore CS0659 // "AuthData" переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode().
    {
        
        public string Type => typeof(AuthData).ToString();
        
        public string Data
        {
            get
            {                    
                var jObject = new JObject
                {
                    {"login", Login},
                    {"password", Password}
                };
                return jObject.ToString();
            }
            
        }
        
        
        public string Login;
        
        public string Password;

        public AuthData(string login, string password)
        {
            Login = login;
            Password = password;
        }
        
        
        public string SerializeToJSON()
        {
            var jObject = new JObject
            {
                {"type", Type},
                {"data", Data}
            };
            return jObject.ToString();
        }


        public IData DeserializeFromJSON(string json)
        {
            JObject jObject = new JObject(json);
            JToken type = jObject["type"];
//            
//            if ( == typeof(AuthData).ToString())
//            {
//                
//            }
            return null; //todo;
        }


        public override bool Equals(object obj)
        {
            var authData = obj as AuthData;
            return string.Equals(authData?.Login, Login);
        }
    }
}