using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    [Table("Ninno_Menus")]
    public partial class NinnoMenu
    {
        [Key]
        [Column("idNinnoMenu")]
        public int IdNinnoMenu { get; set; }
        [Column("fechaConsumido", TypeName = "date")]
        public DateTime FechaConsumido { get; set; }
        [Column("idNinno")]
        public int IdNinno { get; set; }
        [Column("idNumeroMenu")]
        public int IdNumeroMenu { get; set; }

        [ForeignKey(nameof(IdNinno))]
        [InverseProperty(nameof(Ninno.NinnoMenus))]
        public virtual Ninno IdNinnoNavigation { get; set; }
        [ForeignKey(nameof(IdNumeroMenu))]
        [InverseProperty(nameof(Menu.NinnoMenus))]
        public virtual Menu IdNumeroMenuNavigation { get; set; }
    }
}
