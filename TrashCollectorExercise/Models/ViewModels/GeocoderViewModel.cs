using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml.Linq;

namespace TrashCollectorExercise.Models.ViewModels
{
    public class GeocoderViewModel
    {
        public Customer customer { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string address { get; set; }


        public GeocoderViewModel()
        {
   
        }

        public string FormatAddress(Customer customer)
        {
            string address = customer.streetAddress + " " + customer.city + " " + customer.state + " " + customer.zip;
            return address;
        }


    }
}