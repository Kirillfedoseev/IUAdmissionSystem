using System;
using DataModel.Data;

namespace DataModel.Users
{
    public class TokensRegister
    {
        
        
        
        
        
        public string GetAuthToken()
        {
            return "";
        }
    }

    class TokenData : IData
    {
        public string Type { get; }
        public string Data { get; }

        private const int TTL = 3600;
        
        private readonly DateTime CreateTime;

        private DateTime UpdateTime;

        private readonly string _token;
        
        public bool IsValid
        {
            get
            {
                if ((DateTime.Now - UpdateTime).Seconds > TTL)
                    return false;
                
                UpdateTime = DateTime.Now;
                return true;
            }
        }
            
        public TokenData()
        {
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }
        
        
        public string SerializeToJSON()
        {
            throw new System.NotImplementedException();
        }

        public IData DeserializeFromJSON(string json)
        {
            throw new System.NotImplementedException();
        }
    }
    
}