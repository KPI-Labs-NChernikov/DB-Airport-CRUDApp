using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public partial class AirportContext : DbContext
    {
        public AirportContext(DbContextOptions<AirportContext> options)
            : base(options)
        {        }

        public virtual DbSet<Baggage> Baggages { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Passenger> Passengers { get; set; } = null!;
        public virtual DbSet<Plane> Planes { get; set; } = null!;
        public virtual DbSet<Terminal> Terminals { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Visa> Visas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Baggage>(entity =>
            {
                entity.ToTable("Baggage");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Volume).HasColumnType("decimal(18, 9)");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 9)");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Baggages)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK__Baggage__TicketI__3B75D760");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("Flight");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.To).HasMaxLength(100);

                entity.HasOne(d => d.Plane)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.PlaneId)
                    .HasConstraintName("FK__Flight__PlaneId__2A4B4B5E");

                entity.HasOne(d => d.Terminal)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.TerminalId)
                    .HasConstraintName("FK__Flight__Terminal__2B3F6F97");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.ToTable("Passenger");

                entity.HasIndex(e => e.PassportCode, "UQ__Passenge__1ADCE623AC8F5C8C")
                    .IsUnique();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PassportCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Patronymic).HasMaxLength(50);
            });

            modelBuilder.Entity<Plane>(entity =>
            {
                entity.ToTable("Plane");

                entity.HasIndex(e => e.RegistrationNumber, "UQ__Plane__E8864602FE666F98")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Terminal>(entity =>
            {
                entity.ToTable("Terminal");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.HasIndex(e => e.Code, "UQ__Ticket__A25C5AA7F34E175A")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Type).HasMaxLength(30);

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.FlightId)
                    .HasConstraintName("FK__Ticket__FlightId__38996AB5");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.PassengerId)
                    .HasConstraintName("FK__Ticket__Passenge__37A5467C");
            });

            modelBuilder.Entity<Visa>(entity =>
            {
                entity.ToTable("Visa");

                entity.HasIndex(e => e.Code, "UQ__Visa__A25C5AA75445882F")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Expiry).HasColumnType("date");

                entity.Property(e => e.Issue).HasColumnType("date");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Visas)
                    .HasForeignKey(d => d.PassengerId)
                    .HasConstraintName("FK__Visa__PassengerI__33D4B598");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
