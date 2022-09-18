using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{

    public class Subdivision
    {
        [Key]
        public int id { get; set; }

        [Column]
        public int rota { get; set; }

        [Column]
        public int vzvod { get; set; }

        [Column]
        public string specShort { get; set; }

        [Column]
        public string specAll { get; set; }

    }
}
