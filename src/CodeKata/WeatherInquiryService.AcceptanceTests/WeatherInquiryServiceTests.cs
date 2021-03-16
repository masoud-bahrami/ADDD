using System;
using WeatherInquiryService.AcceptanceTests.TestDoubles;
using WeatherInquiryService.Hexagon.DriverPort;
using WeatherInquiryService.Hexagon.Exceptions;
using Xunit;

namespace WeatherInquiryService.Hexagon
{
    public class WeatherInquiryServiceTests
    {
        private IWeatherService _sut;

        [Fact]
        public void Inquiry_WeatherInquiryPortIsNull_ExceptionThrown()
        {
            IWeatherInquiryDrivenPort Null_Port = null;
            //Hexagon 
            Assert.Throws<WeatherInquiryPortNullException>(() => new WeatherService(Null_Port));
        }

        [Fact]
        public void Inquiry_WeatherInquiryPortIsUnavailable_ExceptionThrown()
        {
            //Driven Port
            IWeatherInquiryDrivenPort port = WeatherInquiryDrivenPortStub.WithReturnException();
            //Hexagon 
            _sut = new WeatherService(port);
            Assert.Throws<WeatherInquiryPortNullException>(() => _sut.Inuiry());
        }

        [Theory]
        [InlineData(75.2, 24)]
        [InlineData(96.8, 36)]
        [InlineData(113, 45)]
        public void Inquiry_WeatherInquiryPortReturnTemperatureInFarhenheit_ConvertedToCelcius(double temperatur, double expextedResult)
        {
            //Driven Port
            IWeatherInquiryDrivenPort port = WeatherInquiryDrivenPortStub.WithReturn(fahrenheit: temperatur);
            //Hexagon 
            _sut = new WeatherService(port);
            var result = _sut.Inuiry();
            Assert.Equal(expextedResult, result);
        }

        [Fact]
        public void Inquiry_ConsoleApplication_ShowResultIntheConsoleApp()
        {
            //Driven Port
            IWeatherInquiryDrivenPort port = WeatherInquiryDrivenPortStub.WithReturn(fahrenheit: 75.2);
            //Hexagon 
            var weatherService= new WeatherService(port);

            IConsoleWriter consoleWriter = ConsoelWriterMock.WhichExpect(expectedMessage:24);
            //Driver adapter
            IConsoleApp sut = new ConsoleAdapter(weatherService, consoleWriter);
            sut.Run();

            ((ConsoelWriterMock)consoleWriter).Verify();
        }
    }
}
