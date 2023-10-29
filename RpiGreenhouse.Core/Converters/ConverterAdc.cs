using Iot.Device.Adc;

namespace RpiGreenhouse.Core.Converters;

public abstract class ConverterAdc 
{
    /// <inheritdoc />
    protected ConverterAdc(Mcp3xxx mcp3000 ) 
    {
    }
}