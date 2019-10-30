using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollectorExercise.Models
{
    public class Day
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}