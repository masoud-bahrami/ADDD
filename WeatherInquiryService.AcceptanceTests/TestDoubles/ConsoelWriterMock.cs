using WeatherInquiryService.Hexagon;
using Xunit;

namespace WeatherInquiryService.AcceptanceTests.TestDoubles
{
    public class ConsoelWriterMock : IConsoleWriter
    {
        private double _actualMessage;
        protected double _expectedResult;

        internal static IConsoleWriter WhichExpect(int expectedMessage)
        {
            return new ConsoelWriterMock { _expectedResult = expectedMessage };
        }

        public void Write(double message)
        {
            _actualMessage = message;
        }

        internal void Verify()
        {
            Assert.Equal(_expectedResult, _actualMessage);
        }
    }
}
