using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrashCollectorExercise.Models;

namespace TrashCollectorExercise.ViewModels
{
    public class BalanceViewModel
    {
        public Customer customer { get; set; }

        public string predictedBalance { get; set; }


    }
}