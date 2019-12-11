using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritesAPI.Model
{
    public class Route
    {
        public string CompanyName { get; set; }
        public long Price { get; set; }
        public int travelTime { get; set; }
        public bool Transit { get; set; }
    }
}
