using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntiyLayers
{
    [Table("Reklamlar")]
    public class Advertisement : EntityBase
    {
        public string AdvertisementImageFileName { get; set; }

    }
}
