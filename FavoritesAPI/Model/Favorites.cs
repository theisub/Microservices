using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlaneAPI.Model;
using BusAPI.Model;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FavoritesAPI.Model
{
    public class Favorites
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string InCountry { get; set; }
        public string OutCountry { get; set; }
        public string InCity { get; set; }
        public string OutCity { get; set; }
        // public Route route { get; set; }
        public IEnumerable<Route> BusesRoute { get; set; }
        public IEnumerable<Route> PlanesRoute { get; set; }

    }

}
