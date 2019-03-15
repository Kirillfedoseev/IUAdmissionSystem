using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;


namespace DataModel.Data
{
    public class TokenData:IData
    {
        public string Type { get; } = typeof(TokenData).ToString();

        //TODO: No needed
        public string Data
        {
            get { return "json"; }

        }

        public string token;
        
        //TODO: No needed
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

        //TODO: No needed
        public IData DeserializeFromJSON()
        {
            throw new System.NotImplementedException();
        }
    }
}
