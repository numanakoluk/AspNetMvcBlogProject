using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Kategori"), Required(ErrorMessage ="{0} alanı gereklidir."),StringLength(50, ErrorMessage = "{0} alanı max. {1} karekter içermeli.")]
        public string Title { get; set; }
        
        [DisplayName("Açıklama"), StringLength(150, ErrorMessage = "{0} alanı max. {1} karekter içermeli.")]
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
