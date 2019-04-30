using BDL.Kettle.Model;
using BDL.Kettle.Model.Inputs;
using BDL.Kettle.Model.Outputs;
using BDL.Kettle.Model.Workers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kettle.Controller
{
    public class Kettle : IPowerSwitch, IHeatingElement
    {
        event EventHandler PowerOnEvent;
        event EventHandler PowerOffEvent;

        object objectLock = new Object();

        public bool IsPowerOn;

       

        public Kettle()
        {
            IPowerSwitch p = (IPowerSwitch)this;
            p.SwitchedOn += new EventHandler(On_PowerOn_Press);
            p.SwitchedOff += new EventHandler(On_PowerOff_Press);

             var sensors = new TemperatureSensors();
             PrintTemperatures(sensors);
        }

        static void PrintTemperatures(ISensorCollection<ISensor<Task> sensors)
        {
            while (sensors.MoreSensors)
            {
                var sensor = sensors.NextSensor;
                Console.WriteLine($"Temperature is {sensor.CurrentValue} degree Celsius");
            }
        }


        void ISwitchable.SwitchOnAsync()
        {
            EventHandler handler = PowerOffEvent;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        void ISwitchable.SwitchOffAsync()
        {
            EventHandler handler = PowerOnEvent;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        bool ISwitchable.IsOn
        {
            get { return IsPowerOn; }
        }

        event EventHandler ISwitchable.SwitchedOff
        {
            add
            {
                lock (objectLock)
                {
                    PowerOffEvent += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    PowerOffEvent -= value;
                }
            }
        }

        event EventHandler ISwitchable.SwitchedOn
        {
            add
            {
                lock (objectLock)
                {
                    PowerOnEvent += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    PowerOnEvent -= value;
                }
            }
        }

        protected void On_PowerOn_Press(object sender, EventArgs e)
        {
            if (!((IPowerSwitch)sender).IsOn)
            {
                Console.WriteLine("Power Is ON");
                ((Kettle)sender).IsPowerOn = true;
                ((IPowerLamp)this).SwitchOnAsync();
                ((IHeatingElement)this).SwitchOnAsync();

               
            }
            else
            {
                Console.WriteLine("Already ON");

            }

        }

        protected void On_PowerOff_Press(object sender, EventArgs e)
        {
            if (((IPowerSwitch)sender).IsOn)
            {
                Console.WriteLine("Power Is OFF");
                ((Kettle)sender).IsPowerOn = false;
                ((IPowerLamp)this).SwitchOffAsync();
                ((IHeatingElement)this).SwitchOffAsync();
            }
            else
            {
                Console.WriteLine("Already OFF");
            } 

        }

    }

    class FakeTemperatureSensor : ISensor<Task>
    {
        private static Random rnd = new Random();

        public int CurrentValue => rnd.Next(0, 20);
    }

    interface ISensorCollection<T>
    {
        bool MoreSensors { get; }
        T NextSensor { get; }
    }

    class TemperatureSensors : ISensorCollection<FakeTemperatureSensor>
    {
        private readonly List<FakeTemperatureSensor> sensors =
            new List<FakeTemperatureSensor>
            {
            new FakeTemperatureSensor(),
            new FakeTemperatureSensor()
            };

        private int currentSensor = 0;

        public bool MoreSensors => currentSensor < sensors.Count;
        public FakeTemperatureSensor NextSensor => sensors[currentSensor++];
    }
}
