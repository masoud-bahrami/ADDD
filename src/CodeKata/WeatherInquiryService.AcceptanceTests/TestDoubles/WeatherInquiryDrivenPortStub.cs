using System;
using WeatherInquiryService.Hexagon;
using WeatherInquiryService.Hexagon.Exceptions;

namespace WeatherInquiryService.AcceptanceTests.TestDoubles
{
    public class WeatherInquiryDrivenPortStub : IWeatherInquiryDrivenPort
    {
        public double _temperature;
        public bool _shouldRaiseExceptrion;
        public WeatherInquiryDrivenPortStub(bool shouldRaiseExceptrion)
        {
            _shouldRaiseExceptrion = shouldRaiseExceptrion;
        }
        public WeatherInquiryDrivenPortStub(double temperature)
        {
            _temperature = temperature;
        }
        public double Inquiry()
        {
            if (_shouldRaiseExceptrion)
                throw new WeatherInquiryPortNullException();
            return _temperature;
        }


        internal static IWeatherInquiryDrivenPort WithReturnException()
        {
            return new WeatherInquiryDrivenPortStub(true);
        }
        internal static IWeatherInquiryDrivenPort WithReturn(double fahrenheit)
        {
            return new WeatherInquiryDrivenPortStub(fahrenheit);
        }
    }
}
