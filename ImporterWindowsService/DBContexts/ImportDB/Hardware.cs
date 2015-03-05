namespace ImporterWindowsService.DBContexts.ImportDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hardware")]
    public partial class Hardware
    {
        public Hardware()
        {
            Data = new HashSet<Datum>();
        }

        public int Id { get; set; }

        public int SiteId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string SerialNo { get; set; }

        public virtual ICollection<Datum> Data { get; set; }

        public virtual Site Site { get; set; }
    }
}
