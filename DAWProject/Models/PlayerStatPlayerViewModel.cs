using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAWProject.Models
{
    public class PlayerStatPlayerViewModel
    {
        [MinLength(5, ErrorMessage = "Name cannot be less than 5!"),
           MaxLength(20, ErrorMessage = "Name cannot be more than 20!")]
        public string Name { get; set; }

        [Range(1, 5000,
        ErrorMessage = "NrWins for {0} must be between {1} and {2}.")]
        public int NrWins { get; set; }

        [Range(1, 5000,
        ErrorMessage = "NrLosses for {0} must be between {1} and {2}.")]
        public int NrLosses { get; set; }


        [Required]
        public virtual int DivisionId { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}