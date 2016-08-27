namespace OracelEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YJTJ.TB_COMLOCATION")]
    public partial class TB_COMLOCATION
    {
        [StringLength(400)]
        public string NSR_MC { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string NSR_ID { get; set; }

        [StringLength(1000)]
        public string FADDRESS { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal LON { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal LAT { get; set; }

        [StringLength(30)]
        public string EDITUSER { get; set; }

        public DateTime? EDITTIME { get; set; }

        [StringLength(20)]
        public string XYSYSTEM { get; set; }
    }
}
