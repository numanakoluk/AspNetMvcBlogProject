using BusinessLayer.Abstract;
using BusinessLayer.Results;
using Common.Helpers;
using DataAccessLayer.EntityFramework;
using EntiyLayers;
using EntiyLayers.Messages;
using EntiyLayers.RegisterViewModel;
using EntiyLayers.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class NoteUserManager : ManagerBase<NoteUser>
    {
        //AnaSayfa Metodları.
        public BusinessLayerResult<NoteUser> RegisterUser(RegisterViewModel data)
        {
            //Kullanıcı username kontrolü
            //Kullanıcı e-posta kontrolü
            //Kayıt işlemi
            //Kayıt işlemi
            //Aktivasyon e-postası gönderimi.(İptal.)
           NoteUser user= Find(x => x.UserName == data.Username || x.Email == data.Email);
           BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();
            if (user!=null)
            {
                if (user.UserName == data.Username)
                {
                    res.AddError(ErrorMessageCode.KullanıcıAdiKayitli,"Kullanıcı Adı Kayıtlı");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EpostaAdresiKayitli, "E-Posta Adresi Kayıtlı");
                }
            }
            else
            {
                //Base Classa' insert et
                int dbresult=base.Insert(new NoteUser()
                {
                    UserName = data.Username,
                    Email = data.Email,
                    ProfileImageFileName = "user.png",
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    
                    IsActive = false,
                    IsAdmin = false,
                   

                });
                 if(dbresult>0)
                {
                    res.Result= Find(x => x.Email == data.Email && x.UserName == data.Username);
                    //TODO: aktivasyon maili atılacak.
                    //string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    //string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    //string body = $"Merhaba {res.Result.UserName};<br><br>Hesabını aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>.";
                    //MailHelper.SendMail(body, res.Result.Email, "Blog Hesap Aktifleştirme");
                }
               
            }
            return res;
        }

        public BusinessLayerResult<NoteUser> GetUserByID(int id)
        {
            BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();
            res.Result = Find(x => x.Id == id);
            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.KullaniciBulunamadi, "Kullanıcı Bulunamadı.");
            }
            return res;
        }

        public BusinessLayerResult<NoteUser> LoginUser(LoginViewModel data)
        {
            //Giriş Kontrolü
            //Hesap Aktive Edilmiş mi?
            
            BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();
            res.Result = Find(x => x.UserName == data.Username && x.Password == data.Password);
            
            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                res.AddError(ErrorMessageCode.KullaniciAktifDegil, "Kullanıcı Aktifleştirilmemiştir");
                
                res.AddError(ErrorMessageCode.MailKontrol, "Lütfen E-Posta Adresinizi Kontrol Ediniz.");
                }
                

            }
            else
            {
                res.AddError(ErrorMessageCode.KullaniciyadaSifreHatali, "Kullanıcı Adı veya Şifre Uyuşmuyor.");
                
            }
            return res;
        }
        public BusinessLayerResult<NoteUser> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();
            res.Result = Find(x => x.ActivateGuid == activateId);

            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.KullaniciAktif, "Kullanıcı Zaten Aktif Edilmiştir.");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }

            else
            {
                res.AddError(ErrorMessageCode.AktifKullaniciBulunamadi, "Aktifleştirecek Kullanıcı Bulunamadı.");

            }

            return res;

        }

        public BusinessLayerResult<NoteUser> UpdateProfile(NoteUser data)
        {
            NoteUser db_user = Find(x => x.Id != data.Id && (x.UserName == data.UserName || x.Email == data.Email));
            BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.KullanıcıAdiKayitli, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EpostaAdresiKayitli, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.UserName = data.UserName;

            if (string.IsNullOrEmpty(data.ProfileImageFileName) == false)
            {
                res.Result.ProfileImageFileName = data.ProfileImageFileName;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfilGuncellenemedi, "Profil güncellenemedi.");
            }

            return res;
        }


        public BusinessLayerResult<NoteUser> RemoveUserById(int id)
        {
            
            
            BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();
            NoteUser user = Find(x => x.Id == id);
            if (user!= null)
            {
                if (Delete(user) ==0)
                {
                    res.AddError(ErrorMessageCode.KullaniciSilinemedi, "Kullanıcı Silinemedi.");
                    return res;
                }
            }
            else
            {
                //Admin silme durumu..
                res.AddError(ErrorMessageCode.KullaniciSilinemedi, "Kullanıcı Bulunumadı.");
            }
            return res;
        }

        //Method hiding yapıyoruz..
        public new BusinessLayerResult<NoteUser> Insert(NoteUser data)
        {
            //Kullanıcı username kontrolü
            //Kullanıcı e-posta kontrolü
            //Kayıt işlemi
            //Kayıt işlemi
            //Aktivasyon e-postası gönderimi.
            NoteUser user = Find(x => x.UserName == data.UserName || x.Email == data.Email);
            BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();

            res.Result = data;

            if (user != null)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.KullanıcıAdiKayitli, "Kullanıcı Adı Kayıtlı");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EpostaAdresiKayitli, "E-Posta Adresi Kayıtlı");
                }
            }
            else
            {
                //Base Classdaki ınsert'i kullan.
                res.Result.ProfileImageFileName = "user.png";
                res.Result.ActivateGuid = Guid.NewGuid();

                if(base.Insert(res.Result) ==0)
                {
                    res.AddError(ErrorMessageCode.KullaniciEklenemedi, "Kullanıcı Eklenemedi.");
                }

            }
            return res;
        }

        public new BusinessLayerResult<NoteUser> Update(NoteUser data)
        {
            NoteUser db_user = Find(x => x.Id != data.Id && (x.UserName == data.UserName || x.Email == data.Email));
            BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();
            res.Result = data;
            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.KullanıcıAdiKayitli, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EpostaAdresiKayitli, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.UserName = data.UserName;
            res.Result.IsActive = data.IsActive;
            res.Result.IsAdmin = data.IsAdmin;


            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.KullaniciGuncellenemedi, "Kullanıcı güncellenemedi.");
            }

            return res;
        }
    }
}
