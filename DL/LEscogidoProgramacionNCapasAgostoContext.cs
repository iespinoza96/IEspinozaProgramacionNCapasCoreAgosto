using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class LEscogidoProgramacionNCapasAgostoContext : DbContext
    {
        public LEscogidoProgramacionNCapasAgostoContext()
        {
        }

        public LEscogidoProgramacionNCapasAgostoContext(DbContextOptions<LEscogidoProgramacionNCapasAgostoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; } = null!;
        public virtual DbSet<Direccion> Direccions { get; set; } = null!;
        public virtual DbSet<Grupo> Grupos { get; set; } = null!;
        public virtual DbSet<Horario> Horarios { get; set; } = null!;
        public virtual DbSet<Plantel> Plantels { get; set; } = null!;
        public virtual DbSet<Semestre> Semestres { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-VA31VKK7; Database= LEscogidoProgramacionNCapasAgosto; Trusted_Connection=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno)
                    .HasName("PK__Alumno__460B4740900FEE4E");

                entity.ToTable("Alumno");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdSemestreNavigation)
                    .WithMany(p => p.Alumnos)
                    .HasForeignKey(d => d.IdSemestre)
                    .HasConstraintName("FK__Alumno__IdSemest__24927208");
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasKey(e => e.IdDireccion)
                    .HasName("PK__Direccio__1F8E0C7688807023");

                entity.ToTable("Direccion");

                entity.Property(e => e.Calle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroExterior)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroInterior)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Direccions)
                    .HasForeignKey(d => d.IdAlumno)
                    .HasConstraintName("FK__Direccion__IdAlu__36B12243");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => e.IdGrupo)
                    .HasName("PK__Grupo__303F6FD95F3A9309");

                entity.ToTable("Grupo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPlantelNavigation)
                    .WithMany(p => p.Grupos)
                    .HasForeignKey(d => d.IdPlantel)
                    .HasConstraintName("FK__Grupo__IdPlantel__2A4B4B5E");
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => e.IdHorario)
                    .HasName("PK__Horario__1539229B186B0857");

                entity.ToTable("Horario");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Horarios)
                    .HasForeignKey(d => d.IdGrupo)
                    .HasConstraintName("FK__Horario__IdGrupo__2D27B809");
            });

            modelBuilder.Entity<Plantel>(entity =>
            {
                entity.HasKey(e => e.IdPlantel)
                    .HasName("PK__Plantel__485FDCFE8A4DA16E");

                entity.ToTable("Plantel");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Semestre>(entity =>
            {
                entity.HasKey(e => e.IdSemestre)
                    .HasName("PK__Semestre__BD1FD7F834460859");

                entity.ToTable("Semestre");

                entity.Property(e => e.IdSemestre).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
