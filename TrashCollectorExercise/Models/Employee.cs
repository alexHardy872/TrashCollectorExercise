using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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








    }
}