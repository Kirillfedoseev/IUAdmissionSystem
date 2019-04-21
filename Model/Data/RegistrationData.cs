using Model.Users;

namespace Model.Data
{
    public class RegistrationData : AuthData
    {

        public override string Type { get; } = typeof(RegistrationData).ToString();

        public new string Data => "json";

        public UserProfile Profile { get; set; }
        
        public RootEnum RootType;



        public override string SerializeToJSON()

        {
            //            {
            //                type:"type",
            //                Data:
            //                {
            //                    data1:"data",
            //                    data2:"data"
            //                }
            //            }

            throw new System.NotImplementedException();
        }

        public IData DeserializeFromJSON()
        {
            throw new System.NotImplementedException();
        }

        public RegistrationData(string login, string password) : base(login, password)
        {
            RootType = RootEnum.Candidate;
        }
    }
}