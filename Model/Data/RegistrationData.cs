using Model.Authentication;

namespace Model.Data
{
    public class RegistrationData : AuthData
    {
        public string Type { get; } = typeof(RegistrationData).ToString();

        public new string Data => "json";


        public string email;
        public long number;
        public string citezenship;
        public string acknowledgment;

        public string SerializeToJSON()
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
        }
    }
}