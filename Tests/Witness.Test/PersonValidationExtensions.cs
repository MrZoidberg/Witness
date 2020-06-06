namespace Witness.Test
{
    using System;
    using System.Diagnostics.Contracts;
    using Witness;

    public static class PersonValidationExtensions
    {
        [Pure]
        public static Func<IValidationContext<Person, string>> FirstName(this Func<IValidationContext<Person, Person>> context)
        {
            return () => { return context().Map(c => c.RootOUV.FirstName, "FirstName"); };
        }

        [Pure]
        public static Func<IValidationContext<Person, string>> LastName(this Func<IValidationContext<Person, Person>> context)
        {
            return () => { return context().Map(c => c.RootOUV.LastName, "LastName"); };
        }
    }
}