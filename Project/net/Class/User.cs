using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table(Name = "User")]
    public class User
    {

        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column(Name = "famile")]
        public string famile { get; set; }

        [Column(Name = "name")]
        public string name { get; set; }

        [Column(Name = "otch")]
        public string otch { get; set; }

        [Column(Name = "rota")]
        public int rota { get; set; }

        [Column(Name = "vzvod")]
        public int vzvod { get; set; }

        [Column(Name = "specal")]
        public string specal { get; set; }

        [Column(Name = "tp")]
        public int tp { get; set; }

        [Column(Name = "sp")]
        public int sp { get; set; }

        [Column(Name = "access")]
        public bool access { get; set; }
    }

}
