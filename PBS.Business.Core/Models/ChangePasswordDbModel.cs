namespace PBS.Business.Core.Models
{
    public class ChangePasswordDbModel
    {
        public int Id { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
