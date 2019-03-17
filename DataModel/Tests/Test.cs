using System.Collections.Generic;
using DataModel.Data;

namespace DataModel.Tests
{
    public class Test:IData
    {
        public string Type => GetType().ToString();
        
        public string Data { get; }
        

        public string Name;
                
        public string Description { get; protected set; }
        
        //todo add other properties

        public List<ITestableItem> testItems;
        
        
        
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