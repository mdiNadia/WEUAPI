namespace Application.Features.Role.Queries
{
    public class GetRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
