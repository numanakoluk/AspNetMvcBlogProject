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
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    
                    IsActive = false,
                    IsAdmin = false,
                   

                });
                 if(dbresult>0)
                {
                    res.Result= repo_user.Find(x => x.Email == data.Email && x.UserName == data.Username);
                    //TODO: aktivasyon maili atılacak.
                }
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
    }
}
