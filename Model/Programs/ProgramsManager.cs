using System;
using System.Collections.Generic;
using Model.Data;
using Model.Support;
using Model.Tests;

namespace Model.Programs
{
    public class ProgramsManager : Singletone<ProgramsManager> 
    {
        private List<ProgramData> _programs { get; set; }

        public Dictionary<int, List<TestAssigningData>> _testAssigningToPrograms { get; set; }

        public ProgramsManager()
        {
            _programs = new List<ProgramData>();
            _testAssigningToPrograms = new Dictionary<int, List<TestAssigningData>>();
        }

        public static void CreateProgram(ProgramData data)
            => Instance._programs.Add(data);


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

}