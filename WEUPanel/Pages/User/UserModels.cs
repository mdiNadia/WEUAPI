namespace WEUPanel.Pages.User
{
    public class UserModels
    {
        public class User
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public int ProfileId { get; init; }
            public string ProfileUsername { get; init; }
            public DateTime CreationDate { get; set; }
        }
        public class CreateUser
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Role { get; set; }
        }
        public class EditUser
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Password { get; set; }

            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Role { get; set; }
        }

    }
}
