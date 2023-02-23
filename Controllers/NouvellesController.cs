using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsManager.Models;

namespace NewsManager.Controllers
{
    public class NouvellesController : Controller
    {
        private AppDBEntities DB = new AppDBEntities();

        public ActionResult Index()
        {
            if (Session["CurrentCategoryId"] == null)
                Session["CurrentCategoryId"] = 0;
            ViewBag.Categories = DB.CategoriesToSelectList("Toutes catégories");
            return View(DB.GetNouvelles((int)Session["CurrentCategoryId"]));
        }
        public ActionResult SetCurrentCategoryId(int id)
        {
            Session["CurrentCategoryId"] = id;
            return RedirectToAction("Index");
        }

        public PartialViewResult NouvelleForm(Nouvelle nouvelle)
        {
            return PartialView(nouvelle);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Nouvelle());
        }

        [HttpPost]
        public ActionResult Create(Nouvelle nouvelle)
        {
            if (ModelState.IsValid)
            {
                DB.AddNouvelle(nouvelle);
                return RedirectToAction("Index");
            }
            return View(new Nouvelle());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Nouvelle nouvelleToEdit = DB.Nouvelles.Find(id);
            if (nouvelleToEdit != null)
            {
                ViewBag.Categories = DB.CategoriesNameList();
                return View(nouvelleToEdit);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Nouvelle nouvelle)
        {
            if (ModelState.IsValid)
            {
                DB.UpdateNouvelle(nouvelle);
                return RedirectToAction("Index");
            }
            return View(new Nouvelle());
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Nouvelle nouvelleToEdit = DB.Nouvelles.Find(id);
            if (nouvelleToEdit != null)
            {
                ViewBag.Categories = DB.CategoriesNameList();
                return View(nouvelleToEdit);
            }
            return RedirectToAction("Index");
        }
    }
}