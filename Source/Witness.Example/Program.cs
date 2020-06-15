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

            Console.WriteLine("Validation errors option A:");
            foreach (string err in Validate(person))
            {
                Console.WriteLine(err);
            }
            
            Console.WriteLine("Validation errors option B:");
            foreach (string err in ValidateSame(person))
            {
                Console.WriteLine(err);
            }

            Console.ReadKey();
        }

        private static string[] Validate(Person person)
        {
            var result = person
                .SetupValidation()
                .FirstName().ShouldNotBeEmptyOrNull()
                .And()
                .LastName().ShouldNotBeEmptyOrNull()
                .And()
                .Age().ShouldBeInRange(18, 100)
                .And()
                .GithubAccount().ShouldExistInGithub()
                .ExecuteValidation();

            return result.ValidationErrors;
        }
        
        private static string[] ValidateSame(Person person)
        {
            var result = person
                .SetupValidation()
                .RuleFor(c => c.FirstName).ShouldNotBeEmptyOrNull()
                .And()
                .RuleFor(c => c.LastName).ShouldNotBeEmptyOrNull()
                .And()
                .RuleFor(c => c.Age).ShouldBeInRange(18, 100)
                .And()
                .RuleFor(c => c.GithubAccount).ShouldExistInGithub()
                .ExecuteValidation();

            return result.ValidationErrors;
        }
    }
}
