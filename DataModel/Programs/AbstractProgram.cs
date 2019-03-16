﻿using DataModel.Data;

namespace DataModel.Programs
{
    public abstract class AbstractProgram:IData
    {
        public string Type => GetType().ToString();
        
        public abstract string Data { get; }
        

        public string Name;
        
        public CourseEnum Course { get; protected set; }
        
        public string Description { get; protected set; }
        
        //todo add other properties
        
        public string SerializeToJSON()
        {
            throw new System.NotImplementedException();
        }

        public IData DeserializeFromJSON()
        {
            throw new System.NotImplementedException();
        }
        
        
    }

    public enum CourseEnum
    {
        Bachelor,
        Master
    }
    
}