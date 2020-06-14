namespace Witness.DependencyInjection.Example
{
    using System;
    using System.Diagnostics.Contracts;

    public static class PersonValidationContextExtensions
    {
        [Pure]
        public static Func<IValidationContext<Person, string>> FirstName(this Func<IValidationContext<Person, Person>> context)
        {
            return () => { return context().RuleFor(c => c.FirstName); };
        }

        [Pure]
        public static Func<IValidationContext<Person, string>> LastName(this Func<IValidationContext<Person, Person>> context)
        {
            return () => { return context().RuleFor(c => c.LastName); };
        }
        
        [Pure]
        public static Func<IValidationContext<Person, string>> GithubAccount(this Func<IValidationContext<Person, Person>> context)
        {
            return () => { return context().RuleFor(c => c.GithubAccount); };
        }
        
        [Pure]
        public static Func<IValidationContext<Person, uint>> Age(this Func<IValidationContext<Person, Person>> context)
        {
            return () => { return context().RuleFor(c => c.Age); };
        }
    }
}