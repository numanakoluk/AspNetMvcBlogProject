using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Filters
{
    public class AuthLogin : FilterAttribute, IAuthorizationFilter
    {
        //Artık authlogini hangi contrellera eklersem ya da hangi methoda eklersem kullanıcıyı logine yönlendirecek.

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if(CurrentSession.User == null) //login yapılmadı.
            {
                filterContext.Result = new RedirectResult("/Home/Login"); //Bu sayfaya yönlendir.
            }
        }
    }
}