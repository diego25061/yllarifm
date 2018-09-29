using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YllariFM.Models.DB
{
    public partial class YllariFMContext : DbContext
    {
        public virtual DbSet<Agencia> Agencia { get; set; }
        public virtual DbSet<Biblia> Biblia { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Orden> Orden { get; set; }
        public virtual DbSet<Pasajero> Pasajero { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=serdc.database.windows.net;Initial Catalog=YllariFM;User=serdc;Password=123456Upc");
            }
        }
        */


        public YllariFMContext()
        {
        }

        public YllariFMContext(DbContextOptions<YllariFMContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agencia>(entity =>
            {
                entity.HasKey(e => e.IdAgencia);

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoContacto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoExtra)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Biblia>(entity =>
            {
                entity.HasKey(e => e.IdBiblia);

                entity.HasIndex(e => new { e.Anho, e.Mes })
                    .HasName("IX_Biblia")
                    .IsUnique();
            });

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.IdCiudad);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.HasKey(e => e.IdFile);

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(750)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.HasOne(d => d.IdAgenciaNavigation)
                    .WithMany(p => p.File)
                    .HasForeignKey(d => d.IdAgencia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_File_Agencia");

                entity.HasOne(d => d.IdBibliaNavigation)
                    .WithMany(p => p.File)
                    .HasForeignKey(d => d.IdBiblia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_File_Biblia");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.IdHotel);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasKey(e => e.IdOrden);
            });

            modelBuilder.Entity<Pasajero>(entity =>
            {
                entity.HasKey(e => e.IdPasajero);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.TipoProveedor)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio);

                entity.Property(e => e.Alm)
                    .HasColumnName("ALM")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NombrePasajero)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(750)
                    .IsUnicode(false);

                entity.Property(e => e.Tc)
                    .HasColumnName("TC")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoServicio)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Transp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tren)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vr)
                    .HasColumnName("VR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vuelo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdFileNavigation)
                    .WithMany(p => p.Servicio)
                    .HasForeignKey(d => d.IdFile)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servicio_File");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Servicio)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK_Servicio_Proveedor");
            });
        }
    }
}
