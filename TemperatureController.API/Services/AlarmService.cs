namespace TemperatureController.API.Services
{
    using System.Reactive.Linq;

    public class AlarmService
    {
        private int maxInternal = 30;
        private int maxExternal = 25;

        public AlarmService(ITemperatureMonitor temperatureMonitor)
        {
            temperatureMonitor.ExternalTemperature.Where(x => x > maxExternal)
                .CombineLatest(temperatureMonitor.InternalTemperature.Where(x => x > maxInternal))
                .Subscribe((values) =>
                {
                    Console.WriteLine($"Alarm on external {values.First} internal {values.Second}");
                });
        }
    }
}
