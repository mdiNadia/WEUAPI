namespace WEUPanel.Pages.UserRole
{
    public class UserRoleModels
    {
        public class Role
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string NormalizeName { get; set; }
            public DateTime CreationDate { get; set; }
        }
        public class CreateRole
        {
            public string Name { get; set; }
        }
        public class EditRole
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
