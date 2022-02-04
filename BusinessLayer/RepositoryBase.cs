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
        private static DataBaseContext _db; //Başta newlemedim amaç zaten newlememek
        private static object _lockSync = new object(); //Lock'lama için
        protected RepositoryBase() //Artık bu class newlenemez sadece Miras alan newleyebilir.
        {

        }
        public static DataBaseContext CreateContext() //Statik yaptığımdan ötürü newlenemez.
        {
            if(_db==null) //Null'sa newle Değilse bir daha newleme.
            {
                lock(_lockSync) //sadece 1 parçacaık bunu çalıştırabilsin demiş oluyoruz.Multithread uygulamalar için
                {
                    if (_db == null) //İşi garantiye alıyorum.
                    {
                        _db = new DataBaseContext();
                    }
                
                }
            }
            return _db;
        }
    }
}
