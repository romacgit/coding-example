using FluentAssertions;
using StringValidation.Library.Helper;

namespace StringValidation.Tests.Services
{

    [TestClass]
    public class StringValidationHelperTest
	{
		[TestMethod]
		public async Task ValidateUserInput_Valid_ReturnsTrue()
		{
			// Arrange
			var input = "Lorem ( ipsum ) dolor";

			// Act
			var result = await StringValidationHelper.IsValidInput(input);

			// Assert
			result.Should().BeTrue();
        }

		[TestMethod]
		public async Task ValidateUserInput_Invalid_ReturnsFalse()
		{
            // Arrange
            var input = "Lorem (ipsum dolor";

            // Act
            var result = await StringValidationHelper.IsValidInput(input);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public async Task ValidateUserInput_OnlyChars_ReturnsTrue()
        {
            // Arrange
            var input = "Lorem ipsum dolor";

            // Act
            var result = await StringValidationHelper.IsValidInput(input);

            // Assert
            result.Should().BeTrue();
        }

		[TestMethod]
		public async Task ValidateUserInput_EmptyString_ReturnsFalse()
		{
			// Arrange
			var input = string.Empty;

            // Act
            var result = await StringValidationHelper.IsValidInput(input);

            // Assert
            result.Should().BeFalse();
		}

		[TestMethod]
		public async Task ValidateUserInput_OnlyValidBrackets_ReturnsTrue()
		{
			// Arrange
			var input = "[({})]";

            // Act
            var result = await StringValidationHelper.IsValidInput(input);

            // Assert
            result.Should().BeTrue();
		}

        [TestMethod]
        public async Task ValidateUserInput_MissingClosingBracket_ReturnsFalse()
        {
            // Arrange
            var input = "[({}]";

            // Act
            var result = await StringValidationHelper.IsValidInput(input);

            // Assert
            result.Should().BeFalse();
        }

        public async Task ValidateUserInput_ReversedBrackets_ReturnsFalse()
        {
            // Arrange
            var input = ")(";

            // Act
            var result = await StringValidationHelper.IsValidInput(input);

            // Assert
            result.Should().BeFalse();
        }
    }
}

