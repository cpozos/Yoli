using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Yoli.WebApi.Validations;

namespace Yoli.WebApi.Tests
{
    public class YoliValidatorFactoryTest
    {
        private readonly YoliSignInRequestValidator _sut;

        public YoliValidatorFactoryTest()
        {
            _sut = new YoliSignInRequestValidator();
        }
        
        [Fact]
        public async Task Validation_ShouldSucced()
        {
            // Act
            var result = await _sut.ValidateAsync(new Requests.YoliSignInRequest { });

            // Assert
            result.IsValid.Should().BeTrue(string.Join(" and ", result.Errors.Select(f => f.ErrorMessage.Replace(".",""))));
        }
    }
}