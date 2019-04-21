using System;

namespace Model.Data
{
    public class InterviewInfoData : IData
    {

        public int CandidateID { get; set; }

        public int InterviewerID { get; set; } //TODO: check type

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

        public override bool Equals(object obj)
        {
            var a = obj as InterviewInfoData;
            if (a == null) return false;
            return a.CandidateID == CandidateID && a.InterviewerID == InterviewerID && a.Time.Equals(Time);
        }
    }
}
