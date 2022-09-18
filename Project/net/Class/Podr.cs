using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;
namespace Project.net.Class
{
    [Table(Name = "Подразделения")]
    internal class Podr
    {

        [Column(Name = "Код", IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column(Name = "Рота")]
        public int rota { get; set; }

        [Column(Name = "Взвод")]
        public int vzvod { get; set; }

        [Column(Name = "Специальность(сокр)")]
        public string sp_soc { get; set; }


        [Column(Name = "Специальность(полн)")]
        public string sp_pol { get; set; }
    }
}
