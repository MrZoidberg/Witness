namespace Witness.DependencyInjection.Example
{
    internal class GithubService : IGithubService
    {
        public bool IsUserExists(string userName)
        {
            return false;
        }
    }
}