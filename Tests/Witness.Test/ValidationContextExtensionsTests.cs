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

            RootValidationContext<Person> context = new RootValidationContext<Person>(person);

            // act
            var firstNameContext = context.SetupValidation().Invoke().Map(c => c.RootOUV.FirstName);

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
            Action action = () => context.SetupValidation().Invoke().Map<Person, Person, string>(null);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}