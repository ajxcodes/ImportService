namespace ImporterWindowsService.DBContexts.ExportDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Datum
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string SerialNo { get; set; }

        public decimal Value { get; set; }

        public DateTime Time { get; set; }
    }
}
