using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollectorExercise.Models
{
    public class Customer
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }


        //[ForeignKey("Address")]
        //public int AddressId { get; set; }
        //public Address Address { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string streetAdress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required]
        [Display(Name = "ZIP code")]
        public int zip { get; set; }

        [Required]
        [Display(Name = "Pickup Day")]
        public DayOfWeek pickupDay { get; set; }

       // [Required]
        [Display(Name = "Balance Due")]
        public decimal balance { get; set; }

        //[Required]
        [Display(Name = "Start Vacation")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startBreak { get; set; }

        //[Required]
        [Display(Name = "End Vacation")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? endBreak { get; set; }

        //[Required]
        [Display(Name = "One-Time Pickup")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? oneTimePickup { get; set; }




        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }






    }
}