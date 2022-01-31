using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Category :EntityBase
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
         
        //Categoryinin notları var.İlişkisel olduğu için virtual olarak tanımladım.
        public virtual List<Note> Notes { get; set; }
        



    }
}
