namespace UserAuthentication.Interfaces
{
    public interface IToken
    {
        string BuildToken(string key, string issuer, IEnumerable<string> audience, string userName, long userId);
    }
}
