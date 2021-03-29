using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAWProject.Models
{
    public class GameResult
    {
        [Key, Column(Order = 0)]
        public int GameId { get; set; }
        [Key, Column(Order = 1)]
        public int PlayerId { get; set; }

        //one to many
        public virtual Game Game { get; set; }
        //one to many
        public virtual Player Player { get; set; }

        [Range(0, 100,
        ErrorMessage = "NrKills for {0} must be between {1} and {2}.")]
        public int NrKills { get; set; }

        [Range(0, 50,
        ErrorMessage = "NrDeaths for {0} must be between {1} and {2}.")]
        public int NrDeaths { get; set; }

        [Range(0, 300,
        ErrorMessage = "NrAssists for {0} must be between {1} and {2}.")]
        public int NrAssists { get; set; }

        [Range(0, 100000,
        ErrorMessage = "Damage dealt for {0} must be between {1} and {2}.")]
        public int DamageDealt { get; set; }

        [Required, RegularExpression(@"^[1-9](\d{0,3})$", ErrorMessage = "This is not a valid creep score!")]
        public int CreepScore { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}