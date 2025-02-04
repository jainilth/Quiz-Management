using System.Data;

namespace Quiz_Management.Models
{
    public class QuizModel
    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime QuizDate { get; set; }
        public string UserName { get; set; }  // From MST_User
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

}
