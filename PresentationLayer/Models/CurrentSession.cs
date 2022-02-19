using EntiyLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class CurrentSession
    {
        //Her yerde Session Çağırılmayacak, ve bu session sadece web'de kullanılacağı için.
        public static NoteUser User //statik yapıldı ki newlemeden kullanabileyim.
        { 
            get {
                return Get<NoteUser>("login");
                }
                
        } 
        
        //set
        public static void Set<T>(string key, T obj)
        {
            HttpContext.Current.Session[key] = obj; //verdiğimiz session anahtarına objeyi atıyorum.
        }
        //get otomatik çalış
        public static T Get<T>(string key)
        {
            if (HttpContext.Current.Session[key] !=null)
            {
                return (T)HttpContext.Current.Session[key];
            }
            return default(T); //Eğer class vermişsek null, ne verirsek null döncek 
        }

        //Kontrol
        public static void Remove(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
           
        }
        //Sessionu temizle
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}