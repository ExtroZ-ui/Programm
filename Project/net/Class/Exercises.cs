using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table("Exercises")]
    public class Exercises
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column]
        public string discipline { get; set; }

        [Column]
        public int tema { get; set; }

        [Column]
        public int zanytie { get; set; }

        [Column]
        public int vid { get; set; }

        [Column]
        public string name_lesson { get; set; }

        [Column]
        public string description_lesson { get; set; }

        [Column]
        public string type_lesson { get; set; }

        [Column]
        public string link { get; set; }

        [Column]
        public string text { get; set; }

        [Column]
        public string specialty { get; set; }

        [Column]
        public int vremy { get; set; }

        [Column]
        public int temp { get; set; }
    }
}
