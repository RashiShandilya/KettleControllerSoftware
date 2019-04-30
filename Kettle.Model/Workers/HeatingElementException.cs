using System;

namespace BDL.Kettle.Model.Workers
{
    /// <summary>
    /// Exception raised when the heating element is faulty and could not be switched on.
    /// </summary>
    [Serializable]
    public class HeatingElementException : Exception
    {
        public HeatingElementException() { }
        public HeatingElementException(string message) : base(message) { }
        public HeatingElementException(string message, Exception inner) : base(message, inner) { }
        protected HeatingElementException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
