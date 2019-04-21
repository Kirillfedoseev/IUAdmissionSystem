using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Data
{
    public class CandidateStatusData : IData
    {

        public int CandidateID { get; set; }
        public string CandidateStatus { get; set; }
        public string Type { get; }

        public string Data { get; }

        public IData DeserializeFromJSON(string json)
        {
            throw new NotImplementedException();
        }

        public string SerializeToJSON()
        {
            throw new NotImplementedException();
        }
    }
}
