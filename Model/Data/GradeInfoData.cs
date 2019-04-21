namespace Model.Data
{
    public class GradeInfoData:IData
    {

        public int CandidateID; //TODO: Check Type

        public string Grade;

        public string Type { get; }

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