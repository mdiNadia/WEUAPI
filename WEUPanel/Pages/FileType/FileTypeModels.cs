namespace WEUPanel.Pages.FileType
{
    public class FileTypeModels
    {
        public class FileType
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Size { get; set; }
            public int Type { get; set; }
            public string Extension { get; set; }
            public DateTime CreationDate { get; set; }
        }
        public class CreateFileType
        {
            public string Name { get; set; }
            public int Type { get; set; }
            public string Extension { get; set; }
            public long Size { get; set; }
        }
        public class EditFileType
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Type { get; set; }
            public string Extension { get; set; }
            public long Size { get; set; }
        }

    }
}
