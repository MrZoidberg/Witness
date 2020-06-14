namespace Witness.Test
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class ValidationContextExtensionsTests
    {
        [Fact]
        public void Map_ShouldReturnCorrectContext()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = "Joe",
            };

            // act
            var firstNameContext = person.SetupValidation().Invoke().RuleFor(c => c.FirstName);

            // assert
            firstNameContext.RootOUV.Should().Be(person);
            firstNameContext.OUVName.Should().Be("FirstName");
            firstNameContext.OUV.Should().Be("Joe");
            firstNameContext.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void Map_WithNullExpression_ProducesException()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = "Joe",
            };

            RootValidationContext<Person> context = new RootValidationContext<Person>(person);

            // act
            Action action = () => _ = person.SetupValidation().Invoke().RuleFor<Person, Person, string>(null);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}