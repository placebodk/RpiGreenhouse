using System.Device.I2c;

namespace RpiGreenhouse.Core.Common;

/// <summary>
/// 
/// </summary>
/// <seealso cref="System.IDisposable" />
public abstract class I2cDeviceBase : IDisposable
{
    protected I2cDevice Device;
    private bool _disposed;

    protected I2cDeviceBase(I2cDevice device)
    {
        Device = device;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed ) return;
        Device.Dispose();
        _disposed = true;
    }
}