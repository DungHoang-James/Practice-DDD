using Ddd.Logic.Atms;
using Ddd.Logic.Management;
using Ddd.Logic.SnackMachines;
using Microsoft.EntityFrameworkCore;

namespace Ddd.Logic
{
    public class DddDbContext : DbContext
    {
        public DbSet<SnackMachine> SnackMachines { get; set; }
        public DbSet<Snack> Snacks { get; set; }
        public DbSet<Atm> Atms { get; set; }
        public DbSet<HeadOffice> HeadOffices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Server=.;Database=.;User Id=.;Password=.";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SnackMachine>(sm =>
            {
                sm.ToTable("SnackMachine");
                sm.HasKey(snackMachine => snackMachine.Id);
                sm.OwnsOne(snackMachine => snackMachine.MoneyInside, mi =>
                {
                    mi.Property(money => money.OneCentCount).HasColumnName("OneCentCount");
                    mi.Property(money => money.TenCentCount).HasColumnName("TenCentCount");
                    mi.Property(money => money.QuarterCount).HasColumnName("QuarterCount");
                    mi.Property(money => money.OneDollarCount).HasColumnName("OneDollarCount");
                    mi.Property(money => money.FiveDollarCount).HasColumnName("FiveDollarCount");
                    mi.Property(money => money.TwentyDollarCount).HasColumnName("TwentyDollarCount");
                });
                sm.Ignore(snackMachine => snackMachine.MoneyInTransaction);
            });

            modelBuilder.Entity<Snack>(s =>
            {
                s.ToTable("Snack");
                s.HasKey(snack => snack.Id);
            });

            modelBuilder.Entity<Slot>(s =>
            {
                s.HasKey(slot => slot.Id);
                s.OwnsOne(slot => slot.SnackPile, sp =>
                {
                    sp.Property(snackPile => snackPile.Quantity).HasColumnName("Quantity");
                    sp.Property(snackPile => snackPile.Price).HasColumnName("Price");
                });
            });

            modelBuilder.Entity<Atm>(a =>
            {
                a.ToTable("Atm");
                a.OwnsOne(atm => atm.MoneyInSide, mi =>
                {
                    mi.Property(money => money.OneCentCount).HasColumnName("OneCentCount");
                    mi.Property(money => money.TenCentCount).HasColumnName("TenCentCount");
                    mi.Property(money => money.QuarterCount).HasColumnName("QuarterCount");
                    mi.Property(money => money.OneDollarCount).HasColumnName("OneDollarCount");
                    mi.Property(money => money.FiveDollarCount).HasColumnName("FiveDollarCount");
                    mi.Property(money => money.TwentyDollarCount).HasColumnName("TwentyDollarCount");
                });
            });

            modelBuilder.Entity<HeadOffice>(h =>
            {
                h.ToTable("HeadOffice");
                h.OwnsOne(head => head.Cash, c =>
                {
                    c.Property(money => money.OneCentCount).HasColumnName("OneCentCount");
                    c.Property(money => money.TenCentCount).HasColumnName("TenCentCount");
                    c.Property(money => money.QuarterCount).HasColumnName("QuarterCount");
                    c.Property(money => money.OneDollarCount).HasColumnName("OneDollarCount");
                    c.Property(money => money.FiveDollarCount).HasColumnName("FiveDollarCount");
                    c.Property(money => money.TwentyDollarCount).HasColumnName("TwentyDollarCount");
                });
            });
        }
    }
}
