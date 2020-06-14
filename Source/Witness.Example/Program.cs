namespace Witness.Example
{
    using System;

    internal static class Program
    {
        private static void Main()
        {
            Person person = new Person()
            {
                FirstName = "Joe",
                LastName = string.Empty,
                Age = 5,
            };

            Console.WriteLine("Validation errors:");
            foreach (string err in Validate(person))
            {
                Console.WriteLine(err);
            }

            Console.ReadKey();
        }

        private static string[] Validate(Person person)
        {
            RootValidationContext<Person> validationContext = new RootValidationContext<Person>(person);
            var result = validationContext
                .SetupValidation()
                .FirstName()
                .ShouldNotBeEmptyOrNull()
                .And()
                .LastName()
                .ShouldNotBeEmptyOrNull()
                .And()
                .Age()
                .ShouldBeInValidRange(18, 100)
                .And()
                .GithubAccount()
                .ShouldExistInGithub()
                .ExecuteValidation();

            return result.ValidationErrors;
        }
    }
}
