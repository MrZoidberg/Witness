namespace Witness.DependencyInjection.Example
{
    internal interface IGithubService
    {
        bool IsUserExists(string userName);
    }
}