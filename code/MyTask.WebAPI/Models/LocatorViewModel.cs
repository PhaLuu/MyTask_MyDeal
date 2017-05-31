using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTask.WebAPI.Models
{
    public class Locator
    {
        public string Name { get; set; }
        public virtual List<Passenger> Passengers { get; set; }

        public Locator()
        {
            Passengers = new List<Passenger>();
        }
    }
}