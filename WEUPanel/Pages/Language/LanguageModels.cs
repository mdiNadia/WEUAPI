using Microsoft.AspNetCore.Http;

namespace WEUPanel.Pages.Language
{
    public class LanguageModels
    {
        public class Language
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ShortName { get; set; }
            public int Direction { get; set; }
            public int? IconId { get; set; }
            public string? IconName { get; set; }
            public DateTime CreationDate { get; set; }

        }
        public class CreateLanguage
        {
            public string Name { get; set; }
            public string ShortName { get; set; }
            public int Direction { get; set; }
            public IFormFile IconFile { get; set; }
        }
        
        public class EditLanguage
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ShortName { get; set; }
            public int Direction { get; set; }
            public IFormFile IconFile { get; set; }
            public int IconId { get; set; }
            public string? IconName { get; set; }
        }

    }
}
