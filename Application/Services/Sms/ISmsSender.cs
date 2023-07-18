namespace Application.Services.Sms
{
    public interface ISmsSender
    {
        Task<bool> SendVertificateCode(string receptor, string sender, string text);
    }
}
