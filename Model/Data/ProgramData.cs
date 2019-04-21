using Model.Data;

namespace Model.Programs
{
    public class ProgramData:IData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Course { get; set; }

        public string Description { get; set; }


        public string Type => GetType().ToString();


        public string Data { get; }


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