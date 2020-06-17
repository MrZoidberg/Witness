namespace Witness.DependencyInjection.Example
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    class Program
    {
        private static void Main()
        {
            Person person = new Person()
            {
                FirstName = "Joe",
                LastName = string.Empty,
                Age = 5,
                GithubAccount = "test",
            };
            
            var serviceProviderCollection = new ServiceCollection();
            serviceProviderCollection.AddTransient<IGithubService, GithubService>();
            IServiceProvider serviceProvider = serviceProviderCollection.BuildServiceProvider();

            Console.WriteLine("Validation errors:");
            foreach (string err in Validate(person, serviceProvider))
            {
                Console.WriteLine(err);
            }
            
            Console.ReadKey();
        }

        private static string[] Validate(Person person, IServiceProvider serviceProvider)
        {
            var result = person
                .Validate()
                .WithServiceProvider(serviceProvider)
                .FirstName().ShouldNotBeEmptyOrNull()
                .And()
                .LastName().ShouldNotBeEmptyOrNull()
                .And()
                .Age().ShouldBeInRange(18, 100)
                .And()
                .GithubAccount().ShouldExistInGithub()
                .Execute();

            return result.ValidationErrors;
        }
    }
}