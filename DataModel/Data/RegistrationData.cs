using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;

namespace DataModel.Data
{
    public class RegistrationData : IData
    {
        public string Type { get; } = typeof(RegistrationData).ToString();

        public string Data
        {
            get { return "json"; }

        }

        public string login;

        public string password;

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
    }
}