using WEUPanel.Wrappers;

namespace WEUPanel.Pages.ConfirmedResult
{
    public class ConfirmedResultModels
    {
        public class ConfirmedResult
        {
            public int Id { get; set; }
            public int AdId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Text { get; set; }
            public DateTime CreationDate { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? ExpireDate { get; set; }
            public string ConfirmedDate { get; set; }
            public string ShortKey { get; set; }
            public int AdvertiserId { get; set; }
            public string Parent { get; set; }
            public int Views { get; set; }
            public int Likes { get; set; }
            public List<GetFileWithType> Files { get; set; }
        }
        public class CreateConfirmedResult
        {
            public int AdId { get; set; }
            public List<GetFileWithType> AdFiles { get; set; }
        }
    }
}
