using Microsoft.EntityFrameworkCore;
using SolvefyTask.Models;
using System.Collections.Generic;

namespace SolvefyTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Equipment> Equipments => Set<Equipment>();
        public DbSet<Checkout> Checkouts => Set<Checkout>();

        public DbSet<CheckIn> CheckIns => Set<CheckIn>();

        public DbSet<ApprovalRequest> ApprovalRequests =>Set<ApprovalRequest>();

        public DbSet<MyEquipment> MyEquipments { get; set; }
        public DbSet<DamageReport> DamageReports { get; set; }
        public DbSet<EquipmentHistory> EquipmentHistories { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CheckIn>()
                .HasOne(c => c.Equipment)
                .WithMany()
                .HasForeignKey(c => c.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApprovalRequest>()
                .HasOne(a => a.Equipment)
                .WithMany()
                .HasForeignKey(a => a.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MyEquipment>()
                .HasOne(x => x.Equipment)
                .WithMany()
                .HasForeignKey(x => x.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MyEquipment>()
                .Property(x => x.AssignedTo)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<MyEquipment>()
                .Property(x => x.Status)
                .HasMaxLength(30)
                .IsRequired();
        }

    }
}
