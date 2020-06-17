namespace Witness.Test
{
    using FluentAssertions;
    using Xunit;

    public class UIntExtensionsTests
    {
        [Fact]
        public void ShouldBeInRangeUInt_WithMidInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeUint = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeUint)
                .ShouldBeInRange(4, 6)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldBeInRangeUInt_WithMinInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeUint = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeUint)
                .ShouldBeInRange(5, 6)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldBeInRangeUInt_WithMaxInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeUint = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeUint)
                .ShouldBeInRange(4, 5)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldBeInRangeUInt_WithLessInput_ProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeUint = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeUint)
                .ShouldBeInRange(10, 20)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should().BeEquivalentTo($"{nameof(person.AgeUint)} cannot be less than 10");
        }

        [Fact]
        public void ShouldBeInRangeUInt_WithMoreInput_ProduceError()
        {
            // arrange
            Person person = new Person()
            {
                AgeUint = 5,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.AgeUint)
                .ShouldBeInRange(1, 3)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should().BeEquivalentTo($"{nameof(person.AgeUint)} cannot be bigger than 3");
        }
    }
}