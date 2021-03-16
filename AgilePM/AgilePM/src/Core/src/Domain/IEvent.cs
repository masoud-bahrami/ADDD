using System;

namespace AgilePM.Core.Domain
{
    public abstract class Event
    {
        public DateTime OccuredOn { get; }
        public Version Version { get; }
        protected Event()
        {
            OccuredOn = DateTime.Now;
        }
    }

    public class Version
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Path { get; set; }
    }
}