using System;
using System.Threading.Tasks;

namespace BDL.Kettle.Model
{
    /// <summary>
    /// Switchable component that can be switch on and off, and raises the appropriate events when one of the state changes.
    /// </summary>
    public interface ISwitchable
    {
        /// <summary>
        /// Current state of the component
        /// </summary>
        bool IsOn { get; }
        
        /// <summary>
        /// Raised when the component has been switched on
        /// </summary>
        event EventHandler SwitchedOn;

        /// <summary>
        /// Raised when the component has been switched off
        /// </summary>
        event EventHandler SwitchedOff;

        /// <summary>
        /// Switches on the component and raises SwitchedOn event
        /// </summary>
        /// <returns></returns>
       void SwitchOnAsync();

        /// <summary>
        /// Switches off the component and raises SwitchedOff event
        /// </summary>
        /// <returns></returns>
        void SwitchOffAsync();
    }
}
