using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClashRoyaleAplication.DBModels
{
    public partial class clashroyaleContext : DbContext
    {
        public clashroyaleContext()
        {
        }

        public clashroyaleContext(DbContextOptions<clashroyaleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Batalla> Batallas { get; set; }
        public virtual DbSet<Cartum> Carta { get; set; }
        public virtual DbSet<Clan> Clans { get; set; }
        public virtual DbSet<Desafio> Desafios { get; set; }
        public virtual DbSet<Dispone> Dispones { get; set; }
        public virtual DbSet<Donar> Donars { get; set; }
        public virtual DbSet<Estructura> Estructuras { get; set; }
        public virtual DbSet<GuerradeClane> GuerradeClanes { get; set; }
        public virtual DbSet<Hechizo> Hechizos { get; set; }
        public virtual DbSet<Jugador> Jugadors { get; set; }
        public virtual DbSet<Lucha> Luchas { get; set; }
        public virtual DbSet<Miembro> Miembros { get; set; }
        public virtual DbSet<Participa> Participas { get; set; }
        public virtual DbSet<ParticipaEn> ParticipaEns { get; set; }
        public virtual DbSet<Pertenece> Perteneces { get; set; }
        public virtual DbSet<Tropa> Tropas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-5FBAI0E;Initial Catalog=clashroyale; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Batalla>(entity =>
            {
                entity.HasKey(e => new { e.IdJugador1, e.IdJugador2, e.IdBatalla });

                entity.ToTable("Batalla");

                entity.Property(e => e.Duracion).HasColumnType("datetime");

                entity.HasOne(d => d.IdJugador1Navigation)
                    .WithMany(p => p.BatallaIdJugador1Navigations)
                    .HasForeignKey(d => d.IdJugador1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Batalla_Jugador");

                entity.HasOne(d => d.IdJugador2Navigation)
                    .WithMany(p => p.BatallaIdJugador2Navigations)
                    .HasForeignKey(d => d.IdJugador2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Batalla_Jugador1");
            });

            modelBuilder.Entity<Cartum>(entity =>
            {
                entity.HasKey(e => e.IdCarta)
                    .HasName("PK_Carta_1");

                entity.Property(e => e.IdCarta).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Calidad)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Clan>(entity =>
            {
                entity.HasKey(e => e.IdClan)
                    .HasName("PK_Clan_1");

                entity.ToTable("Clan");

                entity.Property(e => e.IdClan).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Trofeos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Desafio>(entity =>
            {
                entity.HasKey(e => e.IdDesafio);

                entity.ToTable("Desafio");

                entity.Property(e => e.IdDesafio).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoDeDuracion).HasColumnType("datetime");
            });

            modelBuilder.Entity<Dispone>(entity =>
            {
                entity.HasKey(e => new { e.IdJugador, e.IdCarta })
                    .HasName("PK_Dispone_1");

                entity.ToTable("Dispone");

                entity.HasOne(d => d.IdCartaNavigation)
                    .WithMany(p => p.Dispones)
                    .HasForeignKey(d => d.IdCarta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dispone_Carta1");

                entity.HasOne(d => d.IdJugadorNavigation)
                    .WithMany(p => p.Dispones)
                    .HasForeignKey(d => d.IdJugador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dispone_Jugador1");
            });

            modelBuilder.Entity<Donar>(entity =>
            {
                entity.HasKey(e => new { e.IdJugador, e.IdMiembro, e.IdCarta });

                entity.ToTable("Donar");
            });

            modelBuilder.Entity<Estructura>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Estructura");
            });

            modelBuilder.Entity<GuerradeClane>(entity =>
            {
                entity.HasKey(e => e.IdGuerraClanes);

                entity.Property(e => e.IdGuerraClanes).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Hechizo>(entity =>
            {
                entity.HasKey(e => e.IdHechizo)
                    .HasName("PK_Hechizos_1");

                entity.Property(e => e.IdHechizo).ValueGeneratedNever();

                entity.Property(e => e.Duracion).HasColumnType("datetime");
            });

            modelBuilder.Entity<Jugador>(entity =>
            {
                entity.HasKey(e => e.IdJugador);

                entity.ToTable("Jugador");

                entity.Property(e => e.IdJugador).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.CartaPreferidaActualNavigation)
                    .WithMany(p => p.Jugadors)
                    .HasForeignKey(d => d.CartaPreferidaActual)
                    .HasConstraintName("FK_Jugador_Carta");
            });

            modelBuilder.Entity<Lucha>(entity =>
            {
                entity.HasKey(e => new { e.IdJugador1, e.IdJugador2 })
                    .HasName("PK_Lucha_1");

                entity.ToTable("Lucha");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.HasOne(d => d.IdJugador1Navigation)
                    .WithMany(p => p.LuchaIdJugador1Navigations)
                    .HasForeignKey(d => d.IdJugador1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lucha_Jugador");

                entity.HasOne(d => d.IdJugador2Navigation)
                    .WithMany(p => p.LuchaIdJugador2Navigations)
                    .HasForeignKey(d => d.IdJugador2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lucha_Jugador1");
            });

            modelBuilder.Entity<Miembro>(entity =>
            {
                entity.HasKey(e => new { e.IdJugador, e.IdMiembro, e.IdClan })
                    .HasName("PK_Miembro_1");

                entity.ToTable("Miembro");

                entity.Property(e => e.Cargo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClanNavigation)
                    .WithMany(p => p.Miembros)
                    .HasForeignKey(d => d.IdClan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Miembro_Clan");

                entity.HasOne(d => d.IdJugadorNavigation)
                    .WithMany(p => p.Miembros)
                    .HasForeignKey(d => d.IdJugador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Miembro_Jugador");
            });

            modelBuilder.Entity<Participa>(entity =>
            {
                entity.HasKey(e => new { e.IdJugador, e.IdDesafio })
                    .HasName("PK_Participa_1");

                entity.ToTable("Participa");

                entity.Property(e => e.FechadeComienzo).HasColumnType("datetime");

                entity.HasOne(d => d.IdDesafioNavigation)
                    .WithMany(p => p.Participas)
                    .HasForeignKey(d => d.IdDesafio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participa_Desafio");

                entity.HasOne(d => d.IdJugadorNavigation)
                    .WithMany(p => p.Participas)
                    .HasForeignKey(d => d.IdJugador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participa_Jugador");
            });

            modelBuilder.Entity<ParticipaEn>(entity =>
            {
                entity.HasKey(e => new { e.IdClan, e.IdGuerraClanes })
                    .HasName("PK_ParticipaEn_1");

                entity.ToTable("ParticipaEn");

                entity.Property(e => e.FechaComienzo).HasColumnType("datetime");

                entity.HasOne(d => d.IdClanNavigation)
                    .WithMany(p => p.ParticipaEns)
                    .HasForeignKey(d => d.IdClan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParticipaEn_Clan");

                entity.HasOne(d => d.IdGuerraClanesNavigation)
                    .WithMany(p => p.ParticipaEns)
                    .HasForeignKey(d => d.IdGuerraClanes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParticipaEn_GuerradeClanes");
            });

            modelBuilder.Entity<Pertenece>(entity =>
            {
                entity.HasKey(e => new { e.IdJugador, e.IdClan })
                    .HasName("PK_Pertenece_1");

                entity.ToTable("Pertenece");

                entity.HasOne(d => d.IdClanNavigation)
                    .WithMany(p => p.Perteneces)
                    .HasForeignKey(d => d.IdClan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pertenece_Clan");

                entity.HasOne(d => d.IdJugadorNavigation)
                    .WithMany(p => p.Perteneces)
                    .HasForeignKey(d => d.IdJugador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pertenece_Jugador");
            });

            modelBuilder.Entity<Tropa>(entity =>
            {
                entity.HasKey(e => e.IdTropa)
                    .HasName("PK_Tropa_1");

                entity.ToTable("Tropa");

                entity.Property(e => e.IdTropa).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
