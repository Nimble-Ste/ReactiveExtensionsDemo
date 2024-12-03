namespace Sensors
{
    public class TemperatureSensor
    {
        public int CurrentTemp { get; private set; }

        public TemperatureSensor(int startingTemp)
        {
            CurrentTemp = startingTemp;

            Task.Run(async () =>
            {
                while (true)
                {
                    CurrentTemp += 1;
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            });
        }
    }
}