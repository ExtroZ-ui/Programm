using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table(Name = "ActionsAll")]
    public class Action
    {
        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column(Name = "type_action")]
        public int type_action { get; set; }

        [Column(Name = "date")]
        public string date { get; set; }

        [Column(Name = "id_user")]
        public int id_user { get; set; }

        [Column(Name = "time")]
        public DateTime time { get; set; }

        [Column(Name = "tema")]
        public int tema { get; set; }

        [Column(Name = "less")]
        public int less { get; set; }

        [Column(Name = "time_work")]
        public string time_work { get; set; }

        [Column(Name = "num_input")]
        public int num_input { get; set; }

        [Column(Name = "error")]
        public int error { get; set; }

        [Column(Name = "grade")]
        public int grade { get; set; }

        [Column(Name = "num_exercise")]
        public int num_exercise { get; set; }

        [Column(Name = "temp")]
        public int temp { get; set; }

        [Column(Name = "pc")]
        public string pc { get; set; }
    }
}
