using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table(Name = "Question")]
    public class Question
    {
        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column(Name = "question")]
        public string question { get; set; }

        [Column(Name = "answerOne")]
        public string answerOne { get; set; }

        [Column(Name = "answerTwo")]
        public string answerTwo { get; set; }

        [Column(Name = "answerThree")]
        public string answerThree { get; set; }

         [Column(Name = "answerFour")]
        public string answerFour { get; set; }

        [Column(Name = "answerTrue")]
        public string answerTrue { get; set; }

        [Column(Name = "idTest")]
        public int idTest { get; set; }

        [Column(Name = "image")]
        public string image { get; set; }
    }
}
