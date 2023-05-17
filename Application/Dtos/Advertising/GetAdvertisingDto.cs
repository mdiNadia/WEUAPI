using Application.Dtos.Common;
using Domain.Enums;

namespace Application.Dtos.Advertising
{
    public record GetAdvertisingDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public bool IsReject { get; init; }
        public int AdFileType { get; init; }
        public int Like { get; init; }
        public int View { get; init; }

        public string Description { get; init; }
        public string Text { get; init; }
        public bool Displayed { get; init; }
        public DateTime CreationDate { get; init; }

        public DateTime? StartDate { get; init; }

        public DateTime? ExpireDate { get; init; }

        public bool IsActive { get; init; }
        public int AdvertiserId { get; init; }
        public AdStatus AdStatus { get; set; }
        public List<GetNameAndId> Categories { get; init; }
        public List<GetFileWithType> Files { get; init; }

    }

    public class GetFileWithType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Domain.Enums.FileType FileType { get; set; }
    }


}


