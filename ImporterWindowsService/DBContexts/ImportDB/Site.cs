namespace ImporterWindowsService.DBContexts.ImportDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Site
    {
        public int Id { get; set; }

        [Required]
        public DbGeometry Location { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Address { get; set; }

        public virtual Hardware Hardware { get; set; }
    }
}
