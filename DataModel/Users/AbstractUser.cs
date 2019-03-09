using System.Collections.Generic;
using DataModel.Data;

namespace DataModel.Users
{
    public abstract class AbstractUser
    {
        private int id;
        public string Name { get; protected set; }
        protected string Password { get; set; }
        protected List<RootEnum> Roots { get; set; }

        public List<IData> Datas { get; protected set; }

        
        
        public bool HasRoot(RootEnum root)
        {
            return Roots.Contains(root);
        }
        
        
    }
}