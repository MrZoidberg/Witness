namespace Witness.DependencyInjection.Example
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class PersonValidationRulesExtensions
    {
        [Pure]
        public static Func<IValidationContext<Person, string>> ShouldExistInGithub(this Func<IValidationContext<Person, string>> context)
        {
            return () =>
            {
                var c = context();

                bool isValid = !string.IsNullOrEmpty(c.OUV);
                if (isValid)
                {
                    var githubService = c.Resolve<IGithubService>();
                    isValid = githubService.IsUserExists(c.OUV);
                }

                if (!isValid)
                {
                    c.ValidationErrors.Add($"{c.OUVName} should be a valid Github username");
                }

                return c;
            };
        }
    }
}