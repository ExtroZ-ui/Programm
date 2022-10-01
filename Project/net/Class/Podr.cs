using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.net.Class
{
    [Table("Podr")]
    internal class Podr
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column]
        public int rota { get; set; }

        [Column]
        public int vzvod { get; set; }

        [Column]
        public string sp_soc { get; set; }


        [Column]
        public string sp_pol { get; set; }
    }
}
