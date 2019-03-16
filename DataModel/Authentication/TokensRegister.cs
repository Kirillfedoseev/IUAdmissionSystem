using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using DataModel.Data;
using DataModel.Users;

namespace DataModel.Authentication
{
    public class TokensRegister
    {
        private readonly Dictionary<TokenData, AbstractUser> _tokensOfUsers;

        public TokensRegister()
        {
            _tokensOfUsers = new Dictionary<TokenData, AbstractUser>();
        }


        public void FreeToken(TokenData tokenData)
        {
            if (tokenData == null) return;
            _tokensOfUsers.Remove(tokenData);
        }
        
        
        public AbstractUser ValidateAuthToken(TokenData token)
        {
            var pair = _tokensOfUsers.FirstOrDefault(n => n.Key.Equals(token));
            
            if (pair.Key == null) throw new Exception("Incorrecct token!");
            
            if (!pair.Key.IsValid)
            {
                FreeToken(pair.Key);
                throw new Exception("Token's ttl was exceeded, please login again.");
            }
            
            return pair.Value;
        }      
        
        public TokenData GetAuthToken(AbstractUser user)
        {
            if (user == null) throw new ArgumentException("Passed paramet 'User' was null");
            var pair = _tokensOfUsers.FirstOrDefault(n => n.Value.Equals(user));
            
            // free logged in another device
            if (pair.Key != null)
                FreeToken(pair.Key);
            
            var tokenData = new TokenData();
            _tokensOfUsers.Add(tokenData, user);
            return tokenData;
        }
    }

    public class TokenData : IData
    {
        public string Type { get; }
        public string Data { get; }

        private const int TTL = 3600;
        
        private readonly DateTime CreateTime;

        private DateTime UpdateTime;

        public readonly string Token;
        
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
            
            using (var sha256 = SHA256.Create())
            {
                Token = Convert.ToBase64String(
                    sha256.ComputeHash(
                        Guid.NewGuid()
                            .ToByteArray()
                    ));              
            };
        }

        public override bool Equals(object obj)
        {
            return obj is TokenData tokenData && tokenData.Token.Equals(Token);
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