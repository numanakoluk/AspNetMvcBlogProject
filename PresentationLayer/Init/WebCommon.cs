using Common;
using EntiyLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Init
{
    public class WebCommon : ICommon
    {
    
        public string GetCurrentUserName()
        {
            if (HttpContext.Current.Session["login"]!= null) //sessiona erişim
            {
                NoteUser user = HttpContext.Current.Session["login"] as NoteUser;
                return user.UserName; //noteuser varsa
            }
            return null;
        }
    }
}