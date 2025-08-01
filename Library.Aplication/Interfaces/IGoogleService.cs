namespace Library.Aplication.Interfaces
{
    public interface IGoogleService
    {
        Task<string> GetIdToken(string code, CancellationToken ct = default);

        string GetRedirectLink();
    }
}
