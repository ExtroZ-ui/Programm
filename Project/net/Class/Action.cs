using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table("Action")]
    public class Action
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column]
        public int type_action { get; set; }

        [Column]
        public string date { get; set; }

        [Column]
        public int id_user { get; set; }

        [Column]
        public DateTime time { get; set; }

        [Column]
        public int tema { get; set; }

        [Column]
        public int less { get; set; }

        [Column]
        public string time_work { get; set; }

        [Column]
        public int num_input { get; set; }

        [Column]
        public int error { get; set; }

        [Column]
        public int grade { get; set; }

        [Column]
        public int num_exercise { get; set; }

        [Column]
        public int temp { get; set; }

        [Column]
        public string pc { get; set; }
    }
}
