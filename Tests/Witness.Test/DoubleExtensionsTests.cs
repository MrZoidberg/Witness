namespace Witness.Test
{
    using FluentAssertions;
    using Xunit;

    public class DoubleExtensionsTests
    {
        [Fact]
        public void ShouldBeInRangeDouble_WithMidInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeDouble = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeDouble)
                .ShouldBeInRange(4, 6)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldBeInRangeDouble_WithMinInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeDouble = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeDouble)
                .ShouldBeInRange(5, 6)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldBeInRangeDouble_WithMaxInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeDouble = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeDouble)
                .ShouldBeInRange(4, 5)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldBeInRangeDouble_WithLessInput_ProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeDouble = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeDouble)
                .ShouldBeInRange(10, 20)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should().BeEquivalentTo($"{nameof(person.AgeDouble)} cannot be less than 10");
        }

        [Fact]
        public void ShouldBeInRangeDouble_WithMoreInput_ProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeDouble = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeDouble)
                .ShouldBeInRange(1, 3)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should().BeEquivalentTo($"{nameof(person.AgeDouble)} cannot be bigger than 3");
        }
    }
}