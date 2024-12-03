namespace TemperatureController.API
{
    using Sensors;
    using TemperatureController.API.Services;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddActivatedKeyedSingleton<TemperatureSensor>("ExternalSensor", (_, _) => new TemperatureSensor(0));
            builder.Services.AddActivatedKeyedSingleton<TemperatureSensor>("InternalSensor", (_, _) => new TemperatureSensor(10));

            builder.Services.AddActivatedSingleton<ITemperatureMonitor, TemperatureMonitor>();
            builder.Services.AddActivatedSingleton<FanService>();
            builder.Services.AddActivatedSingleton<AlarmService>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();


            app.MapControllers();

            app.Run();
        }
    }
}
