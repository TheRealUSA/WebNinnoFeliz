using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    [Table("tipoAlergias")]
    public partial class TipoAlergia
    {
        public TipoAlergia()
        {
            Alergia = new HashSet<Alergia>();
        }

        [Key]
        [Column("idTipoAlergia")]
        public int IdTipoAlergia { get; set; }
        [Required]
        [Column("nombreTipoAlergia")]
        [StringLength(20)]
        public string NombreTipoAlergia { get; set; }

        [InverseProperty("IdTipoAlergiaNavigation")]
        public virtual ICollection<Alergia> Alergia { get; set; }
    }
}
