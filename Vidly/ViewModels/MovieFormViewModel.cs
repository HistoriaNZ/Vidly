using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genre { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Date (dd MMM yyyy)")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        public int? NumberInStock { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        [Display(Name = "TMDb Link")]
        public string TmdbLink { get; set; }

        //constructor for a new movie
        public MovieFormViewModel()
        {
            Id = 0;
        }

        //constructor for existing movie
        public MovieFormViewModel (Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
            TmdbLink = movie.TmdbLink;
        }
    }
}