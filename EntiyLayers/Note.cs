using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntiyLayers
{
    [Table("Notes")]
    public class Note : EntityBase
    {
        [Required, StringLength(60)]
        public string Title { get; set; }
        [Required, StringLength(2000)]
        public string Text { get; set; }
        //Taslak mı ?
        public bool IsDraft { get; set; }
        //Likelanacak notlar.
        public int LikeCount { get; set; }

        //Bunu Yazmazsak eğer Gözükmez ve sorguyu tekrar tekrar çeker.
        public int CategoryId { get; set; }

        //Bir notun bir tane sahibi olabilir
        public virtual NoteUser Owner { get; set; }

        //Her notun bir kategorisi var
        public virtual Category Category { get; set; }
        //Bir notun birden çok yorumu vardır.
        public virtual List<Comment> Comments { get; set; }

        //Bir notun birden çok like'ı vardır çoka çok
        public virtual List<Liked> Likes { get; set; }
    }
}
