using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Web.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "Film başlığı eklemelisiniz.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Film başlığı 5-50 karakter aralığında olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "Film açıklaması eklemelisiniz.")]
        public string Description { get; set; }

        [DisplayName("Yönetmen")]
        [StringLength(100)]
        public string Director { get; set; }

        [NotMapped]
        public string[] Players { get; set; }

        [DisplayName("Resim URL")]
        public string ImageUrl { get; set; }

        [DisplayName("Tür")]
        [Required]
        public int GenreId { get; set; }

        [DisplayName("Puan")]
        public double Point { get; set; }
    }
}


