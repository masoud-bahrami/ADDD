using System;
using WeatherInquiryService.Hexagon.DriverPort;
using WeatherInquiryService.Hexagon.Exceptions;

namespace WeatherInquiryService.Hexagon
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherInquiryDrivenPort _port;

        public WeatherService(IWeatherInquiryDrivenPort port)
        {
            //Fail fast
            GuardAgainstNullPort(port);
            _port = port;
        }

       

        public double Inuiry()
        {
            var result = _port.Inquiry();
            return ConvertFahrenheitToCelcius(result);

        }

        private double ConvertFahrenheitToCelcius(double fahrenheit) => (fahrenheit - 32) * 5 / 9;

        private void GuardAgainstNullPort(IWeatherInquiryDrivenPort port)
        {
            if (port == null)
                throw new WeatherInquiryPortNullException();
        }
    }
}