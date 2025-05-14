using MovieApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Data{ 
 public class MovieRepository //film bilgilerini saklamak icin static liste kulllanan sinif
{
    private static readonly List<Movie> _movies = null; // film nesnelerini saklamak için kullanılan bir listeyi temsil eder.
        static MovieRepository()  //@*constructor*@ //sinif ilk kez kullanildiginda cagrilir
        {
            _movies = new List<Movie>()
            {
                new Movie
                {
                    MovieId=1,
                    Title = "film 1",
                    Description = "aciklama",
                    Director = "Yonetmen 1",
                    Players = new string[] { "oyuncu 1", "oyuncu 2" },
                    ImageUrl = "1.jpg",
                    GenreId=1,
                    Point=6.7
                },
                new Movie
                {
                    MovieId=2,
                    Title = "film 2",
                    Description = "aciklama",
                    Director = "Yonetmen 2",
                    Players = new string[] { "oyuncu 1", "oyuncu 2" },
                    ImageUrl = "2.jpg",
                    GenreId=2,
                    Point=9.3 
                },
                new Movie
                {
                    MovieId=3,
                    Title = "film 3",
                    Description = "aciklama",
                    Director = "Yonetmen 1",
                    Players = new string[] { "oyuncu 1", "oyuncu 2" },
                    ImageUrl = "3.jpg",
                    GenreId=3,
                    Point=8.7
                },
                new Movie
                {
                    MovieId=4,
                    Title = "film 4",
                    Description = "aciklama",
                    Director = "Yonetmen 1",
                    Players = new string[] { "oyuncu 1", "oyuncu 2" },
                    ImageUrl = "1.jpg",
                    GenreId=4,
                    Point=4.3
                },
                new Movie
                {
                    MovieId=5,
                    Title = "film 5",
                    Description = "aciklama ",
                    Director = "Yonetmen 2",
                    Players = new string[] { "oyuncu 1", "oyuncu 2" },
                    ImageUrl = "4.jpg",
                    GenreId=1,
                    Point=7.7
                },
                new Movie
                {
                    MovieId=6,
                    Title = "film 6",
                    Description = "aciklama",
                    Director = "Yonetmen 1",
                    Players = new string[] { "oyuncu 1", "oyuncu 2" },
                    ImageUrl = "6.jpg",
                    GenreId=3,
                    Point=9.2
                }
            };
        }
    public static List<Movie> Movies
    {
        get
        {
            return _movies; //_movies listesinin bir kopyasını döndürür.
                           // Bu özellik, filmlerin dışarıdan erişilmesini sağlar
        }
    }
        public static void Add(Movie movie)
        {
            movie.MovieId = Movies.Count() + 1; //suanki film sayisina +1 ekleyerek yeni film sayisini bulur otomotik artar
            _movies.Add(movie); //Add metodu, yeni bir Movie nesnesini _movies listesine ekler.
        }
    public static Movie GetById(int id) //GetById metodu, belirtilen id değerine sahip film nesnesini _movies listesinden bulur ve döndürür.
        {
        return _movies.FirstOrDefault(m => m.MovieId == id); //listeden gonderilen her objenin movie idsi ile disaridan gelen degerin idsi kontrol edilir
    }

        public static void Edit (Movie m) //formdan gelen obje movie controolerda get metoduyla alinir
        {                                 //Edit metodu, verilen Movie nesnesini _movies listesinde bulur ve günceller.
            foreach (var movie in _movies)
            {
                if(movie.MovieId==m.MovieId)
                {
                    movie.Title = m.Title;
                    movie.Description = m.Description;
                    movie.Director = m.Director;
                    movie.ImageUrl = m.ImageUrl;
                    movie.GenreId = m.GenreId;
                    break;
                }
            }
        }

        public static void Delete(int MovieId)  //Delete metodu, belirtilen MovieId'ye sahip filmi _movies listesinden siler.
        {
            var movie = GetById(MovieId);
              if(movie!=null)
                {
                _movies.Remove(movie); //kaldirir
                }
        }


}
}