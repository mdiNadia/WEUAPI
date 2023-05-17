namespace WEUPanel.Wrappers
{
    public class GetFileWithType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FileType FileType { get; set; }
    }
    public enum FileType
    {
        Image = 0,
        Video = 1
    }

}
