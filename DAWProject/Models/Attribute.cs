using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DAWProject.Models
{
    public class Attribute
    {
        [Key]
        public int AttributeId { get; set; }

        [MinLength(2, ErrorMessage = "Name cannot be less than 2!"),
            MaxLength(30, ErrorMessage = "Name cannot be more than 30!")]
        public string Name { get; set; }
        
        //many to many
        public ICollection<Game> Games { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}