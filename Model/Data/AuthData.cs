using Newtonsoft.Json.Linq;

namespace Model.Data
{
    public class AuthData:IData
    {
        
        public virtual string Type => typeof(AuthData).ToString();
        
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
        
        
        public virtual string SerializeToJSON()
        {
            var jObject = new JObject
            {
                {"type", Type},
                {"data", Data}
            };
            return jObject.ToString();
        }


        public virtual IData DeserializeFromJSON(string json)
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