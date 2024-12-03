namespace TemperatureController.API.Services
{
    using System.Reactive.Subjects;

    public interface ITemperatureMonitor
    {
        BehaviorSubject<int> ExternalTemperature { get; }

        BehaviorSubject<int> InternalTemperature { get; }
        (int internalTemp, int externalTemp) GetTemperatures();
    }
}