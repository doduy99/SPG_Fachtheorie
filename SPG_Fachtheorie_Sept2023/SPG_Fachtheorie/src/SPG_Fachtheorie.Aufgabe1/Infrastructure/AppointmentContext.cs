using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    public class AppointmentContext : DbContext
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<AppointmentState> AppointmentStates => Set<AppointmentState>();
        public DbSet<ConfirmedAppointmentState> ConfirmedAppointmentStates => Set<ConfirmedAppointmentState>();
        public DbSet<DeletedAppointmentState> DeletedAppointmentStates => Set<DeletedAppointmentState>();
        public AppointmentContext(DbContextOptions options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Patient>().OwnsOne(p => p.Address);
            modelBuilder.Entity<AppointmentState>().HasDiscriminator(a => a.Type);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.AppointmentStateNavigation)
                .WithOne(b => b.AppointmentNavigation)
                .HasForeignKey<Appointment>(a => a.AppointmentStateId);                
        }

    }
}