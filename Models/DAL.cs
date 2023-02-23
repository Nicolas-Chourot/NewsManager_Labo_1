using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelectListUtilities;

namespace NewsManager.Models
{
    public static class DAL
    {
        public static List<string> CategoriesNameList(this AppDBEntities DB)
        {
            List<string> list = new List<string>();
            foreach (Category categorie in DB.Categories.OrderBy(c => c.Name))
            {
                list.Add(categorie.Name);
            }
            return list;
        }
        public static SelectList CategoriesToSelectList(this AppDBEntities DB, string defaultText = "")
        {
            return SelectListItems<Category>.Convert(DB.Categories.OrderBy(d => d.Name), defaultText);
        }
        public static int GetCategorieIdByName(this AppDBEntities DB, string name)
        {
            name = name.First().ToString().ToUpper() + name.Substring(1).ToLower();
            Category category = DB.Categories.Where(c => c.Name == name).FirstOrDefault();
            if (category == null)
            {
                category = new Category();
                category.Name = name;
                category = DB.Categories.Add(category);
                DB.SaveChanges();
            }
            return category.Id;
        }
        public static IEnumerable<Nouvelle> GetNouvelles(this AppDBEntities DB, int categorieId = 0)
        {
            if (categorieId != 0)
                return DB.Nouvelles.Where(n => n.CategorieId == categorieId).OrderByDescending(n => n.DateAjout);
            return DB.Nouvelles.OrderByDescending(n => n.DateAjout);
        }
        public static Nouvelle AddNouvelle(this AppDBEntities DB, Nouvelle nouvelle)
        {
            nouvelle.CategorieId = DB.GetCategorieIdByName(nouvelle.CategoryName);
            nouvelle = DB.Nouvelles.Add(nouvelle);
            DB.SaveChanges();
            return nouvelle;
        }
        public static bool UpdateNouvelle(this AppDBEntities DB, Nouvelle nouvelle)
        {
            nouvelle.CategorieId = DB.GetCategorieIdByName(nouvelle.CategoryName);
            DB.Entry(nouvelle).State = EntityState.Modified;
            DB.SaveChanges();
            return true;
        }
        public static bool RemoveNouvelle(this AppDBEntities DB, Nouvelle nouvelle)
        {
            Nouvelle nouvelleToDelete = DB.Nouvelles.Find(nouvelle.Id);
            DB.Nouvelles.Remove(nouvelleToDelete);
            DB.SaveChanges();
            return true;
        }
    }
}