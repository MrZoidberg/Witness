namespace Witness.Test
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class RuleForTests
    {
        [Fact]
        public void RuleFor_ShouldReturnCorrectContext()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = "Joe",
            };

            // act
            var firstNameContext = person.Validate().Invoke().RuleFor(c => c.FirstName);

            // assert
            firstNameContext.RootOUV.Should().Be(person);
            firstNameContext.OUVName.Should().Be("FirstName");
            firstNameContext.OUV.Should().Be("Joe");
            firstNameContext.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void RuleFor_WithNullExpression_ProducesException()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = "Joe",
            };

            // act
            Action action = () => _ = person.Validate().Invoke().RuleFor<Person, Person, string>(null);

            // assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}