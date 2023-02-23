using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NewsManager.Models
{
    [MetadataType(typeof(NouvelleView))]
    public partial class Nouvelle
    {
        public Nouvelle()
        {
            DateAjout = DateTime.Now;
        }
        [Display(Name = "Catégorie"), Required(ErrorMessage = "Requis")]
        public string CategoryName { get; set; }
    }

    public class NouvelleView
    {
        [Required(ErrorMessage = "Requis")]
        public string Titre { get; set; }
        [Required(ErrorMessage = "Requis")]
        public string Texte { get; set; }
        [DataType(DataType.Url)]
        [Display(Name = "Url de l'image"), Required(ErrorMessage = "Requis")]
        public string ImageUrl { get; set; }
        [DataType(DataType.DateTime), Display(Name = "Création")]
        public System.DateTime DateAjout { get; set; }
    }
}