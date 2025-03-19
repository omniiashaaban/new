using laboratory.DAL.Models;
using Microsoft.EntityFrameworkCore;



namespace laboratory.DAL.Data.context
{
    public class LaboratoryDbContext : DbContext
    {
        public LaboratoryDbContext(DbContextOptions<LaboratoryDbContext> options) : base(options)
        {

        }
        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<User> Users { get; set; }

     
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // تعريف العلاقة Many-to-Many بين Experiments و Chemicals
                modelBuilder.Entity<Experiment>()
                    .HasMany(e => e.Chemicals)
                    .WithMany(c => c.Experiments)
                    .UsingEntity(j => j.ToTable("ExperimentChemicals"));

                // تعريف العلاقة Many-to-Many بين Experiments و Equipments
                modelBuilder.Entity<Experiment>()
                    .HasMany(e => e.Equipments)
                    .WithMany(eq => eq.Experiments)
                    .UsingEntity(j => j.ToTable("ExperimentEquipments"));

                // تعريف العلاقة One-to-Many بين Users و Experiments (المشرفين)
                modelBuilder.Entity<Experiment>()
                    .HasOne(e => e.Supervisor)
                    .WithMany(u => u.SupervisedExperiments)
                    .HasForeignKey(e => e.SupervisorID)
                    .OnDelete(DeleteBehavior.Restrict);
            }
           }
}

