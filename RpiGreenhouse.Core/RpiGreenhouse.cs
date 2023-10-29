using System.CodeDom;
using System.Device.Gpio;
using Iot.Device.Bmxx80;
using System.Device.I2c;
using System.Device.Spi;
using Iot.Device.Board;
using Microsoft.Extensions.Logging;
using RpiGreenhouse.Core.Converters;
using RpiGreenhouse.Core.Displays;
using RpiGreenhouse.Core.Common;
using RpiGreenhouse.Core.Sensors;

namespace RpiGreenhouse.Core;

public sealed class RpiGreenhouse : IDisposable
{
    private readonly GpioController _controller;
    private ILoggerFactory _loggerFactory;
    private ILogger _logger;
    private bool _disposed;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RpiGreenhouse" /> class.
    /// </summary>
    public RpiGreenhouse( ILoggerFactory loggerFactory )
    {
        _controller = new GpioController(PinNumberingScheme.Logical);
        _loggerFactory = loggerFactory;
        _logger = _loggerFactory.CreateLogger<RpiGreenhouse>();
    }

    public T Create<T>() where T : ConverterAdc
    {
#if false
        var type = typeof(T);

        I2cDevice i2cDevice = I2cDevice.Create(new I2cConnectionSettings(1, address));

        SpiDevice spiDevice = SpiDevice.Create( new SpiConnectionSettings(1));
        var fisk = new Iot.Device.Adc.Mcp3004(spiDevice);

        if ( type ==  typeof(Mcp3004))
        {
            return new Mcp3004(i2cDevice) as T ?? throw new InvalidOperationException($"Mcp3004 is not: {type}");
        }
#endif
        throw new NotImplementedException();
    }

    public T CreateI2C<T>(byte address) where T : I2cDeviceBase
    {
        var type = typeof(T);

        int busId = 1;

        _logger.LogDebug($"Creating I2CDevice: busId={busId}, address=0x{address:X2}");
        I2cDevice i2cDevice = I2cDevice.Create(new I2cConnectionSettings(busId, address));

        if (type == typeof(SensorBme280))
        {
            return new SensorBme280(i2cDevice) as T ?? throw new InvalidOperationException($"SensorBme280 is not: {type}");
        }
        if (type == typeof(DisplayLcd2004))
        {
            return new DisplayLcd2004(_controller, i2cDevice, _logger) as T ?? throw new InvalidOperationException($"DisplayLcd2004 is not: {type}");
        }

        throw new NotImplementedException();
    }

    //public T CreateGpioDevice<T>() where T : GpioDevice
    //{
    //    throw new NotImplementedException();
    //}

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed ) return;
        _controller.Dispose();
        _disposed = true;
    }
}