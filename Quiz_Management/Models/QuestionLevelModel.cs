
using System.ComponentModel.DataAnnotations;

namespace Quiz_Management.Models
{
    public class QuestionLevelModel
    {
        public int QuestionLevelID { get; set; }
        [Required]
        public string QuestionLevel { get; set; }
        [Required]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime Created {  get; set; }
        public DateTime Modified { get; set; }
    }
}
