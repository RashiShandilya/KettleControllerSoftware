using System;

namespace BDL.Kettle.Model
{
    /// <summary>
    /// Sensor that raised an event when its value changed.
    /// </summary>
    /// <typeparam name="T">Type of the sensor value</typeparam>
    public interface ISensor<T>
    {
        /// <summary>
        /// Current sensor value
        /// </summary>
        T CurrentValue { get; }

        /// <summary>
        /// Event raised when the value of the sensor changed
        /// </summary>
        event EventHandler<ValueChangedEventArgs<T>> ValueChanged;
    }
}
