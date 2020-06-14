namespace Witness.Example
{
    using System;
    using System.Diagnostics.Contracts;

    public static class PersonValidationContextExtensions
    {
        [Pure]
        public static Func<IValidationContext<Person, string>> FirstName(this Func<IValidationContext<Person, Person>> context)
        {
            return () => { return context().Map(c => c.RootOUV.FirstName); };
        }

        [Pure]
        public static Func<IValidationContext<Person, string>> LastName(this Func<IValidationContext<Person, Person>> context)
        {
            return () => { return context().Map(c => c.RootOUV.LastName); };
        }
        
        [Pure]
        public static Func<IValidationContext<Person, string>> GithubAccount(this Func<IValidationContext<Person, Person>> context)
        {
            return () => { return context().Map(c => c.RootOUV.GithubAccount); };
        }
        
        [Pure]
        public static Func<IValidationContext<Person, uint>> Age(this Func<IValidationContext<Person, Person>> context)
        {
            return () => { return context().Map(c => c.RootOUV.Age); };
        }
    }
}