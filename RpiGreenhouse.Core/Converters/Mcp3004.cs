using System.Device.Spi;

namespace RpiGreenhouse.Core.Converters;

public class Mcp3004 : ConverterAdc
{
    /// <inheritdoc />
    public Mcp3004(SpiDevice device) : base(new Iot.Device.Adc.Mcp3004(device))
    {
    }
}