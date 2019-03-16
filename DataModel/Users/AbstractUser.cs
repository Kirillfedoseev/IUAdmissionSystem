using System.Collections.Generic;
using DataModel.Data;

namespace DataModel.Users
{
    public abstract class AbstractUser
    {
        private readonly int id;

        protected AbstractUser(RootEnum[] roots)
        {
            id = 1; //todo genrate id
            // throw new System.NotImplementedException();
        }

        public string Name { get; protected set; }
        protected string Password { get; set; }
        protected List<RootEnum> Roots { get; set; }

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