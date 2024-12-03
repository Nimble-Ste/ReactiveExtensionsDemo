namespace TemperatureController.API.Services
{
    using System.Reactive.Linq;

    public class FanService
    {
        private int maxTemp = 20;
        public FanService(ITemperatureMonitor temperatureMonitor)
        {
            temperatureMonitor.ExternalTemperature.Where(temp=>temp> maxTemp).Subscribe(temp =>
            {
                Console.WriteLine($"Fan on Current temp {temp}");
            });
        }
    }
}
