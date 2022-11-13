using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class Menu
    {
        public Menu()
        {
            MenuPlatos = new HashSet<MenuPlato>();
            NinnoMenus = new HashSet<NinnoMenu>();
        }

        [Key]
        [Column("idNumeroMenu")]
        public int IdNumeroMenu { get; set; }
        [Required]
        [Column("nombreMenu")]
        [StringLength(20)]

        [Display(Name = "Nombre menú")]
        public string NombreMenu { get; set; }

        [InverseProperty(nameof(MenuPlato.IdNumeroMenuNavigation))]
        public virtual ICollection<MenuPlato> MenuPlatos { get; set; }
        [InverseProperty(nameof(NinnoMenu.IdNumeroMenuNavigation))]
        public virtual ICollection<NinnoMenu> NinnoMenus { get; set; }
    }
}
