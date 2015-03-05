namespace ImporterWindowsService.DBContexts.ImportDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ImportDB : DbContext
    {
        public ImportDB()
            : base("name=ImportDB")
        {
        }

        public virtual DbSet<Hardware> Hardwares { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<Datum> Data { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hardware>()
                .Property(e => e.SerialNo)
                .IsUnicode(false);

            modelBuilder.Entity<Hardware>()
                .HasMany(e => e.Data)
                .WithRequired(e => e.Hardware)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .HasOptional(e => e.Hardware)
                .WithRequired(e => e.Site);

            modelBuilder.Entity<Datum>()
                .Property(e => e.Value)
                .HasPrecision(18, 0);
        }
    }
}
