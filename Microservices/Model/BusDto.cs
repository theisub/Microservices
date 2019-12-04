﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusAPI.Model
{
    public class BusDto
    {  
        public long Id { get; set; }
        public string BusCompany { get; set; }
        public string InCountry { get; set; }
        public string OutCountry { get; set; }
        public string InCity { get; set;}
        public string OutCity { get; set; }
        public long Price { get; set; }
        public int TravelTime { get; set; }

        public bool Transit { get; set; }

    }
}
