using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    [Table("Plato_Ingredientes")]
    public partial class PlatoIngrediente
    {
        [Key]
        [Column("idPlatoIngrediente")]
       
        public int IdPlatoIngrediente { get; set; }
        [Column("idPlato")]
        [StringLength(20)]
        [Display(Name = "Nombre plato")]
        public int IdPlato { get; set; }
        [Column("idIngrediente")]
        [StringLength(20)]

        [Display(Name = "Nombre ingrediente ")]
        public int IdIngrediente { get; set; }

        [ForeignKey(nameof(IdIngrediente))]
        [InverseProperty(nameof(Ingrediente.PlatoIngredientes))]
        public virtual Ingrediente IdIngredienteNavigation { get; set; }
        [ForeignKey(nameof(IdPlato))]
        [InverseProperty(nameof(Plato.PlatoIngredientes))]
        public virtual Plato IdPlatoNavigation { get; set; }
    }
}
