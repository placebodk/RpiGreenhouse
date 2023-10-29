using Iot.Device.Bmxx80;
using Microsoft.Extensions.Logging;
using RpiGreenhouse.Core.Displays;
using RpiGreenhouse.Core.Sensors;
#pragma warning disable CS8602

namespace RpiGreenhouse.Elverstien;

internal class Elverstien
{
    private SensorBme280 _sensorBme280;
    private readonly SensorBh1750 _sensorBh1750;
    private readonly DisplayLcd2004 _lcd;

    /// <summary>
    /// Initializes a new instance of the <see cref="Elverstien"/> class.
    /// </summary>
    public Elverstien( ILoggerFactory loggerFactory )
    {
        var rpiGreenhouse = new Core.RpiGreenhouse(loggerFactory);
        //var mcp3004 = rpiGreenhouse.Create<Mcp3004>(0x1);
        _sensorBme280 = rpiGreenhouse.CreateI2C<SensorBme280>(Bmx280Base.SecondaryI2cAddress);
        _sensorBh1750 = rpiGreenhouse.CreateI2C<SensorBh1750>(0x23);
        _lcd = rpiGreenhouse.CreateI2C<DisplayLcd2004>(0x27);
    }


    public void Run()
    {
        if (_sensorBh1750 == null ) throw new NullReferenceException( nameof(_sensorBh1750));

        int iCount = 0;

        while (true)
        {
            _lcd?.Clear();

            if (iCount++ % 2 == 1)
            {
                _sensorBh1750?.Read();
                _lcd.Write( "Light:", $"{_sensorBh1750.Illuminance}", 0);
            }
            else
            {
                _sensorBme280.ReadStatus();
                _lcd.Write("Temp:", $"{_sensorBme280.Temperature.DegreesCelsius:0.#} C", 0);
                _lcd.Write("Press:", $"{_sensorBme280.Pressure.Hectopascals:#.##} hPa", 1);
                _lcd.Write("Humidity:", $"{_sensorBme280.Humidity.Percent:#.##}%", 2);
                _lcd.Write("Altitude:", $"{_sensorBme280.Altitude.Meters:#} m", 3);
            }

            Thread.Sleep(5000);
        }
    }
}