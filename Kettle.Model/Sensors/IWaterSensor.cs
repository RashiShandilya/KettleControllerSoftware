namespace BDL.Kettle.Model.Sensors
{
    /// <summary>
    /// Sensor returning the presence of water.
    /// True when water is present, otherwise false.    
    /// </summary>
    public interface IWaterSensor : ISensor<bool>
    {
    }
}
