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
        public string streetAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [Display(Name = "State")]
        public State state { get; set; }



        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "ZIP code")]
        public string zip { get; set; }

        [Required]
        [Display(Name = "Pickup Day")]
        public DayOfWeek pickupDay { get; set; }



        [DataType(DataType.Currency)]
        [Display(Name = "Balance Due")]
        public decimal balance { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Vacation")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startBreak { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Vacation")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? endBreak { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [Display(Name = "One-Time Pickup")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? oneTimePickup { get; set; }




        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }



        [Display(Name = "Pickup Confirmed")]
        public bool confirmed { get; set; }


    }
}