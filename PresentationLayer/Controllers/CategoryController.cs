using BusinessLayer;
using EntiyLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class CategoryController : Controller
    {
        
        // GET: TempData ile Category Listeleme
        //public ActionResult Select(int? id) //id boş geçilerek de select cagırılabilir.
        //{
        //    if (id==null)
        //    {
        //        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        //    }
        //    CategoryManager cm = new CategoryManager();
        //    Category cat= cm.GetCategoryById(id.Value); //value ile ilgili değeri getir.

        //    if (cat == null)
        //    {
        //        return HttpNotFound();
        //        //return RedirectToAction("Index", "Home"); // Bu da olabilirdi
        //    }
        //    TempData["mm"] = cat.Notes;
        //    return RedirectToAction("Index","Home");
        //}
    }
}