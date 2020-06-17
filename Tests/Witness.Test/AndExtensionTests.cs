namespace Witness.Test
{
    using FluentAssertions;
    using Xunit;

    public class AndExtensionTests
    {
        [Fact]
        public void And_ShouldReturnCorrectContext()
        {
            // arrange
            Person person = new Person()
            {
                LastName = "Doe",
            };

            // act
            var firstNameContext = person.Validate()
                .RuleFor(c => c.FirstName)
                .And()
                .RuleFor(c => c.LastName)
                .Invoke();

            // assert
            firstNameContext.RootOUV.Should().Be(person);
            firstNameContext.OUVName.Should().Be("LastName");
            firstNameContext.OUV.Should().Be("Doe");
            firstNameContext.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void And_WithValidInput_ShouldNotProduceErrors()
        {
            // arrange
            Person person = new Person()
            {
                AgeInt = 5,
                LastName = "Doe",
            };

            // act
            var validationResult = person.Validate()
                .RuleFor(c => c.LastName)
                .ShouldNotBeEmptyOrNull()
                .And()
                .RuleFor(c => c.AgeInt)
                .ShouldBeEqual(5)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }
    }
}