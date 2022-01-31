using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntiyLayers
{
    public class EntityBase
    {
        //Her Tabloda bu 4 özellik olacak
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Otomatik Artan Ve Identity
        public int Id { get; set; }
        [Required] //Boş Geçilemez
        public DateTime CreatedOn { get; set; }
        [Required]
        public DateTime ModifiedOn { get; set; }
        [Required, StringLength(30)]
        public string ModifiedUserName { get; set; }




    }
}
