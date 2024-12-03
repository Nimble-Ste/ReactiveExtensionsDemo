namespace TemperatureController.Tests
{
    using System;
    using System.Reactive.Subjects;
    using System.Text;
    using API.Services;
    using FluentAssertions;
    using NSubstitute;

    [TestFixture]
    public class FanServiceTests
    {
        private FanService Fixture;
        private ITemperatureMonitor TemperatureMonitor;

        private static readonly BehaviorSubject<int> externalTemp = new(0);

        private StringBuilder ConsoleOutput = new();

        [SetUp]
        public void Setup()
        {
            Console.SetOut(new StringWriter(ConsoleOutput));
            ConsoleOutput.Clear();

            TemperatureMonitor = Substitute.For<ITemperatureMonitor>();
            TemperatureMonitor.ExternalTemperature.Returns(externalTemp);
            Fixture = new FanService(TemperatureMonitor);
        }

        [Test]
        public void TestFanComesOn()
        {
            externalTemp.OnNext(100);

            ConsoleOutput.ToString().Trim().Should().BeEquivalentTo($"Fan on Current temp {100}");
        }
    }
}
