using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    [Table("Ninno_Alergia_Ingredientes")]
    public partial class NinnoAlergiaIngrediente
    {
        public NinnoAlergiaIngrediente()
        {
            NaiPlatos = new HashSet<NaiPlato>();
        }

        [Key]
        [Column("idNinnoAlergiaIngrediente")]
        public int IdNinnoAlergiaIngrediente { get; set; }
        [Column("idAlergia")]
        public int IdAlergia { get; set; }
        [Column("idIngrediente")]
        public int IdIngrediente { get; set; }
        [Column("idNinno")]
        public int IdNinno { get; set; }

        [ForeignKey(nameof(IdAlergia))]
        [InverseProperty(nameof(Alergia.NinnoAlergiaIngredientes))]
        public virtual Alergia IdAlergiaNavigation { get; set; }
        [ForeignKey(nameof(IdIngrediente))]
        [InverseProperty(nameof(Ingrediente.NinnoAlergiaIngredientes))]
        public virtual Ingrediente IdIngredienteNavigation { get; set; }
        [ForeignKey(nameof(IdNinno))]
        [InverseProperty(nameof(Ninno.NinnoAlergiaIngredientes))]
        public virtual Ninno IdNinnoNavigation { get; set; }
        [InverseProperty(nameof(NaiPlato.IdNinnoAlergiaIngredienteNavigation))]
        public virtual ICollection<NaiPlato> NaiPlatos { get; set; }
    }
}
