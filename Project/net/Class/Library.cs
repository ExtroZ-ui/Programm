using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table("Library")]
    public class Library
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column]
        public string adress { get; set; }

        [Column]
        public string specialization { get; set; }

        [Column]
        public string inscription { get; set; }

    }
}
