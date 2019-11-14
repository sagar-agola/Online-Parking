namespace PBS.Web.Helpers
{
    public interface ITokenDecoder
    {
        string RowToken { get; }
        bool IsLoggedIn { get; }
        int UserId { get; }
        string UserName { get; }
        string UserRole { get; }
        bool IsEmailConfirmed { get; }
    }
}