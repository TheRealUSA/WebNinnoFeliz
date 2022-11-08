using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class Alergia
    {
        public Alergia()
        {
            NinnoAlergiaIngredientes = new HashSet<NinnoAlergiaIngrediente>();
        }

        [Key]
        [Column("idAlergia")]
        public int IdAlergia { get; set; }
        [Required]
        [Column("nombreAlergia")]
        [StringLength(20)]
        public string NombreAlergia { get; set; }
        [Column("idTipoAlergia")]
        public int IdTipoAlergia { get; set; }

        [ForeignKey(nameof(IdTipoAlergia))]
        [InverseProperty(nameof(TipoAlergia.Alergia))]
        public virtual TipoAlergia IdTipoAlergiaNavigation { get; set; }
        [InverseProperty(nameof(NinnoAlergiaIngrediente.IdAlergiaNavigation))]
        public virtual ICollection<NinnoAlergiaIngrediente> NinnoAlergiaIngredientes { get; set; }
    }
}
