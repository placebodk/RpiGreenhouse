using System.Device.I2c;
using Iot.Device.Bmp180;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using RpiGreenhouse.Core.Common;

namespace RpiGreenhouse.Core.Sensors;

public class SensorBme280 : I2cDeviceBase
{
    private Bme280 _bme280;

    /// <summary>
    /// Gets the temperature.
    /// </summary>
    public UnitsNet.Temperature Temperature { get; private set; }

    public UnitsNet.Pressure Pressure { get; private set; }

    public UnitsNet.RelativeHumidity Humidity { get; private set; }

    public UnitsNet.Length Altitude { get;private set; }

    /// <inheritdoc />
    public SensorBme280(I2cDevice device) : base(device)
    {
        _bme280 = new Bme280(device);
        _bme280.SetPowerMode(Bmx280PowerMode.Forced);
    }

    public void ReadStatus()
    {
        _bme280.SetPowerMode(Bmx280PowerMode.Forced);
        _bme280.TryReadTemperature(out var tempValue);
        _bme280.TryReadPressure(out var preValue);
        _bme280.TryReadHumidity(out var humValue);
        _bme280.TryReadAltitude(out var altValue);

        Temperature = tempValue;
        Pressure = preValue;
        Humidity = humValue;
        Altitude = altValue;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"Bme280: Temperature={Temperature}, Pressure={Pressure}, Humidity={Humidity}, Altitude={Altitude}";
    }
}
