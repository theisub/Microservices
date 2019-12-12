using System;
using System.Collections.Generic;
using System.Text;
using BusAPI.Model;

namespace WebApiTest
{
    public class BusActionsTest 
    {
        private readonly List<Bus> bus;

        public BusActionsTest()
        {
            bus = new List<Bus>()
            {
                new Bus() { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false },
                new Bus() { BusCompany = "TourCompany2", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Bordeaux", Price = 66, TravelTime = 94, Transit = false },
                 new Bus() { BusCompany = "TourCompany3", InCountry = "Russia", OutCountry = "Germany", InCity = "Omsk", OutCity = "Berlin", Price = 148, TravelTime = 210, Transit = false },
            };
        }

    }
}
