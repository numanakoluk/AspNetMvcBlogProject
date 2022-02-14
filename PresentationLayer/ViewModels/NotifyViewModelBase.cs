using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.ViewModels
{
    //View'ler için Bildirim işlemleri ayrı ayrı generic kullandım.Her view'e ViewModel yapıyoruz.
    public class NotifyViewModelBase<T>
    {
        //ErrorMessageObje kısmı Items oluyor
        public List<T> Items { get; set; }

        public string Header { get; set; }
        public string Title { get; set; }

        //Yönlendiriliyor mu?
        public bool IsRedirecting { get; set; }

        //Geçerli Url
        public string RedirectingUrl { get; set; }

        //Saniye için parametreli yapıyorum.
        public int RedirectTimeOut { get; set; }

        //Default değerler
        public NotifyViewModelBase()
        {
            Header = "Yönlendiriliyorsunuz...";
            Title = "Geçersiz İşlem";
            IsRedirecting = true;
            RedirectingUrl = "/Home/Index";
            RedirectTimeOut = 10000;
            Items = new List<T>(); //Devamlı new'lememek için..
        }
    }
}