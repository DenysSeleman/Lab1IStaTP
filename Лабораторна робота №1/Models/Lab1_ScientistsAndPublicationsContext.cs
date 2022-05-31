using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lab1_Scientists_And_Publications
{
    public partial class Lab1_ScientistsAndPublicationsContext : DbContext
    {
        public Lab1_ScientistsAndPublicationsContext()
        {
        }

        public Lab1_ScientistsAndPublicationsContext(DbContextOptions<Lab1_ScientistsAndPublicationsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Edition> Editions { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Publication> Publications { get; set; } = null!;
        public virtual DbSet<Scientist> Scientists { get; set; } = null!;
        public virtual DbSet<ScientistEdition> ScientistEditions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-6TV4M9M; Database=Lab1_ScientistsAndPublications; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.FacultyId).HasColumnName("FacultyID");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_Faculty");
            });

            modelBuilder.Entity<Edition>(entity =>
            {
                entity.ToTable("Edition");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.PublicationId).HasColumnName("PublicationID");

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.HasOne(d => d.Publication)
                    .WithMany(p => p.Editions)
                    .HasForeignKey(d => d.PublicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Edition_Publication");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("Faculty");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateOfFoundation).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.ToTable("Publication");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.Topic).HasMaxLength(200);
            });

            modelBuilder.Entity<Scientist>(entity =>
            {
                entity.ToTable("Scientist");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.FullName).HasMaxLength(200);

                entity.Property(e => e.ScienceDegree).HasMaxLength(200);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Scientists)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Scientist_Department");
            });

            modelBuilder.Entity<ScientistEdition>(entity =>
            {
                entity.ToTable("ScientistEdition");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EditionId).HasColumnName("EditionID");

                entity.Property(e => e.ScientistId).HasColumnName("ScientistID");

                entity.HasOne(d => d.Edition)
                    .WithMany(p => p.ScientistEditions)
                    .HasForeignKey(d => d.EditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScientistEdition_Edition");

                entity.HasOne(d => d.Scientist)
                    .WithMany(p => p.ScientistEditions)
                    .HasForeignKey(d => d.ScientistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScientistEdition_Scientist");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
