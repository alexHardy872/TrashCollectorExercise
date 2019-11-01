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
        public double lat { get; set; }
        public double lng { get; set; }


        public GeocoderViewModel(Customer customerIn)
        {
            customer = customerIn;



        }


    }
}