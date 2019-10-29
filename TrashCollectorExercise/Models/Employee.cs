using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollectorExercise.Models
{
    public class Employee
    {


        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ZIP Code")]
        public int zipCode { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }







    }
}