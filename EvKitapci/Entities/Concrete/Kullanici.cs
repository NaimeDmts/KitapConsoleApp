using EvKitapci.Entities.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.Entities.Concrete
{
    [Table("TblKullanici")]
    public class Kullanici :BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Ad { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Soyad { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Sifre { get; set; }

    }
}
