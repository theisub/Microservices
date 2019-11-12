using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Model
{
    public class Bus
    {
        public int Id { get; set; }
        public string BusCompany { get; set; }
        public string InCountry { get; set; }
        public string OutCountry { get; set; }
        public string InCity { get; set;}
        public string OutCity { get; set; }
        public int Price { get; set; }
        public bool Transit { get; set; }

        public Bus() { }
    }
}
