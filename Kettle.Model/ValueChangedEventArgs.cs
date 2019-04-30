using System;

namespace BDL.Kettle.Model
{
    /// <summary>
    /// EventArgs that contains a typed value
    /// </summary>
    /// <typeparam name="T">Type of the value</typeparam>
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public T Value { get; }

        public ValueChangedEventArgs(T value)
        {
            Value = value;
        }        
    }    
}
