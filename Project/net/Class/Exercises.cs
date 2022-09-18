using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table(Name = "Exercises")]
    public class Exercises
    {
        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column(Name = "discipline")]
        public string discipline { get; set; }

        [Column(Name = "tema")]
        public int tema { get; set; }

        [Column(Name = "zanytie")]
        public int zanytie { get; set; }

        [Column(Name = "vid")]
        public int vid { get; set; }

        [Column(Name = "name_lesson")]
        public string name_lesson { get; set; }

        [Column(Name = "description_lesson")]
        public string description_lesson { get; set; }

        [Column(Name = "type_lesson")]
        public string type_lesson { get; set; }

        [Column(Name = "link")]
        public string link { get; set; }

        [Column(Name = "text")]
        public string text { get; set; }

        [Column(Name = "specialty")]
        public string specialty { get; set; }

        [Column(Name = "vremy")]
        public int vremy { get; set; }

        [Column(Name = "temp")]
        public int temp { get; set; }
    }
}
