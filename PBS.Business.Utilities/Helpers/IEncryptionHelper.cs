namespace PBS.Business.Utilities.Helpers
{
    public interface IEncryptionHelper
    {
        string Decrypt (string inputString, int key);
        string Encrypt (string inputString, int key);
    }
}