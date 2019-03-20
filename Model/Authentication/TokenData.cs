using System;
using Model.Data;

namespace Model.Authentication
{
    public class TokenData : IData
    {
        public string Type { get; }
        public string Data { get; }

        public string Token { get; set; }

        public virtual bool IsValid => false;

        public TokenData(string token)
        {
            Token = token;
        }

        public TokenData()
        {

        }
        
        
        public override bool Equals(object obj)
        {
            return obj is TokenData tokenData && tokenData.Token.Equals(Token);
        }
        
        public string SerializeToJSON()
        {
            throw new NotImplementedException();
        }

        public IData DeserializeFromJSON(string json)
        {
            throw new NotImplementedException();
        }
    }
}