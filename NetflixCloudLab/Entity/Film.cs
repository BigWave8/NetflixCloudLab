using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serverless.Entity;

namespace Serverless.Entity
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal? Rating { get; set; }
        public string ReleaseDate { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
