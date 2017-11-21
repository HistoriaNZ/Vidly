using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


/* Implementation: Api(*) -> Domain (model) ->
 * 
 */
namespace Vidly.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date Rented")]
        public DateTime DateRented { get; set; }

        [Display(Name = "Date Returned")]
        public DateTime? DateReturned { get; set; }
        
        [Required]
        [Display(Name = "Customer")]
        public Customer Customer { get; set; }

        [Required]
        [Display(Name = "Movies")]
        public Movie Movie { get; set; }
    }
}