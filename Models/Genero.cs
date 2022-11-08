using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    [Table("Genero")]
    public partial class Genero
    {
        public Genero()
        {
            Ninnos = new HashSet<Ninno>();
        }

        [Key]
        [Column("idGenero")]
        public int IdGenero { get; set; }
        [Required]
        [Column("detalleGen")]
        [StringLength(10)]
        public string DetalleGen { get; set; }

        [InverseProperty(nameof(Ninno.IdGeneroNavigation))]
        public virtual ICollection<Ninno> Ninnos { get; set; }
    }
}
