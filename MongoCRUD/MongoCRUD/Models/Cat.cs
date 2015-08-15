using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoRepository;

namespace MongoCRUD.Models
{
    [CollectionName("CatsCollection")]
    public class Cat:Entity
    {
        [RegularExpression(@"^[A-Z][a-z]+")]
        [Required (ErrorMessage = "Please, input correct name.")]
        [StringLength(30)]
        public string Name { get; set; }
        [RegularExpression(@"[0-9][0-9]?")]
        [Required(ErrorMessage = "Please, input correct age.")]
        public int Age { get; set; }
        [RegularExpression(@"[a-zA-Z]+")]
        [Required(ErrorMessage = "Please, input correct breed.")]
        public string Breed { get; set; }
        [RegularExpression(@"^[0-9]+\.[0-9][0-9]")]
        [Required(ErrorMessage = "Please, input correct weight.")]
        public double Weight { get; set; }
        public string Url { get; set; }
    }
}