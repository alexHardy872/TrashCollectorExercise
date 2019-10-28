using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollectorExercise.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Adress Line 1")]
        public string streetAdress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required]
        [Display(Name = "State")]
        public int zip { get; set; }







    }
}