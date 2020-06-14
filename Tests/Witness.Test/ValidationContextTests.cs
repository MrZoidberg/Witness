namespace Witness.Test
{
    using FluentAssertions;
    using Xunit;

    public class ValidationContextTests
    {
        [Fact]
        public void RootValidationContext_HasValidConstructor()
        {
            Person person = new Person();
            RootValidationContext<Person> validationContext = new RootValidationContext<Person>(person);

            validationContext.RootOUV.Should().Be(person);
            validationContext.OUV.Should().Be(person);
            validationContext.OUVName.Should().Be(nameof(Person));
            validationContext.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ValidationContext_HasValidConstructor()
        {
            Person person = new Person();
            ValidationContext<Person, Person> validationContext = new ValidationContext<Person, Person>(person);

            validationContext.RootOUV.Should().Be(person);
            validationContext.OUV.Should().Be(person);
            validationContext.OUVName.Should().Be(nameof(Person));
            validationContext.ValidationErrors.Should().BeEmpty();
        }
    }
}