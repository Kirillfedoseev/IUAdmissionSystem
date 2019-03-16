using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataModel.Data
{
    public class AuthData:IData
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
            
        }
    }
}