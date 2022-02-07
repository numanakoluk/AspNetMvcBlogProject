using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    //WebConfig içerisindeki key'i okuyup bize geri döndürecek
    public class ConfigHelper
    {
        //Normalde string dönüyordu Fakat ben bunu generic tipe dönüştürüyorum
        public static T Get<T>(string key)
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
        }
    }
}
