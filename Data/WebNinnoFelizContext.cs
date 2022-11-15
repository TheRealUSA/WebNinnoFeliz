using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebNinnoFeliz.Models;

#nullable disable

namespace WebNinnoFeliz.Data
{
    public partial class WebNinnoFelizContext : DbContext
    {
        public WebNinnoFelizContext()
        {
        }

        public WebNinnoFelizContext(DbContextOptions<WebNinnoFelizContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AbonadorCargoMensuale> AbonadorCargoMensuales { get; set; }
        public virtual DbSet<Abonadore> Abonadores { get; set; }
        public virtual DbSet<Alergia> Alergias { get; set; }
        public virtual DbSet<Asistencia> Asistencias { get; set; }
        public virtual DbSet<CargoMensuale> CargoMensuales { get; set; }
        public virtual DbSet<Encargado> Encargados { get; set; }
        public virtual DbSet<EncargadoMatricula> EncargadoMatriculas { get; set; }
        public virtual DbSet<EncargadoRegistroDeBaja> EncargadoRegistroDeBajas { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<Ingrediente> Ingredientes { get; set; }
        public virtual DbSet<Matricula> Matriculas { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuPlato> MenuPlatos { get; set; }
        public virtual DbSet<Mese> Meses { get; set; }
        public virtual DbSet<NaiPlato> NaiPlatos { get; set; }
        public virtual DbSet<Ninno> Ninnos { get; set; }
        public virtual DbSet<NinnoAlergiaIngrediente> NinnoAlergiaIngredientes { get; set; }
        public virtual DbSet<NinnoEncargado> NinnoEncargados { get; set; }
        public virtual DbSet<NinnoMenu> NinnoMenus { get; set; }
        public virtual DbSet<Parentezco> Parentezcos { get; set; }
        public virtual DbSet<Plato> Platos { get; set; }
        public virtual DbSet<PlatoIngrediente> PlatoIngredientes { get; set; }
        public virtual DbSet<RegistroBaja> RegistroBajas { get; set; }
        public virtual DbSet<TipoAlergia> TipoAlergias { get; set; }
        public virtual DbSet<UsoComedore> UsoComedores { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                optionsBuilder.UseSqlServer("server=DESKTOP-HPDH4AT; database=NinnoFeliz; trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AbonadorCargoMensuale>(entity =>
            {
                entity.HasKey(e => e.IdAbonadorCargoMensual)
                    .HasName("PK_Abonador_CargoMensual");

                entity.HasOne(d => d.IdAbonadorNavigation)
                    .WithMany(p => p.AbonadorCargoMensuales)
                    .HasForeignKey(d => d.IdAbonador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonador_CargoMensual_Abonador");

                entity.HasOne(d => d.IdCargoNavigation)
                    .WithMany(p => p.AbonadorCargoMensuales)
                    .HasForeignKey(d => d.IdCargo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonador_CargoMensual_Cargo");
            });

            modelBuilder.Entity<Abonadore>(entity =>
            {
                entity.HasKey(e => e.IdAbonador)
                    .HasName("PK_Abonador");

                entity.HasOne(d => d.IdEncargadoNavigation)
                    .WithMany(p => p.Abonadores)
                    .HasForeignKey(d => d.IdEncargado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AbonadorEncargado");
            });

            modelBuilder.Entity<Alergia>(entity =>
            {
                entity.HasKey(e => e.IdAlergia)
                    .HasName("PK_Alergia");

                entity.Property(e => e.NombreAlergia).IsUnicode(false);

                entity.HasOne(d => d.IdTipoAlergiaNavigation)
                    .WithMany(p => p.Alergia)
                    .HasForeignKey(d => d.IdTipoAlergia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoAlergia");
            });

            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.HasKey(e => e.IdAsistencia)
                    .HasName("PK_Asistencia");

                entity.HasOne(d => d.IdNinnoNavigation)
                    .WithMany(p => p.Asistencia)
                    .HasForeignKey(d => d.IdNinno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AsistenciaNinno");
            });

            modelBuilder.Entity<CargoMensuale>(entity =>
            {
                entity.HasKey(e => e.IdCargo)
                    .HasName("PK_CargoMensual");

                entity.Property(e => e.CargoMensual).IsUnicode(false);

                entity.HasOne(d => d.IdUsoComedorNavigation)
                    .WithMany(p => p.CargoMensuales)
                    .HasForeignKey(d => d.IdUsoComedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CargoMensual_UsoComedor");
            });

            modelBuilder.Entity<Encargado>(entity =>
            {
                entity.HasKey(e => e.IdEncargado)
                    .HasName("PK_Encargado");

                entity.Property(e => e.Apell1Encargado).IsUnicode(false);

                entity.Property(e => e.Apell2Encargado).IsUnicode(false);

                entity.Property(e => e.DirecciónEncargado).IsUnicode(false);

                entity.Property(e => e.IdentificacionEncargado).IsUnicode(false);

                entity.Property(e => e.NombreEncargado).IsUnicode(false);

                entity.Property(e => e.TelefonoEncargado).IsUnicode(false);

                entity.HasOne(d => d.IdParentezcoNavigation)
                    .WithMany(p => p.Encargados)
                    .HasForeignKey(d => d.IdParentezco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EncargadoParentezco");
            });

            modelBuilder.Entity<EncargadoMatricula>(entity =>
            {
                entity.HasKey(e => e.IdEncargadoMatricula)
                    .HasName("PK_Encargado_Matricula");

                entity.HasOne(d => d.IdEncargadoNavigation)
                    .WithMany(p => p.EncargadoMatriculas)
                    .HasForeignKey(d => d.IdEncargado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Encargado_Matricula_Encarg");

                entity.HasOne(d => d.NumeroMatriculaNavigation)
                    .WithMany(p => p.EncargadoMatriculas)
                    .HasForeignKey(d => d.NumeroMatricula)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Encargado_Matricula_Matr");
            });

            modelBuilder.Entity<EncargadoRegistroDeBaja>(entity =>
            {
                entity.HasKey(e => e.IdEncargadoRegistroBaja)
                    .HasName("PK_Encargado_RegistroDeBaja");

                entity.HasOne(d => d.IdEncargadoNavigation)
                    .WithMany(p => p.EncargadoRegistroDeBajas)
                    .HasForeignKey(d => d.IdEncargado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Encargado_RegistroDeBaja_Encargado");

                entity.HasOne(d => d.IdRegistroBajaNavigation)
                    .WithMany(p => p.EncargadoRegistroDeBajas)
                    .HasForeignKey(d => d.IdRegistroBaja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Encargado_RegistroDeBaja_Regis");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.Property(e => e.DetalleGen).IsUnicode(false);
            });

            modelBuilder.Entity<Ingrediente>(entity =>
            {
                entity.HasKey(e => e.IdIngrediente)
                    .HasName("PK_Ingrediente");

                entity.Property(e => e.NombreIngrediente).IsUnicode(false);
            });

            modelBuilder.Entity<Matricula>(entity =>
            {
                entity.HasKey(e => e.NumeroMatricula)
                    .HasName("PK_Matricula");

                entity.HasOne(d => d.IdNinnoNavigation)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(d => d.IdNinno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatriculaNinno");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdNumeroMenu)
                    .HasName("PK_Menu");

                entity.Property(e => e.NombreMenu).IsUnicode(false);
            });

            modelBuilder.Entity<MenuPlato>(entity =>
            {
                entity.HasKey(e => e.IdnumeroMenuPlato)
                    .HasName("PK_Menu_Plato");

                entity.HasOne(d => d.IdNumeroMenuNavigation)
                    .WithMany(p => p.MenuPlatos)
                    .HasForeignKey(d => d.IdNumeroMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuM");

                entity.HasOne(d => d.IdPlatoNavigation)
                    .WithMany(p => p.MenuPlatos)
                    .HasForeignKey(d => d.IdPlato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlatoM");
            });

            modelBuilder.Entity<Mese>(entity =>
            {
                entity.HasKey(e => e.IdMes)
                    .HasName("PK_Mes");

                entity.Property(e => e.NombreMes).IsUnicode(false);
            });

            modelBuilder.Entity<NaiPlato>(entity =>
            {
                entity.HasKey(e => e.IdNaiplato)
                    .HasName("PK_NAIPlato");

                entity.HasOne(d => d.IdNinnoAlergiaIngredienteNavigation)
                    .WithMany(p => p.NaiPlatos)
                    .HasForeignKey(d => d.IdNinnoAlergiaIngrediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NinnoAlergiaIngredienteN");

                entity.HasOne(d => d.IdPlatoNavigation)
                    .WithMany(p => p.NaiPlatos)
                    .HasForeignKey(d => d.IdPlato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlatoN");
            });

            modelBuilder.Entity<Ninno>(entity =>
            {
                entity.HasKey(e => e.IdNinno)
                    .HasName("PK_Ninno");

                entity.Property(e => e.Apell1Ninno).IsUnicode(false);

                entity.Property(e => e.Apell2Ninno).IsUnicode(false);

                entity.Property(e => e.DireccionNinno).IsUnicode(false);

                entity.Property(e => e.IdentificacionNinno).IsUnicode(false);

                entity.Property(e => e.NombreNinno).IsUnicode(false);

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.Ninnos)
                    .HasForeignKey(d => d.IdGenero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NinnoGenero");
            });

            modelBuilder.Entity<NinnoAlergiaIngrediente>(entity =>
            {
                entity.HasKey(e => e.IdNinnoAlergiaIngrediente)
                    .HasName("PK_NinnoAlergiaIngrediente");

                entity.HasOne(d => d.IdAlergiaNavigation)
                    .WithMany(p => p.NinnoAlergiaIngredientes)
                    .HasForeignKey(d => d.IdAlergia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlergiaN");

                entity.HasOne(d => d.IdIngredienteNavigation)
                    .WithMany(p => p.NinnoAlergiaIngredientes)
                    .HasForeignKey(d => d.IdIngrediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IngredienteN");

                entity.HasOne(d => d.IdNinnoNavigation)
                    .WithMany(p => p.NinnoAlergiaIngredientes)
                    .HasForeignKey(d => d.IdNinno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NinnoN");
            });

            modelBuilder.Entity<NinnoEncargado>(entity =>
            {
                entity.HasKey(e => e.IdNinnoEncargado)
                    .HasName("PK_Ninno_Encargado");

                entity.HasOne(d => d.IdEncargadoNavigation)
                    .WithMany(p => p.NinnoEncargados)
                    .HasForeignKey(d => d.IdEncargado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ninno_Encargados_Encargado");

                entity.HasOne(d => d.IdNinnoNavigation)
                    .WithMany(p => p.NinnoEncargados)
                    .HasForeignKey(d => d.IdNinno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ninno_Encargados_Ninno");
            });

            modelBuilder.Entity<NinnoMenu>(entity =>
            {
                entity.HasKey(e => e.IdNinnoMenu)
                    .HasName("PK_NinnoMenu");

                entity.HasOne(d => d.IdNinnoNavigation)
                    .WithMany(p => p.NinnoMenus)
                    .HasForeignKey(d => d.IdNinno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NinnoNI");

                entity.HasOne(d => d.IdNumeroMenuNavigation)
                    .WithMany(p => p.NinnoMenus)
                    .HasForeignKey(d => d.IdNumeroMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuNI");
            });

            modelBuilder.Entity<Parentezco>(entity =>
            {
                entity.HasKey(e => e.IdParentezco)
                    .HasName("PK_Parentezco");

                entity.Property(e => e.DetallePar).IsUnicode(false);
            });

            modelBuilder.Entity<Plato>(entity =>
            {
                entity.HasKey(e => e.IdPlato)
                    .HasName("PK_Plato");

                entity.Property(e => e.NombrePlato).IsUnicode(false);

                entity.Property(e => e.PrecioPlato).IsUnicode(false);
            });

            modelBuilder.Entity<PlatoIngrediente>(entity =>
            {
                entity.HasKey(e => e.IdPlatoIngrediente)
                    .HasName("PK_Plato_Ingrediente");

                entity.HasOne(d => d.IdIngredienteNavigation)
                    .WithMany(p => p.PlatoIngredientes)
                    .HasForeignKey(d => d.IdIngrediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IngredientePI");

                entity.HasOne(d => d.IdPlatoNavigation)
                    .WithMany(p => p.PlatoIngredientes)
                    .HasForeignKey(d => d.IdPlato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlatoPI");
            });

            modelBuilder.Entity<RegistroBaja>(entity =>
            {
                entity.HasKey(e => e.IdRegistroBaja)
                    .HasName("PK_RegistroBaja");

                entity.HasOne(d => d.IdNinnoNavigation)
                    .WithMany(p => p.RegistroBajas)
                    .HasForeignKey(d => d.IdNinno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegistroBajasNinno");
            });

            modelBuilder.Entity<TipoAlergia>(entity =>
            {
                entity.HasKey(e => e.IdTipoAlergia)
                    .HasName("PK_TipoAlergia");

                entity.Property(e => e.NombreTipoAlergia).IsUnicode(false);
            });

            modelBuilder.Entity<UsoComedore>(entity =>
            {
                entity.HasKey(e => e.IdUsoComedor)
                    .HasName("PK_UsoComedor");

                entity.HasOne(d => d.IdMesNavigation)
                    .WithMany(p => p.UsoComedores)
                    .HasForeignKey(d => d.IdMes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonador_UsoComedoresMes");

                entity.HasOne(d => d.IdNinnoNavigation)
                    .WithMany(p => p.UsoComedores)
                    .HasForeignKey(d => d.IdNinno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonador_UsoComedoresNinno");
            });

        }

    }
}
