using DataModel.Data;

namespace DataModel.Tests
{
    public class AbstractTest:IData
    {
        public string Type { get; }
        public string Data { get; }
        public string SerializeToJSON()
        {
            throw new System.NotImplementedException();
        }

        public IData DeserializeFromJSON()
        {
            throw new System.NotImplementedException();
        }
    }
}