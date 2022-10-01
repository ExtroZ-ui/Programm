using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table("ButtonGen")]
    public class ButtonGen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column]
        public int column { get; set; }

        [Column]
        public int row { get; set; }

        [Column]
        public int numburTema { get; set; }

        [Column]
        public int numburLesson { get; set; }

        [Column]
        public string nameTema { get; set; }

         [Column]
        public string nameLesson { get; set; }

         [Column]
        public string description { get; set; }

        [Column]
        public string discipline { get; set; }

        [Column]
        public string specialization { get; set; }

        [Column]
        public int test { get; set; }

        [Column]
        public string objectUri { get; set; }

        [Column]
        public string literature { get; set; }
    }
}
