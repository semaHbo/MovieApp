using System.Collections.Generic;

namespace MovieApp.Web.Models
{
    public class MoviesViewModel
    {
        public List<Movie> Movies  { get; set; }
 
    }
}
//bir veya daha fazla Movie nesnesini içeren bir listeyi saklamak için kullanılır. Bu model genellikle bir view'da (görünümde) filmleri göstermek amacıyla veri sağlayan bir yapı olarak işlev görür