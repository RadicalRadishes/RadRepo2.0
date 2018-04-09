using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Scores
{
    [Table("High Score")]
    public class Score
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Position { get; set; }

        [Display(Name = "High Score")]
        public int HighScore { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        public virtual ApplicationUser Username { get; set; }
    }
}