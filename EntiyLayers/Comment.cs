using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntiyLayers
{
    [Table("Comments")]
    public class Comment : EntityBase
    {
        [Required,StringLength(300)]
        public string Text { get; set; }

        //Hangi Yazıya Not eklenecek
        public virtual Note Note { get; set; }

        public virtual NoteUser Owner { get; set; }
    }
}
