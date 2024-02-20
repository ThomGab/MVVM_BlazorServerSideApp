using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Xml.Linq;
using WPC_App_BlazorServerSide.Helpers;
using WPC_App_BlazorServerSide.Validators.GeoCoordinateValidator;

namespace UnitTests
{
    /// <summary>
    /// TODO And culture wrapper so tests don't fail locally.
    /// </summary>

    public class GlobalCoordinateValidator_Tests
    {
        private static ushort maxDecimalPlaces = 5;

        public static IEnumerable<object[]> ValidInputTestData =>
            new List<object[]>
            {
                new object[] { "90" },
                new object[] { "12" },
                new object[] { "1" },
                new object[] { "1.12345" },
                new object[] { "1.123" },
                new object[] { "0" },
                new object[] { " 90" },
                new object[] { "-90" },
            };

        public static IEnumerable<object[]> InvalidInputTestData =>
            new List<object[]>
            {
                new object[] { "Potateo" },
                new object[] { "£12" },
                new object[] { "e12" },
                new object[] { "1234" },
                new object[] { "-1234 " },
                new object[] { "12..1" },
                new object[] { "12,,1" },
                new object[] { "180.1" },
                new object[] { " " },
                new object[] { "1.123456789" },
            };

        [Fact]
        public void Ctor_ThrowsArgumentOutOfRangeException_WhenGeoCoordinateTypeIsUninitialized()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new GlobalCoordinateValidator(GeoCoordinateType.Unintialized, maxDecimalPlaces));
        }

        [Theory]
        [MemberData(nameof(InvalidInputTestData))]
        public void IsValid_ReturnsFalse_ForGivenLatitudeInput(string input)
        {
            // arrange
            var sut = new GlobalCoordinateValidator(GeoCoordinateType.Latitude, maxDecimalPlaces);

            // act
            var result = sut.IsValid(input);

            // assert
            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(InvalidInputTestData))]
        public void IsValid_ReturnsFalse_ForGivenLongitudeInput(string input)
        {
            // arrange
            var sut = new GlobalCoordinateValidator(GeoCoordinateType.Longitude, maxDecimalPlaces);

            // act
            var result = sut.IsValid(input);

            // assert
            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(ValidInputTestData))]
        public void IsValid_ReturnsTrue_WhenInputIsValidAndLatitudeType(string input)
        {
            // arrange
            var sut = new GlobalCoordinateValidator(GeoCoordinateType.Latitude, maxDecimalPlaces);

            // act
            var result = sut.IsValid(input);

            // assert
            Assert.Equal(true, result);
        }

        [Theory]
        [MemberData(nameof(ValidInputTestData))]
        public void IsValid_ReturnsTrue_WhenInputIsValidAndLongitudeType(string input)
        {
            // arrange
            var sut = new GlobalCoordinateValidator(GeoCoordinateType.Longitude, maxDecimalPlaces);

            // act
            var result = sut.IsValid(input);

            // assert
            Assert.Equal(true, result);
        }


        [Fact]
        public void IsValid_HandlesCurrentCultureDecimalPoint()
        {
            // arrange
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            var sut = new GlobalCoordinateValidator(GeoCoordinateType.Latitude, maxDecimalPlaces);

            // act
            var result = sut.IsValid("12,1");

            // assert
            Assert.True(result);
        }
    }
}