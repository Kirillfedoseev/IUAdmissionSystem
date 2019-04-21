namespace Model.Data
{
    public  class FileData
    {
        public string Type { get; set; }

        public string FileName { get; set; } 

    }

    public struct FileDataWrapper
    {
        public FileData Data { get; set; }
        public string Bytes { get; set; }

        public FileDataWrapper(FileData data, string bytes) : this()
        {
            Data = data;
            Bytes = bytes;
        }

    }
}
