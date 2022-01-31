using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class NoteUser:EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //Aktif mi kullanıcı?
        public bool IsActive { get; set; }

        //Kullanıcı için oluşturulan şifreli string kodu.Guid, benzersiz değerler oluşturmak için kullanılmaktadır
        public Guid ActivateGuid { get; set; }

        public bool IsAdmin { get; set; }
        //Bir Kullanıcının birden çok notu var
        public virtual List<Note> Notes { get; set; }
        //Bir kullanıcının birden çok yorumu olabilir
        public virtual List<Comment> Comments { get; set; }

        //Bir kullanıcının birden fazla like'ı vardır çoka çok
        public virtual List<Liked> Likes { get; set; }

    }
}
