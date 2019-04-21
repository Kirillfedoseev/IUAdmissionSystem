using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Data
{
    public class InterviewInfoData : IData
    {

        public int CandidateID; //TODO: check type
        public int InterviewerID; //TODO: check type
        public string Time;

        public string Type => throw new NotImplementedException();

        public string Data => throw new NotImplementedException();

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
