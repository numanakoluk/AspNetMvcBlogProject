using BusinessLayer;
using BusinessLayer.Results;
using EntiyLayers;
using EntiyLayers.Messages;
using EntiyLayers.RegisterViewModel;
using EntiyLayers.ViewModel;
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
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private NoteUserManager noteUserManager = new NoteUserManager();
        private AboutManager aboutManager = new AboutManager();

        // GET: Home
        public ActionResult Index()
        {
            //CategoryController üzerinden gelen view Talebi ve model
            //if(TempData["mm"]!= null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}
            
            //Draft olmayanlar gelsin için where sorgusu ekledim.
            return View(noteManager.ListQueryable().Where(x=>x.IsDraft == false).OrderByDescending(x=>x.ModifiedOn).ToList()); //Son yazılanları sıralayarak. C sharp tarafından
            //return View(nm.GetAllNote().OrderByDescending(x=>x.ModifiedOn).ToList()); //Sql Tarafından 

        }
        public ActionResult ByCategory(int? id) //id boş geçilerek de select cagırılabilir.
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            //Category cat = categoryManager.Find(x=>x.Id == id.Value); //value ile ilgili değeri getir.

            //if (cat == null)
            //{
            //    return HttpNotFound();
            //    //return RedirectToAction("Index", "Home"); // Bu da olabilirdi
            //}

            //Notları çekip direk aktaracak.
            List<Note> notes = noteManager.ListQueryable().Where(
                x => x.IsDraft == false && x.CategoryId == id).OrderByDescending(
                x => x.ModifiedOn).ToList();

            return View("Index", notes);
        }
        public ActionResult MostLiked()
        {

            return View("Index", noteManager.ListQueryable().OrderByDescending(x => x.LikeCount).ToList()); //ındex viewinda
        }
        public ActionResult About()
        {

            aboutManager.List();



            return View();
        }
        //Profil Goruntuleme
        public ActionResult ShowProfile()
        {
            NoteUser currentUser = Session["login"] as NoteUser;
            BusinessLayerResult<NoteUser> res = noteUserManager.GetUserByID(currentUser.Id);
            if(res.Errors.Count>0)
            {
                ErrrorViewModel errorNotfiyObje = new ErrrorViewModel()
                {
                    Title = "Hata Oluştu.",
                    Items = res.Errors

                };

                return View("Error", errorNotfiyObje);
            }
            return View(res.Result);
        }

        public ActionResult EditProfile()
        {
            NoteUser currentUser = Session["login"] as NoteUser;
            BusinessLayerResult<NoteUser> res = noteUserManager.GetUserByID(currentUser.Id);
            if (res.Errors.Count > 0)
            {
                ErrrorViewModel errorNotfiyObje = new ErrrorViewModel()
                {
                    Title = "Hata Oluştu.",
                    Items = res.Errors

                };

                return View("Error", errorNotfiyObje);
            }
            return View(res.Result);
        }

        [HttpPost]
        public ActionResult EditProfile(NoteUser model, HttpPostedFileBase ProfileImage) //HttpPosteFileBase ile img çekilecek.
        {
            
            ModelState.Remove("ModifiedUserName"); //Hata mesajında görünmesin.
            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
                    (ProfileImage.ContentType == "image/jpeg" ||
                    ProfileImage.ContentType == "image/jpg" ||
                    ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}"; //Dosya Adı

                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}")); //Dosyayı kaydet
                    model.ProfileImageFileName = filename;
                }
                BusinessLayerResult<NoteUser> res = noteUserManager.UpdateProfile(model);
                if (res.Errors.Count > 0)
                {
                    ErrrorViewModel errorNotifyObj = new ErrrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Güncellenemedi.",
                        RedirectingUrl = "/Home/EditProfile"
                    };

                    return View("Error", errorNotifyObj);
                }
                Session["login"] = res.Result; //Profil güncellendiği için session güncellendi.

                return RedirectToAction("ShowProfile");

            }
            return View(model);
        }

        //Get kısmında silinecek post kısmı js ile yapılacak
        public ActionResult DeleteProfile()
        {
            NoteUser currentUser = Session["login"] as NoteUser;

            BusinessLayerResult<NoteUser> res = noteUserManager.RemoveUserById(currentUser.Id);

            if (res.Errors.Count>0)
            {
                ErrrorViewModel errorNotifyObj = new ErrrorViewModel()
                {
                    Items = res.Errors,
                    Title = "Profil Silinemedi.",
                    RedirectingUrl = "/Home/ShowProfile"
                };
                return View("Error", errorNotifyObj);
            }
            Session.Clear();
            return RedirectToAction("Index");

        }

        //public ActionResult TestNotify() 
        //{

        //   ErrrorViewModel model = new ErrrorViewModel() //**Dikkat
        //    {
        //        Header = "Yönlendirme..",
        //        Title = "Ok Test",
        //        RedirectTimeOut = 3000,
        //        Items = new List<ErrorMessageObj>()
        //        { new ErrorMessageObj() { Message = "Test başarılı 1" },
        //          new ErrorMessageObj() { Message="Test başarılı 2"}}
        //    };
        //    return View("Error",model); //Eğer bunu yapmasam düz açılacak
        //}

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {                  
            if (ModelState.IsValid)
            {
                BusinessLayerResult<NoteUser> res = noteUserManager.LoginUser(model);
                if (res.Errors.Count > 0)
                {
                    if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null) //Sınıf yazma sebebi bu
                    {
                        ViewBag.SetLink = "http://Home/Activate/1234-4567-7890"; //Aslında amaç gelen hata mesajlarını da ayıklamak.
                    }
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }
                Session["login"] = res.Result;//Session'a kullanıcı bilgi saklama
                return RedirectToAction("Index"); //Yönlendirme
            }
            return View(model);
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
                BusinessLayerResult<NoteUser> res = noteUserManager.RegisterUser(model);
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                    

                }

                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login",
                    
                };
                notifyObj.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon link'ine tıklayarak hesabınızı aktive ediniz. Hesabınızı aktive etmeden not ekleyemez ve beğenme yapamazsınız.");

                return View("Ok",notifyObj); //Her şey okeyse.
            }
           
            return View(model);
        }
   
        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<NoteUser> res= noteUserManager.ActivateUser(id);

            if (res.Errors.Count>0)
            {
                ErrrorViewModel errorNotfiyObje = new ErrrorViewModel()
                {
                    Title="Geçersiz İşlem",
                    Items = res.Errors

                };

                return View("Error", errorNotfiyObje);
            }
            OkViewModel okNotifyObj = new OkViewModel()
            {
                Title ="Hesap Aktifleştirildi",
                RedirectingUrl = "/Home/Login"
            };
            okNotifyObj.Items.Add("Hesabınız Aktifleştirildi.Artık not paylaşabilir ve beğenme yapabilirsiniz.");

            return View("Ok",okNotifyObj);
        }

        //public ActionResult UserActivateOk()
        //{
        //    //kullanıcı aktivasyonu sağlanacak
        //    return View();
        //}

        //public ActionResult UserActivateCancel()
        //{
        //    List<ErrorMessageObj> errors = null;
        //    if (TempData["errors"] != null)
        //    {
        //         errors= TempData["errors"] as List<ErrorMessageObj>; //TempData obje olarak tuttuğu için as diyerek tip dönüşümü

        //    }
            

        //    return View(errors);
        //}

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }


    }
}