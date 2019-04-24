using System.Collections.Generic;
using System.Linq;
using Model.Authentication;
using Model.Data;
using Model.Support;
using Model.Users;

namespace Model.Tests
{
    public class TestsManager:Singletone<TestsManager>
    {
        public int LastId { get; set; }

        public List<TestData> Tests { get; set; }

        public Dictionary<int, List<TestResultsData>> TestsResults { get; set; }


        public TestsManager()
        {
            LastId = 0;
            Tests = new List<TestData>();
        }

        public static void CreateTest(TestData data)
        {
            data.SetIds(++Instance.LastId);
            Instance.Tests.Add(data);
        }

        public static void DeleteTest(int id)
            => Instance.Tests.RemoveAll(n => n.TestId == id);


        public static TestData GetTest(int id) 
            => Instance.Tests.SingleOrDefault(n=>n.TestId == id);


        public static TestData[] GetTests(int[] id)
            => Instance.Tests.Where(n => id.Contains(n.TestId)).ToArray();

        public static bool IsTestExist(int id)
            => Instance.Tests.SingleOrDefault(n => n.TestId == id) != null;


        public static TestResultsData[] GetTestsResult(int candidateId)
            => Instance.TestsResults.SingleOrDefault(n => n.Key == candidateId).Value.ToArray();


        public static void SubmitTestResult(int candidateId, TestResultsData data)
        {
            if (!UsersManager.IsUserExists<CandidateUser>(candidateId))
                throw new InterviewException.CandidateDoesntExistsException(candidateId);

            if (!Instance.TestsResults.ContainsKey(candidateId))
                Instance.TestsResults.Add(candidateId, new List<TestResultsData>());

            Instance.TestsResults[candidateId].Add(data);

        }
        
    }

    public class TestResultsData:IData
    {

        public int TestId { get; set; }

        public QuestionAnswerPair[] QuestionAnswerPairs { get; set; }

        public struct QuestionAnswerPair
        {
            public int QuestionId { get; set; }

            public int AnswerId { get; set; }

        }


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