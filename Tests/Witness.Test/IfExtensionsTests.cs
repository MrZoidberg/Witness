namespace Witness.Test
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class IfExtensionsTests
    {
        [Fact]
        public void If_WithValidInput_ShouldNotProduceErrors()
        {
            // arrange
            Person person = new Person()
            {
                AgeInt = 6,
                LastName = string.Empty,
            };

            // act
            var validationResult = person.Validate()
                .If(
                    c => c.AgeInt == 5,
                    c => c.RuleFor(x => x.LastName).ShouldNotBeEmptyOrNull())
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void If_WithInvalidInput_ShouldProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeInt = 5,
                LastName = string.Empty,
            };

            // act
            var validationResult = person.Validate()
                .If(
                    c => c.AgeInt == 5,
                    c => c.RuleFor(x => x.LastName).ShouldNotBeEmptyOrNull())
                .Execute();

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should()
                .BeEquivalentTo($"{nameof(person.LastName)} cannot be empty or null");
        }

        [Fact]
        public void If_WithNullCondition_ShouldThrowException()
        {
            // arrange
            Person person = new Person();

            // act
            Action action = () =>
            {
                _ = person.Validate()
                    .If(null, 
                        c => c.RuleFor(x => x.LastName).ShouldNotBeEmptyOrNull())
                      .Execute();
            };

            action.Should().Throw<ArgumentNullException>();
        }


        [Fact]
        public void If_WithNullPositiveBranch_ShouldThrowException()
        {
            // arrange
            Person person = new Person();

            // act
            Action action = () =>
            {
                _ = person.Validate().If<Person, Person, Person>(c => c.AgeInt == 6, null).Execute();
            };

            action.Should().Throw<ArgumentNullException>();
        }
    }
}