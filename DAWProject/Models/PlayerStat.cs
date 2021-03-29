using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAWProject.Models
{
    [Table("PlayersStat")]
    public class PlayerStat
    {
        public int PlayerStatId { get; set; }

        [Range(1, 5000,
        ErrorMessage = "NrWins for {0} must be between {1} and {2}.")]
        public int NrWins { get; set; }

        [Range(1, 5000,
        ErrorMessage = "NrLosses for {0} must be between {1} and {2}.")]
        public int NrLosses { get; set; }

        //one to one
        public virtual Player Player { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}