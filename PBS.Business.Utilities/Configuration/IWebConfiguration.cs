namespace PBS.Business.Utilities.Configuration
{
    public interface IWebConfiguration
    {
        string SenderName { get; }
        string Email { get; }
        string Password { get; }
    }
}