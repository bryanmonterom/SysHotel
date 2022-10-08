using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysHotelv1.Models
{
    public class SysHotelDataContext : DbContext
    {
        public SysHotelDataContext() : base("name=SysHotelDataContext") { }

        public DbSet<BedType> BedType { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Clients> Clients {get;set;}
        public DbSet<Country> Countries { get; set; }
        public DbSet<PersonType> PricesPerPeople { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationDetails> ReservationDetails { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<Employee> Employee { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region PrimaryKeys
            //modelBuilder.Configurations.Add(new class anme)

            modelBuilder.Entity<ReservationDetails>().HasKey(p => new { p.Id }).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Reservation>().HasKey(p => new { p.Id }).Property(p=>p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Rooms>().HasKey(p => new { p.Id }).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Building>().HasKey(p => new { p.Id }).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Clients>().HasKey(p => new { p.Id }).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<City>().HasKey(p => new { p.Id }).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<BedType>().HasKey(p => new { p.Id }).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<BookingStatus>().HasKey(p => new { p.Id }).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<PersonType>().HasKey(p => new { p.Id }).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<RoomType>().HasKey(p => new { p.Id }).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Country>().HasKey(p => new { p.Id }).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Invoices>().HasKey(a => new { a.Id }).Property(b => b.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Employee>().HasKey(a => new { a.Id }).Property(b => b.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            #endregion
            

            #region ForeignKeys
            modelBuilder.Entity<City>().HasRequired<Country>(s => s.Country).WithMany(t => t.City).HasForeignKey<int>(s => s.CountryId);
            modelBuilder.Entity<ReservationDetails>().HasRequired<Reservation>(s => s.Reservation).WithMany(t => t.ReservationDetails).HasForeignKey<int>(s => s.ReservationId);
            modelBuilder.Entity<ReservationDetails>().HasRequired<Rooms>(x => x.Rooms).WithMany(a => a.ReservationDetails).HasForeignKey<int>(s=> s.RoomId);
            modelBuilder.Entity<Rooms>().HasRequired<RoomType>(X=> X.RoomType).WithMany(x=> x.Rooms).HasForeignKey<int>(s=>s.RoomTypeId);
            modelBuilder.Entity<Rooms>().HasRequired<BedType>(x => x.BedType).WithMany(x => x.Rooms).HasForeignKey<int>(s => s.BedTypeId);
            modelBuilder.Entity<Rooms>().HasRequired<Building>(x => x.Building).WithMany(x => x.Rooms).HasForeignKey<int>(s => s.BuildingId);  
            modelBuilder.Entity<Reservation>().HasRequired<BookingStatus>(x => x.BookingStatus).WithMany(x => x.Reservations).HasForeignKey<int>(s => s.BookingStatusId);
            modelBuilder.Entity<Reservation>().HasRequired<Clients>(x => x.Clients).WithMany(x => x.Reservation).HasForeignKey<int>(s => s.ClientId);
            modelBuilder.Entity<Clients>().HasRequired<Country>(x => x.Country).WithMany(x => x.Clients).HasForeignKey<int>(s => s.IdCountry);
            modelBuilder.Entity<Invoices>().HasRequired<ReservationDetails>(a => a.ReservationDetails).WithMany(b => b.Invoices).HasForeignKey<int>(c => c.ReservationDetailsId);
            modelBuilder.Entity<Reservation>().HasRequired<Employee>(a => a.Employee).WithMany(b => b.Reservations).HasForeignKey<int>(c => c.IdEmployee).WillCascadeOnDelete(false);
            modelBuilder.Entity<Employee>().HasRequired<Country>(a => a.Country).WithMany(b => b.Employee).HasForeignKey<int>(c => c.CountryId);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}