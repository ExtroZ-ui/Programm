using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table("Subdivision")]
    public class Subdivision
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column("rota")]
        public int rota { get; set; }

        [Column("rota")]
        public int vzvod { get; set; }

        [Column("specShort")]
        public string specShort { get; set; }

        [Column("specAll")]
        public string specAll { get; set; }

    }
}
