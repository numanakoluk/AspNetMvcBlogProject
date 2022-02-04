using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    //Singleton Pattern : Bir kere newlensin amacım kontrollü bir şekilde DataBaseContext classının oluşmasını sağlamak.
    public class RepositoryBase
    {
        protected static DataBaseContext context; //Başta newlemedim amaç zaten newlememek.Protected'a çevirdim ki Repository Miras alsın.
        private static object _lockSync = new object(); //Lock'lama için
        protected RepositoryBase() //Artık bu class newlenemez sadece Miras alan newleyebilir.
        {
            CreateContext(); //Miras alındığında ctor'da çalışacak.
        }
        public static void CreateContext() //Statik yaptığımdan ötürü newlenemez.
        {
            if(context==null) //Null'sa newle Değilse bir daha newleme.
            {
                lock(_lockSync) //sadece 1 parçacaık bunu çalıştırabilsin demiş oluyoruz.Multithread uygulamalar için
                {
                    if (context == null) //İşi garantiye alıyorum.
                    {
                        context = new DataBaseContext();
                    }
                }
            }
        }
    }
}
