using System.Device.Gpio;
using System.Device.I2c;
using System.Formats.Tar;
using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;
using Microsoft.Extensions.Logging;
using RpiGreenhouse.Core.Common;

namespace RpiGreenhouse.Core.Displays;

public class DisplayLcd2004 : I2cDeviceBase
{
    private readonly ILogger _logger;

    private readonly Pcf8574 _driver;
    private readonly Lcd2004 _lcd;

    /// <inheritdoc />
    public DisplayLcd2004(GpioController controller, I2cDevice device, ILogger logger) : base(device)
    {
        _logger = logger;

        // LCD2004
        _logger.LogDebug("Creating Pcf8574...");
        _driver = new Pcf8574(Device, -1, controller);
        _logger.LogDebug("... Pcf8574 created!");

        _logger.LogDebug($"QueryComponentInformation={_driver.QueryComponentInformation()}");

        _lcd = new Lcd2004(0,
            2,
            new[] { 4, 5, 6, 7 },
            3,
            0.1f,
            1,
            new GpioController(PinNumberingScheme.Logical, _driver));
    }

    public void Clear()
    {
        _lcd.Clear();
    }

    public void Write( string text, int line)
    {
        _lcd.SetCursorPosition(0,line);
        _lcd.Write(text);
    }

    public void Write( string left, string right, int line)
    {
        var value = right.PadLeft(20 - left.Length);
        Write($"{left}{value}", line);
    }
}