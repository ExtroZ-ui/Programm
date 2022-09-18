using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table(Name = "ButtonGen")]
    public class ButtonGen
    {
        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column(Name = "column")]
        public int column { get; set; }

        [Column(Name = "row")]
        public int row { get; set; }

        [Column(Name = "numburTema")]
        public int numburTema { get; set; }

        [Column(Name = "numburLesson")]
        public int numburLesson { get; set; }

        [Column(Name = "nameTema")]
        public string nameTema { get; set; }

         [Column(Name = "nameLesson")]
        public string nameLesson { get; set; }

         [Column(Name = "description")]
        public string description { get; set; }

        [Column(Name = "discipline")]
        public string discipline { get; set; }

        [Column(Name = "specialization")]
        public string specialization { get; set; }

        [Column(Name = "test")]
        public int test { get; set; }

        [Column(Name = "objectUri")]
        public string objectUri { get; set; }

        [Column(Name = "literature")]
        public string literature { get; set; }
    }
}
