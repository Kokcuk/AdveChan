using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoCRUD.Models;
using MongoRepository;

namespace MongoCRUD.Controllers
{
    public class TestController : Controller
    {
        private readonly MongoRepository<Cat> _catRep;

        public TestController()
        {
            _catRep = new MongoRepository<Cat>();
        }
        public ActionResult CreateSomeCats()
        {
            var testCat = new Cat
            {
                Name = "Busya",
                Age = 7,
                Breed = "Devon-Rex",
                Weight = 3.25,
                Url = "someUrl"
            };
            var testCat2 = new Cat
            {
                Name = "Barsik",
                Age = 6,
                Breed = "Trash-Cat",
                Weight = 4.15,
                Url = "someUrl"
            };
            _catRep.Add(testCat);
            _catRep.Add(testCat2);
            return null;
        }
    }
}