using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ScaffoldSample
{
    public partial class ProCSharpMenus2Context : DbContext
    {
        public ProCSharpMenus2Context()
        {
        }

        public ProCSharpMenus2Context(DbContextOptions<ProCSharpMenus2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuCard> MenuCards { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=ProCSharpMenus2;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menus", "mc");

                entity.HasIndex(e => e.MenuCardId, "IX_Menus_MenuCardId");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MenuCard)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.MenuCardId);
            });

            modelBuilder.Entity<MenuCard>(entity =>
            {
                entity.ToTable("MenuCards", "mc");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurants", "mc");

                entity.Property(e => e.Id).HasColumnName("_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
