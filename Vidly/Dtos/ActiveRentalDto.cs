using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class ActiveRentalDto
    {
        [Required]
        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }

        public int CustomerId { get; set; }

        [Required]
        public Movie Movie { get; set; }

        public int MovieId { get; set; }

    }
}