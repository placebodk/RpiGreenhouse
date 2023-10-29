using System.Device.I2c;
using Iot.Device.Bh1750fvi;
using RpiGreenhouse.Core.Common;

namespace RpiGreenhouse.Core.Sensors
{
    public class SensorBh1750 : I2cDeviceBase
    {
        private Bh1750fvi _sensor;

        /// <summary>
        /// The illuminance
        /// </summary>
        public UnitsNet.Illuminance Illuminance { get; private set; }

        /// <inheritdoc />
        protected SensorBh1750(I2cDevice device) : base(device)
        {
            _sensor = new Bh1750fvi(device);
            var firstReading = _sensor.Illuminance;
        }

        public void Read()
        {
            Illuminance = _sensor.Illuminance;
        }
    }
}
