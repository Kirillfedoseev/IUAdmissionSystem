using System;

namespace Model.Data
{
#pragma warning disable CS0659 // "TokenData" переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode().
    public class TokenData : IData
#pragma warning restore CS0659 // "TokenData" переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode().
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