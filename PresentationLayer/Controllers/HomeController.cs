using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //BL' deki Ctoru cağırıyorum.
            BusinessLayer.Test test = new BusinessLayer.Test();
            //Ekleme islemi icin
            //test.InsertTest();

            //Update işlemi için
            //test.UpdateTest();
            //Delete işlemi için
            //test.DeleteTest();
            test.CommentTest();
            return View();
        }
  
    }
}