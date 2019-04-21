using System;

namespace Model.Data
{
#pragma warning disable CS0659 // "InterviewInfoData" переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode().
    public class InterviewInfoData : IData
#pragma warning restore CS0659 // "InterviewInfoData" переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode().
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

        public override int GetHashCode()
        {
            return HashCode.Combine(CandidateID, InterviewerID, Time, Type, Data);
        }
    }
}
