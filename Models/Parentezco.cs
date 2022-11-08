using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class Parentezco
    {
        public Parentezco()
        {
            Encargados = new HashSet<Encargado>();
        }

        [Key]
        [Column("idParentezco")]
        public int IdParentezco { get; set; }
        [Required]
        [Column("detallePar")]
        [StringLength(15)]
        public string DetallePar { get; set; }

        [InverseProperty(nameof(Encargado.IdParentezcoNavigation))]
        public virtual ICollection<Encargado> Encargados { get; set; }
    }
}
