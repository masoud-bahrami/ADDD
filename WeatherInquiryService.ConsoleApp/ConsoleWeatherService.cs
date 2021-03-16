using WeatherInquiryService.Hexagon.DriverPort;

namespace WeatherInquiryService.Hexagon
{
    public class ConsoleAdapter : IConsoleApp
    {
        //Hexagon port
        private readonly IWeatherService _weatherInquiryDrivenPort;
        private IConsoleWriter _consoleWriter;
        
        public ConsoleAdapter(IWeatherService weatherInquiryDrivenPort,IConsoleWriter consoleWriter)
        {
            _weatherInquiryDrivenPort = weatherInquiryDrivenPort;
            _consoleWriter = consoleWriter;
        }
        public void Run()
        {
            //TODO
            var result = _weatherInquiryDrivenPort.Inuiry();
            _consoleWriter.Write(result);
        }
    }
}