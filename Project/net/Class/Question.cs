using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.net.Class
{
    [Table("Question")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column("question")]
        public string question { get; set; }

        [Column("answerOne")]
        public string answerOne { get; set; }

        [Column("answerTwo")]
        public string answerTwo { get; set; }

        [Column("answerThree")]
        public string answerThree { get; set; }

        [Column("answerFour")]
        public string answerFour { get; set; }

        [Column("answerTrue")]
        public string answerTrue { get; set; }

        [Column("idTest")]
        public int idTest { get; set; }

        [Column("image")]
        public string image { get; set; }
    }
}
