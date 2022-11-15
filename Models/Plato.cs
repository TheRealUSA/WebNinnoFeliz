using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class Plato
    {
        public Plato()
        {
            MenuPlatos = new HashSet<MenuPlato>();
            NaiPlatos = new HashSet<NaiPlato>();
            PlatoIngredientes = new HashSet<PlatoIngrediente>();
        }

        [Key]
        [Column("idPlato")]
        public int IdPlato { get; set; }
        [Required]
        [Column("nombrePlato")]
        [StringLength(20)]
        public string NombrePlato { get; set; }
        [Required]
        [Column("precioPlato")]
        [StringLength(10)]
        public string PrecioPlato { get; set; }

        [InverseProperty(nameof(MenuPlato.IdPlatoNavigation))]
        public virtual ICollection<MenuPlato> MenuPlatos { get; set; }
        [InverseProperty(nameof(NaiPlato.IdPlatoNavigation))]
        public virtual ICollection<NaiPlato> NaiPlatos { get; set; }
        [InverseProperty(nameof(PlatoIngrediente.IdPlatoNavigation))]
        public virtual ICollection<PlatoIngrediente> PlatoIngredientes { get; set; }
    }
}
