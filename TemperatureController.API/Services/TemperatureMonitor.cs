namespace TemperatureController.API.Services
{
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using Sensors;

    public class TemperatureMonitor : ITemperatureMonitor
    {
        private readonly TemperatureSensor internalSensor;
        private readonly TemperatureSensor externalSensor;

        public BehaviorSubject<int> ExternalTemperature { get; } = new(0);
        public BehaviorSubject<int> InternalTemperature { get; } = new(0);

        public TemperatureMonitor([FromKeyedServices("InternalSensor")] TemperatureSensor internalSensor, [FromKeyedServices("ExternalSensor")] TemperatureSensor externalSensor)
        {
            this.internalSensor = internalSensor;
            this.externalSensor = externalSensor;

            int previousExternalTemp = externalSensor.CurrentTemp;
            int previousInternalTemp = internalSensor.CurrentTemp;

            Observable.Interval(TimeSpan.FromMilliseconds(100)).StartWith(0).Subscribe(_ =>
            {
                if (previousExternalTemp != this.externalSensor.CurrentTemp)
                {
                    ExternalTemperature.OnNext(this.externalSensor.CurrentTemp);
                }

                if (previousInternalTemp != this.internalSensor.CurrentTemp)
                {
                    InternalTemperature.OnNext(this.internalSensor.CurrentTemp);
                }
            });
        }

        public (int internalTemp, int externalTemp) GetTemperatures()
        {
            return (internalSensor.CurrentTemp, externalSensor.CurrentTemp);
        }
    }
}