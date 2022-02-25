using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class App
    //Buradaki amaç kullanıcıyı hangi admin ekleyecek bunu belirlemek.
    //Artık şu an DefaultCommon ile çalışıyor.Common.Get UserName dediğimde DefaultCommon sınıfı çalışacak.Ve bu işlem newlemeden olacak statik olduğu için.
    {
        public static ICommon Common = new DefaultCommon(); 
    }
}
