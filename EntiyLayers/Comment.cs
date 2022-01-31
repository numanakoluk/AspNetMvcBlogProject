using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntiyLayers
{
    public class Comment : EntityBase
    {
        public string Text { get; set; }

        //Hangi Yazıya Not eklenecek
        public virtual Note Note { get; set; }

        public virtual NoteUser Owner { get; set; }
    }
}
