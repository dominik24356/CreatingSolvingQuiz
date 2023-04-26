using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuizGen.Model.Question
{
    [Table("Questions")]

    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OptionA { get; set; }

        [Required]
        public string OptionB { get; set; }

        [Required]
        public string OptionC { get; set; }

        [Required]
        public string OptionD { get; set; }

        [Required]
        public string CorrectOption { get; set; }

        [ForeignKey("QuizId")]
        public int QuizId { get; set; } // change the type to int

        public Quiz.Quiz Quiz { get; set; }
    }
}
