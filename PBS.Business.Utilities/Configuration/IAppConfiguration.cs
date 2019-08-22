namespace PBS.Business.Utilities.Configuration
{
    public interface IAppConfiguration
    {
        string Token { get; }
        string Issuer { get; }
        string Audience { get; }
    }
}