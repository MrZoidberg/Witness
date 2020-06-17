namespace Witness.Test
{
    using FluentAssertions;
    using Xunit;

    public class IntExtensionsTests
    {
        [Fact]
        public void ShouldBeInRangeInt_WithMidInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeInt = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeInt)
                .ShouldBeInRange(4, 6)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldBeInRangeInt_WithMinInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeInt = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeInt)
                .ShouldBeInRange(5, 6)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldBeInRangeInt_WithMaxInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeInt = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeInt)
                .ShouldBeInRange(4, 5)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldBeInRangeInt_WithLessInput_ProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeInt = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeInt)
                .ShouldBeInRange(10, 20)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should().BeEquivalentTo($"{nameof(person.AgeInt)} cannot be less than 10");
        }

        [Fact]
        public void ShouldBeInRangeInt_WithMoreInput_ProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeInt = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeInt)
                .ShouldBeInRange(1, 3)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should().BeEquivalentTo($"{nameof(person.AgeInt)} cannot be bigger than 3");
        }
    }
}