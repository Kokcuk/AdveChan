using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoRepository;

namespace MongoCRUD.Models
{
    [CollectionName("CatsCollection")]
    public class Cat:Entity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public double Weight { get; set; }
        public string Url { get; set; }
    }
}