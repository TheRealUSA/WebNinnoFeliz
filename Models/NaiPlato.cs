using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    [Table("NAI_Platos")]
    public partial class NaiPlato
    {
        [Key]
        [Column("idNAIPlato")]
        public int IdNaiplato { get; set; }
        [Column("idNinnoAlergiaIngrediente")]
        public int IdNinnoAlergiaIngrediente { get; set; }
        [Column("idPlato")]
        public int IdPlato { get; set; }

        [ForeignKey(nameof(IdNinnoAlergiaIngrediente))]
        [InverseProperty(nameof(NinnoAlergiaIngrediente.NaiPlatos))]
        public virtual NinnoAlergiaIngrediente IdNinnoAlergiaIngredienteNavigation { get; set; }
        [ForeignKey(nameof(IdPlato))]
        [InverseProperty(nameof(Plato.NaiPlatos))]
        public virtual Plato IdPlatoNavigation { get; set; }
    }
}
