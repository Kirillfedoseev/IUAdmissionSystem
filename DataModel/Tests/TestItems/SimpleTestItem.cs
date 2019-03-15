using DataModel.Data;

namespace DataModel.Tests.TestItems
{
    public class SimpleTestItem:IData, ITestableItem
    {
        public string Type { get; }
        
        public string Data { get; }

        public double Grade => _rightAnswer == Answer ? MaxGrade : 0;
        
        public double MaxGrade { get; }

        public string TaskDesription { get; set; }

        public double Answer { get; set; }

        private double _rightAnswer;

        
        public SimpleTestItem(double maxGrade, string taskDescription , double rightAnswer )
        {
            MaxGrade = maxGrade;
            TaskDesription = taskDescription;
        }
        
        
        public string SerializeToJSON()
        {
            throw new System.NotImplementedException();
        }

        public IData DeserializeFromJSON()
        {
            throw new System.NotImplementedException();
        }

        
    }
}