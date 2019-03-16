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

        /// <summary>
        /// Remove token form register
        /// </summary>
        /// <param name="tokenData">token, which should be removed</param>
        public void FreeToken(TokenData tokenData)
        {
            if (tokenData == null) return;
            _tokensOfUsers.Remove(tokenData);
        }
        
        /// <summary>
        /// Validate passed token
        /// Check on existing in register
        /// Check on expiring
        /// </summary>
        /// <param name="token">token, which should be checked</param>
        /// <returns>User with such token, if it exists</returns>
        /// <exception cref="TokenDoesNotExists">If registed doesn't have such token</exception>
        /// <exception cref="TokenExpired">If token in register, but expired</exception>
        public AbstractUser ValidateAuthToken(TokenData token)
        {
            var pair = _tokensOfUsers.FirstOrDefault(n => n.Key.Equals(token));
            
            if (pair.Key == null) throw new TokenExceptions.TokenDoesNotExists(token);
            
            if (!pair.Key.IsValid)
            {
                FreeToken(pair.Key);
                throw new TokenExceptions.TokenExpired(token);
            }
            
            return pair.Value;
        }      
        
        /// <summary>
        /// Generate new auth token for particular user
        /// </summary>
        /// <param name="user">Particular user</param>
        /// <returns>Valid auth token</returns>
        /// <exception cref="ArgumentException">If user equal to null</exception>
        public TokenData GetAuthToken(AbstractUser user)
        {
            if (user == null) throw new ArgumentException("Passed parameter 'user' was null");
            var pair = _tokensOfUsers.FirstOrDefault(n => n.Value.Equals(user));
            
            // free logged in another device
            if (pair.Key != null)
                FreeToken(pair.Key);
            
            var tokenData = new TokenData();
            tokenData.Initialize();
            _tokensOfUsers.Add(tokenData, user);
            return tokenData;
        }
    }

    public class TokenData : IData
    {
        public string Type { get; }
        public string Data { get; }

        private const int TTL = 3600;
        
        private DateTime CreateTime;

        private DateTime UpdateTime;

        public string Token { get; private set; }
        
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
            
        }
        
        internal void Initialize()
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