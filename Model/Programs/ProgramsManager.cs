using System;
using System.Collections.Generic;
using System.Linq;
using Model.Authentication;
using Model.Data;
using Model.Support;
using Model.Tests;
using Model.Users;

namespace Model.Programs
{
    public class ProgramsManager : Singletone<ProgramsManager> 
    {
        public List<ProgramData> _programs { get; set; }

        public Dictionary<int, List<TestAssigningData>> _testAssigningToPrograms { get; set; }

        public Dictionary<int, UserAssigningData> _userAssigning { get; set; }

        public ProgramsManager()
        {
            _programs = new List<ProgramData>();
            _testAssigningToPrograms = new Dictionary<int, List<TestAssigningData>>();
            _userAssigning = new Dictionary<int, UserAssigningData>();

            
        }

        public static void CreateProgram(ProgramData data)
            => Instance._programs.Add(data);

        public static ProgramData[] GetProgramsList()
            => Instance._programs.ToArray();

        public static void AssignCandidateToProgram(UserAssigningData data)
        {
            if (!UsersManager.IsUserExists<CandidateUser>(data.CandidateId))
                throw new InterviewException.CandidateDoesntExistsException(data.CandidateId);
            if (Instance._userAssigning.ContainsKey(data.CandidateId))
            {
                Instance._userAssigning[data.CandidateId] = data;
            }
            else
            {
                Instance._userAssigning.Add(data.CandidateId,data);
            }
        }

        public static ProgramData GetCandidateProgram(int candidateId)
        {
            if (!Instance._userAssigning.ContainsKey(candidateId)) return null;

            var programId = Instance._userAssigning[candidateId].ProgramId;

            return Instance._programs.SingleOrDefault(n => n.Id == programId);

        }

        public static TestData[] GetCandidateTests(int candidateId) 
            => GetTests(GetCandidateProgram(candidateId).Id);


        public static TestData[] GetTests(int programId)
            => TestsManager.GetTests(Instance._testAssigningToPrograms[programId].Select(n => n.TestId).ToArray());


        public static void AssignTestToProgram(TestAssigningData data)
        {

            if (!TestsManager.IsTestExist(data.TestId))
                throw new Exception("Test doesn't exists!");

            if (!Instance._testAssigningToPrograms.ContainsKey(data.ProgramId))
                Instance._testAssigningToPrograms.Add(data.ProgramId, new List<TestAssigningData>());

            if (!Instance._testAssigningToPrograms[data.ProgramId].Contains(data))
                Instance._testAssigningToPrograms[data.ProgramId].Add(data);
            
        }

        public static void DeleteTestFromProgram(TestAssigningData data)
        {
            if (!TestsManager.IsTestExist(data.TestId))
                throw new Exception("Test doesn't exists!");


            if (Instance._testAssigningToPrograms.ContainsKey(data.ProgramId))
                Instance._testAssigningToPrograms[data.ProgramId].Remove(data);

        }

    }

    public class TestAssigningData:IData
    {
        public int TestId { get; set; }

        public int ProgramId { get; set; }

        public override bool Equals(object obj)
        {
            var a = obj as TestAssigningData;
            return a.ProgramId == ProgramId && a.TestId == TestId;
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


    public class UserAssigningData : IData
    {
        public int CandidateId { get; set; }

        public int ProgramId { get; set; }

        public override bool Equals(object obj)
        {
            var a = obj as UserAssigningData;
            return a.ProgramId == ProgramId && a.CandidateId == CandidateId;
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