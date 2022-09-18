using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table(Name = "User")]
    public class Library
    {
        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column(Name = "adress")]
        public string adress { get; set; }

        [Column(Name = "specialization")]
        public string specialization { get; set; }

        [Column(Name = "inscription")]
        public string inscription { get; set; }

    }
}
