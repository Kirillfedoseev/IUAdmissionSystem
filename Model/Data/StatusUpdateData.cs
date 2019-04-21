using Model.Users;

namespace Model.Data
{
    public class StatusUpdateData : IData
    {
        public string Type { get; }
        public string Data { get; }

        public int CandidateId { get; set; }

        public CandidateUser.AdmissionStatus Status { get; set; }



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