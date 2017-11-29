using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
    public class ReturnViewModel
    {
        [Required]
        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }

        public int CustomerId { get; set; }

        public int MovieId { get; set; }
    }
}