using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class App
    {
        public static ICommon Common = new DefaultCommon(); //Artık şu an DefaultCommon ile çalışıyor.Common.Get UserName dediğimde DefaltCommon sınıfı çalışacak.
    }
}
