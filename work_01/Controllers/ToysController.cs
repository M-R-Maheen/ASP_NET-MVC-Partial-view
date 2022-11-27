using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using work_01.Models;
using work_01.Models.ViewModels;

namespace work_01.Controllers
{
    public class ToysController : Controller
    {
        private ToyDbContext db = new ToyDbContext();
        // GET: Toys
        public ActionResult Index()
        {
            return View(db.Toys.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.categories = new SelectList(db.Categories, "CId", "CName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToyVM tvm)
        {
            if (ModelState.IsValid)
            {
                if (tvm.Picture != null)
                {
                    //for Image
                    string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(tvm.Picture.FileName));
                    tvm.Picture.SaveAs(Server.MapPath(filePath));

                    //Toys
                    Toy toys = new Toy
                    {
                        ToyName=tvm.ToyName,
                        CId=tvm.CId,
                        Price=tvm.Price,
                        StoreDate=tvm.StoreDate,
                        PicturePath=filePath
                    };
                    db.Toys.Add(toys);
                    db.SaveChanges();
                    return PartialView("_success");
                }
            }
            ViewBag.categories = new SelectList(db.Categories, "CId", "CName");
            return PartialView("_error");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Toy toys = db.Toys.Find(id);

            if (toys == null)
            {
                return HttpNotFound();
            }
            ToyVM tvm = new ToyVM
            {
                Id = toys.Id,
                ToyName = toys.ToyName,
                CId = toys.CId,
                Price = toys.Price,
                StoreDate = toys.StoreDate,
                PicturePath = toys.PicturePath
            };
            ViewBag.categories = new SelectList(db.Categories, "CId", "CName");
            return View(tvm);
        }   
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit( ToyVM tvm )
        {
            if (ModelState.IsValid)
            {
                string filePath = tvm.PicturePath;

                if (tvm.Picture!=null)
                {
                    //for Image
                    filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(tvm.Picture.FileName));
                    tvm.Picture.SaveAs(Server.MapPath(filePath));

                    Toy toys = new Toy
                    {
                        Id = tvm.Id,
                        ToyName = tvm.ToyName,
                        CId = tvm.CId,
                        Price = tvm.Price,
                        StoreDate = tvm.StoreDate,
                        PicturePath = filePath
                    };
                    db.Entry(toys).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Toy toys = new Toy
                    {
                        Id = tvm.Id,
                        ToyName = tvm.ToyName,
                        CId = tvm.CId,
                        Price = tvm.Price,
                        StoreDate = tvm.StoreDate,
                        PicturePath = filePath
                    };
                    db.Entry(toys).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.categories = new SelectList(db.Categories, "CId", "CName");
            return View(tvm);
        }

        public ActionResult Delete( int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Toy toys = db.Toys.Find(id);

            if (toys == null)
            {
                return HttpNotFound();
            }
            return View(toys);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
       

        public  ActionResult DeleteConfirm( int id)
        {
            Toy toys = db.Toys.Find(id);
            string file_name = toys.PicturePath;
            string path = Server.MapPath(file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            db.Toys.Remove(toys);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}