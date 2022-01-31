using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntiyLayers
{
    [Table("Likes")]
    public class Liked
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Çoklu İlişki için bunu kullanıyorum.Bir notun birden çok kullanıcısı.Bir kullanıcın birden çok notu olabilir.
        public int Id { get; set; }
        public virtual Note Note { get; set; }
        public virtual NoteUser LikedUser { get; set; }
    }
}
