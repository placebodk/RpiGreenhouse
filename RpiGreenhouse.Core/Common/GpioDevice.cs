using System.Device.Gpio;

namespace RpiGreenhouse.Core.Common;

/// <summary>
///     Base entity
/// </summary>
public abstract class GpioDevice 
{
    protected GpioController GpioController;

    protected GpioDevice(GpioController controller)
    {
        GpioController = controller;
    }
}