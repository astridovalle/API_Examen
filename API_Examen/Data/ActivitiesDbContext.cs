using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_Examen
{
    public partial class ActivitiesDbContext : DbContext
    {
        public ActivitiesDbContext()
        {
        }

        public ActivitiesDbContext(DbContextOptions<ActivitiesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<Property> Properties { get; set; } = null!;
        public virtual DbSet<Survey> Surveys { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_buffercache")
                .HasPostgresExtension("pg_stat_statements");

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("activity");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.Property(e => e.Schedule)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("schedule");

                entity.Property(e => e.Status)
                    .HasMaxLength(35)
                    .HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_property");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("property");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DisabeledAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("disabeled_at");

                entity.Property(e => e.Status)
                    .HasMaxLength(35)
                    .HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("survey");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.Property(e => e.Answers)
                    .HasColumnType("json")
                    .HasColumnName("answers");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Surveys)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_activity");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
