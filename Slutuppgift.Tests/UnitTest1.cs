using Slutuppgift.Utils;

namespace Slutuppgift.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("1234567891234", 13, 13, true)]
        [InlineData("1234567891234", 14, 13, true)]
        [InlineData("1234567891234", 14, 12, true)]
        public void ValidationUtils_StringLength(string input, int max, int min, bool outfall)
        {
            //Apply

            //Act
            bool internOutfall = ValidationUtils.StringLength(input, max, min);

            //Assert
            Assert.Equal(outfall, internOutfall);
        }
    }
}