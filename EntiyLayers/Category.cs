using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntiyLayers
{
    [Table("Categories")]
    public class Category : EntityBase
    {
        [Required,StringLength(50)]
        public string Title { get; set; }
        [StringLength(150)]
        public string Description { get; set; }

        //Categoryinin notları var.İlişkisel olduğu için virtual olarak tanımladım.
        public virtual List<Note> Notes { get; set; }

        //Not eklediğimde null hatası almıyım.
        public Category()
        {
            Notes = new List<Note>();
        }



    }
}
