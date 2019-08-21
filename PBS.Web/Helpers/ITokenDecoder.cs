namespace PBS.Web.Helpers
{
    public interface ITokenDecoder
    {
        bool IsLoggedIn { get; }

        int UserId { get; }

        string UserName { get; }

        string UserRole { get; }
    }
}