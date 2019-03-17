using System;
using System.Collections.Generic;

namespace DataModel.Users
{
    public abstract class AbstractUser
    {
        private readonly int id;

          
        public UserProfile Profile { get; internal set; }
        
        
        private List<RootEnum> Roots { get; set; }
        
        
        protected AbstractUser(RootEnum[] roots)
        {
            id = 1; //todo genrate id
        }
      
        
        
        public override bool Equals(object obj)
        {
            return obj is AbstractUser user && user.id == id;
        }

        public bool HasRoot(RootEnum root)
        {
            return Roots.Contains(root);
        }
        
    }
}