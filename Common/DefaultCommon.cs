using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DefaultCommon : ICommon
    {
        //2-)GetCurrent Name'i n default değeri
       public string GetCurrentUserName()
        {
            return "system";
        }
    }
}
