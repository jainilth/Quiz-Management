using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Quiz_Management.Models
{
    public class QuizModel
    {
        public int QuizID { get; set; } // Nullable
        [Required]
        public string QuizName { get; set; }
        [Required]
        public int TotalQuestions { get; set; }
        [Required]
        public DateTime QuizDate { get; set; }
        [Required]
        public int UserID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }


}
