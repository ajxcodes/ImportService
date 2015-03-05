namespace ImporterWindowsService.DBContexts.ImportDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Datum
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HardwareId { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal Value { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Timestamp { get; set; }

        public virtual Hardware Hardware { get; set; }
    }
}
