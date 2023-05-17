namespace Application.Dtos.User
{
    public record GetUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string UserName { get; init; }
        public string PhoneNumber { get; init; }

        public int ProfileId { get; init; }
        public string ProfileUsername { get; init; }
        public DateTime CreationDate { get; init; }

    }
}
