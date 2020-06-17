namespace Witness.Test
{
    using FluentAssertions;
    using Xunit;

    public class StringExtensionsTests
    {
        [Fact]
        public void ShouldNotBeEmptyOrNull_WithValidInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = "Joe",
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.FirstName)
                .ShouldNotBeEmptyOrNull()
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldNotBeEmptyOrNull_WithEmptyInput_ProduceError()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = string.Empty,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.FirstName)
                .ShouldNotBeEmptyOrNull()
                .Execute();

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should()
                .BeEquivalentTo(new[] { $"{nameof(person.FirstName)} cannot be empty or null" });
        }

        [Fact]
        public void ShouldNotBeEmptyOrNull_WithNullInput_ProduceError()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = null,
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.FirstName)
                .ShouldNotBeEmptyOrNull()
                .Execute();

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should()
                .BeEquivalentTo(new[] { $"{nameof(person.FirstName)} cannot be empty or null" });
        }

        [Fact]
        public void ShouldHaveLengthWithin_WithMidInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = "Joe",
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.FirstName)
                .ShouldHaveLengthWithin(2, 4)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldHaveLengthWithin_WithMinInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = "Joe",
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.FirstName)
                .ShouldHaveLengthWithin(3, 4)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldHaveLengthWithin_WithMaxInput_DoesNotProduceError()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = "Joe",
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.FirstName)
                .ShouldHaveLengthWithin(1, 3)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().BeEmpty();
        }

        [Fact]
        public void ShouldHaveLengthWithin_WithLessInput_ProduceError()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = "Joe",
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.FirstName)
                .ShouldHaveLengthWithin(10, 20)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should().BeEquivalentTo(new[] { $"{nameof(person.FirstName)} length cannot be less than 10" });
        }

        [Fact]
        public void ShouldHaveLengthWithin_WithMoreInput_ProduceError()
        {
            // arrange
            Person person = new Person()
            {
                FirstName = "Joe",
            };

            // act
            var validationResult = person
                .Validate()
                .RuleFor(c => c.FirstName)
                .ShouldHaveLengthWithin(1, 2)
                .Execute();

            // assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should().BeEquivalentTo(new[] { $"{nameof(person.FirstName)} length cannot be bigger than 2" });
        }
    }
}