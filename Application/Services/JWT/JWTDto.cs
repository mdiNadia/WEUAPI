namespace Application.Services.JWT
{
    public class JWTDto
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
        public int DurationInYear { get; set; }
    }
}
