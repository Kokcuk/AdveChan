using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoCRUD.Models;
using MongoCRUD.ViewModels;
using MongoRepository;

namespace MongoCRUD.Controllers
{
    public class ManagmentController : Controller
    {
        private readonly MongoRepository<Cat> _catRep;

        public ManagmentController()
        {
            _catRep = new MongoRepository<Cat>();
        }
        public ActionResult ShowCatList()
        {
            List<Cat> ct = _catRep.OrderBy(x => x.Name).ToList();
            var model = new CatModel
            {
                Cats = ct
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCat(Cat model)
        {
            var cat = new Cat
            {
                Name = model.Name,
                Age = model.Age,
                Breed = model.Breed,
                Weight = model.Weight,
            };
            _catRep.Add(cat);
            return RedirectToAction("ShowCatList");
        }

        [HttpPost]
        public ActionResult DeleteCat(string id)
        {
            var catToDelete = _catRep.GetById(id);
            _catRep.Delete(catToDelete);
            return RedirectToAction("ShowCatList");
        }

        [HttpPost]
        public ActionResult EditCat(Cat model)
        {
            var catToEdit = _catRep.GetById(model.Id);
            catToEdit.Name = model.Name;
            catToEdit.Age = model.Age;
            catToEdit.Breed = model.Breed;
            catToEdit.Weight = model.Weight;
            _catRep.Update(catToEdit);
            return RedirectToAction("ShowCatList");
        }
    }
}