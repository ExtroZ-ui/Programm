using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table("Test")]
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column("idSpecl")]
        public int idSpecl { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("time")]
        public string time { get; set; }

         [Column("countQuestion")]
        public int countQuestion { get; set; }

        [Column("five")]
        public int five { get; set; }

        [Column("four")]
        public int four { get; set; }

        [Column("three")]
        public int three { get; set; }
    }
}
