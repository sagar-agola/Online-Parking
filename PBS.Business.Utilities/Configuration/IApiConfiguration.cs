namespace PBS.Business.Utilities.Configuration
{
    public interface IApiConfiguration
    {
        string Token { get; }
        string Issuer { get; }
        string Audience { get; }
    }
}