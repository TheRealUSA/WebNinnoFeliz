using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    [Table("Menu_Platos")]
    public partial class MenuPlato
    {
        [Key]
        [Column("idnumeroMenuPlato")]
        public int IdnumeroMenuPlato { get; set; }
        [Column("idNumeroMenu")]
        public int IdNumeroMenu { get; set; }
        [Column("idPlato")]
        public int IdPlato { get; set; }

        [ForeignKey(nameof(IdNumeroMenu))]
        [InverseProperty(nameof(Menu.MenuPlatos))]
        public virtual Menu IdNumeroMenuNavigation { get; set; }
        [ForeignKey(nameof(IdPlato))]
        [InverseProperty(nameof(Plato.MenuPlatos))]
        public virtual Plato IdPlatoNavigation { get; set; }
    }
}
