using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using work_01.Models;

namespace work_01.Controllers
{
    public class CategoriesController : Controller
    {
        private ToyDbContext db = new ToyDbContext();
        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
    }
}