using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table("StateTest")]
    public class StateTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column]
        public string data { get; set; }

        [Column]
        public string nameTest { get; set; }

        [Column]
        public int grade { get; set; }

        [Column]
        public int error { get; set; }

        [Column]
        public string familiy { get; set; }

        [Column]
        public string discipline { get; set; }

        [Column]
        public string speciality { get; set; }

        [Column]
        public string namePK { get; set; }

        [Column]
        public string nomVopr { get; set; }

        [Column]
        public string time { get; set; }

        [Column]
        public int vzdov { get; set; }

        [Column]
        public int rota { get; set; }

        [Column]
        public int idUser { get; set; }
    }
}
