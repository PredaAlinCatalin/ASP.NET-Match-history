using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DAWProject.Models
{
    public class Player
    {
        public int PlayerId { get; set; }

        [MinLength(5, ErrorMessage = "Name cannot be less than 5!"),
           MaxLength(20, ErrorMessage = "Name cannot be more than 20!")]
        public string Name { get; set; }

        //many to one
        public virtual ICollection<GameResult> GameResults { get; set; }

        //one to one
        [Required]
        public virtual PlayerStat PlayerStat { get; set; }

        //one to many
        public virtual int DivisionId { get; set; }
        public virtual Division Division { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}