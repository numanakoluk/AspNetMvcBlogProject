using BusinessLayer;
using EntiyLayers;
using PresentationLayer.ViewModels;
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
            //CategoryController üzerinden gelen view Talebi ve model
            //if(TempData["mm"]!= null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}
            NoteManager nm = new NoteManager();
            
            return View(nm.GetAllNote().OrderByDescending(x=>x.ModifiedOn).ToList()); //Son yazılanları sıralayarak. C sharp tarafından
            //return View(nm.GetAllNote().OrderByDescending(x=>x.ModifiedOn).ToList()); //Sql Tarafından 

        }
        public ActionResult ByCategory(int? id) //id boş geçilerek de select cagırılabilir.
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value); //value ile ilgili değeri getir.

            if (cat == null)
            {
                return HttpNotFound();
                //return RedirectToAction("Index", "Home"); // Bu da olabilirdi
            }
            return View("Index", cat.Notes.OrderByDescending(x=>x.ModifiedOn).ToList());
        }
        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();

            return View("Index",nm.GetAllNote().OrderByDescending(x => x.LikeCount).ToList()); //ındex viewinda
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //Giriş Kontrolü
            //Session'a kullanıcı bilgi saklama
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Username=="aaa")
                {
                    ModelState.AddModelError("", "Kullanıcı Adı Kullanılıyor.");
                }
                if (model.Email == "aaa@aa.com") 
                {
                    ModelState.AddModelError("", "E-Posta Adresi Kullanılıyor.");
                }
                foreach (var item in ModelState)
                {
                    if (item.Value.Errors.Count>0)
                    {
                        return View(model);
                    }
                }

                return RedirectToAction("RegisterOk"); //Her şey okeyse.
            }
            //Kullanıcı username kontrolü
            //Kullanıcı e-posta kontrolü
            //Kayıt işlemi
            //Kayıt işlemi
            //Aktivasyon e-postası gönderimi.
            return View(model);
        }
        public  ActionResult RegisterOk()
        {
            return View();

        }
        public ActionResult ActivateUser(Guid activate_id)
        {
            //kullanıcı aktivasyonu sağlanacak
            return View();
        }
        public ActionResult Logout()
        {
            return View();
        }


    }
}