using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Scores
{
    [Table("HighScore")]
    public class Score
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "HighScore")]
        public int HighScore { get; set; }

        [ForeignKey("UserId")]
        public string UserID { get; set; }

        public virtual ApplicationUser UserId { get; set; }
    }
}