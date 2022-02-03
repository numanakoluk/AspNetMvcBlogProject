using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    
    public class Test
    {
        public Test()
        {
            DataAccessLayer.DataBaseContext db = new DataAccessLayer.DataBaseContext();
            //db.Database.CreateIfNotExists(); //Ctor Tanımlayarak Açtığımda DB Oluştur diyorum.Bunu da Contreller'da çağırıcam
            db.Categories.ToList(); // Fake Data için bir tane data çağırmam yeterli şimdilik.
        }
    }
}
