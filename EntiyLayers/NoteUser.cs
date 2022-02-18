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
    [Table("NoteUsers")]
    public class NoteUser : EntityBase
    {
        [DisplayName("İsim"),StringLength(25,ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]     
        public string Name { get; set; }
        [DisplayName("Soyad"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage ="{0} alanı gereklidir."), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string UserName { get; set; }
        [DisplayName("EPosta"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Password { get; set; }

        [StringLength(30),ScaffoldColumn(false)] //kullanıcı_12.jpg gibi //ScaffoldColumn üretilirken istemiyorum.
        public string ProfileImageFileName { get; set; }

        //Aktif mi kullanıcı?
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("Is Admin")]

        public bool IsAdmin { get; set; }

        //Kullanıcı için oluşturulan şifreli string kodu.Guid, benzersiz değerler oluşturmak için kullanılmaktadır
        [Required,ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }
        
        
        //Bir Kullanıcının birden çok notu var
        public virtual List<Note> Notes { get; set; }
        //Bir kullanıcının birden çok yorumu olabilir
        public virtual List<Comment> Comments { get; set; }

        //Bir kullanıcının birden fazla like'ı vardır çoka çok
        public virtual List<Liked> Likes { get; set; }

    }
}
