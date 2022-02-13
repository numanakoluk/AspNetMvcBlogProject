using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntiyLayers
{
    [Table("NoteUsers")]
    public class NoteUser : EntityBase
    {
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(25)]
        public string Surname { get; set; }
        [Required, StringLength(25)]
        public string UserName { get; set; }
        [Required, StringLength(70)]
        public string Email { get; set; }
        [Required,StringLength(25)]
        public string Password { get; set; }

        [StringLength(30)] //kullanıcı_12.jpg gibi
        public string ProfileImageFileName { get; set; }

        //Aktif mi kullanıcı?
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        //Kullanıcı için oluşturulan şifreli string kodu.Guid, benzersiz değerler oluşturmak için kullanılmaktadır
        [Required]
        public Guid ActivateGuid { get; set; }
        
        
        //Bir Kullanıcının birden çok notu var
        public virtual List<Note> Notes { get; set; }
        //Bir kullanıcının birden çok yorumu olabilir
        public virtual List<Comment> Comments { get; set; }

        //Bir kullanıcının birden fazla like'ı vardır çoka çok
        public virtual List<Liked> Likes { get; set; }

    }
}
