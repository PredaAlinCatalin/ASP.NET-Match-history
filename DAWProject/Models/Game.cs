using DAWProject.Models.MyValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAWProject.Models
{
    public class Game
    {
        public int GameId { get; set; }

        [DurationNumberValidator]
        public int Duration { get; set; }

        [Required, RegularExpression(@"^((0[1-9])|([12]\d)|(3[01]))$", ErrorMessage = "This is not a valid day number!")]
        public string Day { get; set; }

        [Required, RegularExpression(@"^(0[1-9])|(1[012])$", ErrorMessage = "This is not a valid month!")]
        public string Month { get; set; }

        [Required, RegularExpression(@"^[1-9](\d{3})$", ErrorMessage = "This is not a valid year!")]
        public int Year { get; set; }

        //many to one
        public virtual ICollection<GameResult> GameResults { get; set; }

        //many to many
        public virtual ICollection<Attribute> Attributes { get; set; }

        // checkboxes list
        [NotMapped]
        public List<CheckBoxViewModel> AttributesList { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}