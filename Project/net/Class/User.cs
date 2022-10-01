using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table("User")]
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column]
        public string famile { get; set; }

        [Column]
        public string name { get; set; }

        [Column]
        public string otch { get; set; }

        [Column]
        public int rota { get; set; }

        [Column]
        public int vzvod { get; set; }

        [Column]
        public string specal { get; set; }

        [Column]
        public int tp { get; set; }

        [Column]
        public int sp { get; set; }

        [Column]
        public bool access { get; set; }
    }

}
