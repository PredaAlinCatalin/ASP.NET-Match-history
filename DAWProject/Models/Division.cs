using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAWProject.Models
{
    public class Division
    {
        public int DivisionId { get; set; }

        [MinLength(2, ErrorMessage = "Name cannot be less than 2!"),
           MaxLength(30, ErrorMessage = "Name cannot be more than 30!")]
        public string Name { get; set; }

        //many to one
        public virtual ICollection<Player> Players { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}