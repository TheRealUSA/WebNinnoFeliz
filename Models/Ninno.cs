using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebNinnoFeliz.Models
{
    public partial class Ninno
    {
        public Ninno()
        {
            Asistencia = new HashSet<Asistencia>();
            Matriculas = new HashSet<Matricula>();
            NinnoAlergiaIngredientes = new HashSet<NinnoAlergiaIngrediente>();
            NinnoEncargados = new HashSet<NinnoEncargado>();
            NinnoMenus = new HashSet<NinnoMenu>();
            RegistroBajas = new HashSet<RegistroBaja>();
            UsoComedores = new HashSet<UsoComedore>();
        }

        [Key]
        [Column("idNinno")]
        public int IdNinno { get; set; }
        [Required]
        [Column("identificacionNinno")]
        [StringLength(20)]
        public string IdentificacionNinno { get; set; }
        [Required]
        [Column("nombreNinno")]
        [StringLength(15)]
        public string NombreNinno { get; set; }
        [Required]
        [Column("apell1Ninno")]
        [StringLength(15)]
        public string Apell1Ninno { get; set; }
        [Required]
        [Column("apell2Ninno")]
        [StringLength(15)]
        public string Apell2Ninno { get; set; }
        [Column("fechaNacimiento", TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [Column("direccionNinno")]
        [StringLength(50)]
        public string DireccionNinno { get; set; }
        [Column("idGenero")]
        public int IdGenero { get; set; }

        [ForeignKey(nameof(IdGenero))]
        [InverseProperty(nameof(Genero.Ninnos))]
        public virtual Genero IdGeneroNavigation { get; set; }
        [InverseProperty("IdNinnoNavigation")]
        public virtual ICollection<Asistencia> Asistencia { get; set; }
        [InverseProperty(nameof(Matricula.IdNinnoNavigation))]
        public virtual ICollection<Matricula> Matriculas { get; set; }
        [InverseProperty(nameof(NinnoAlergiaIngrediente.IdNinnoNavigation))]
        public virtual ICollection<NinnoAlergiaIngrediente> NinnoAlergiaIngredientes { get; set; }
        [InverseProperty(nameof(NinnoEncargado.IdNinnoNavigation))]
        public virtual ICollection<NinnoEncargado> NinnoEncargados { get; set; }
        [InverseProperty(nameof(NinnoMenu.IdNinnoNavigation))]
        public virtual ICollection<NinnoMenu> NinnoMenus { get; set; }
        [InverseProperty(nameof(RegistroBaja.IdNinnoNavigation))]
        public virtual ICollection<RegistroBaja> RegistroBajas { get; set; }
        [InverseProperty(nameof(UsoComedore.IdNinnoNavigation))]
        public virtual ICollection<UsoComedore> UsoComedores { get; set; }
    }
}
