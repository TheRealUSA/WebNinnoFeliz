using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class Ingrediente
    {
        public Ingrediente()
        {
            NinnoAlergiaIngredientes = new HashSet<NinnoAlergiaIngrediente>();
            PlatoIngredientes = new HashSet<PlatoIngrediente>();
        }

        [Key]
        [Column("idIngrediente")]
        public int IdIngrediente { get; set; }
        [Required]
        [Column("nombreIngrediente")]
        [StringLength(20)]

        [Display( Name = "Nombre ingrediente")]
        public string NombreIngrediente { get; set; }
      


        [InverseProperty(nameof(NinnoAlergiaIngrediente.IdIngredienteNavigation))]
        public virtual ICollection<NinnoAlergiaIngrediente> NinnoAlergiaIngredientes { get; set; }
        [InverseProperty(nameof(PlatoIngrediente.IdIngredienteNavigation))]
        public virtual ICollection<PlatoIngrediente> PlatoIngredientes { get; set; }
    }
}
