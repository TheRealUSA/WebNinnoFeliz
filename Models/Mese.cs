using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class Mese
    {
        public Mese()
        {
            UsoComedores = new HashSet<UsoComedore>();
        }

        [Key]
        [Column("idMes")]
        public int IdMes { get; set; }
        [Required]
        [Column("nombreMes")]
        [StringLength(10)]
        public string NombreMes { get; set; }

        [InverseProperty(nameof(UsoComedore.IdMesNavigation))]
        public virtual ICollection<UsoComedore> UsoComedores { get; set; }
    }
}
