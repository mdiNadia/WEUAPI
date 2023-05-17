namespace Application.Dtos.Attachment
{
    public record GetAttachmentDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        //اسم فیلم یا عکس که در دیتابیس ذخیره میشود
        public string FileName { get; init; }
        public Domain.Enums.FileType FileType { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
