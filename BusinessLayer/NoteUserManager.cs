using Common.Helpers;
using DataAccessLayer.EntityFramework;
using EntiyLayers;
using EntiyLayers.Messages;
using EntiyLayers.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class NoteUserManager
    {
        private Repository<NoteUser> repo_user = new Repository<NoteUser>();
       public BusinessLayerResult<NoteUser> RegisterUser(RegisterViewModel data)
        {
            //Kullanıcı username kontrolü
            //Kullanıcı e-posta kontrolü
            //Kayıt işlemi
            //Kayıt işlemi
            //Aktivasyon e-postası gönderimi.
           NoteUser user= repo_user.Find(x => x.UserName == data.Username || x.Email == data.Email);
           BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();
            if (user!=null)
            {
                if (user.UserName == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists,"Kullanıcı Adı Kayıtlı");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlredyExists, "E-Posta Adresi Kayıtlı");
                }
            }
            else
            {
                int dbresult=repo_user.Insert(new NoteUser()
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
                    res.Result= repo_user.Find(x => x.Email == data.Email && x.UserName == data.Username);
                    //TODO: aktivasyon maili atılacak.
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.UserName};<br><br>Hesabını aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>.";
                    MailHelper.SendMail(body, res.Result.Email, "Blog Hesap Aktifleştirme");
                }
               
            }
            return res;
        }

        public BusinessLayerResult<NoteUser> GetUserByID(int id)
        {
            BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();
            res.Result = repo_user.Find(x => x.Id == id);
            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı.");
            }
            return res;
        }

        public BusinessLayerResult<NoteUser> LoginUser(LoginViewModel data)
        {
            //Giriş Kontrolü
            //Hesap Aktive Edilmiş mi?
            
            BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();
            res.Result = repo_user.Find(x => x.UserName == data.Username && x.Password == data.Password);
            
            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı Aktifleştirilmemiştir");
                
                res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen E-Posta Adresinizi Kontrol Ediniz.");
                }
                

            }
            else
            {
                res.AddError(ErrorMessageCode.UsernamaOrPassWrong, "Kullanıcı Adı veya Şifre Uyuşmuyor.");
                
            }
            return res;
        }
        public BusinessLayerResult<NoteUser> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();
            res.Result = repo_user.Find(x => x.ActivateGuid == activateId);

            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı Zaten Aktif Edilmiştir.");
                    return res;
                }
                res.Result.IsActive = true;
                repo_user.Update(res.Result);
            }

            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirecek Kullanıcı Bulunamadı.");

            }

            return res;

        }

        public BusinessLayerResult<NoteUser> UpdateProfile(NoteUser data)
        {
            NoteUser db_user = repo_user.Find(x => x.Id != data.Id && (x.UserName == data.UserName || x.Email == data.Email));
            BusinessLayerResult<NoteUser> res = new BusinessLayerResult<NoteUser>();

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlredyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = repo_user.Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.UserName = data.UserName;

            if (string.IsNullOrEmpty(data.ProfileImageFileName) == false)
            {
                res.Result.ProfileImageFileName = data.ProfileImageFileName;
            }

            if (repo_user.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");
            }

            return res;
        }
    }
}
