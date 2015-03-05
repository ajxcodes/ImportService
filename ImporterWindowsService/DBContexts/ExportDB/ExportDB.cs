namespace ImporterWindowsService.DBContexts.ExportDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ExportDB : DbContext
    {
        public ExportDB()
            : base("name=ExportDB")
        {
        }

        public virtual DbSet<Datum> Data { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Datum>()
                .Property(e => e.SerialNo)
                .IsUnicode(false);

            modelBuilder.Entity<Datum>()
                .Property(e => e.Value)
                .HasPrecision(18, 0);
        }
    }
}
