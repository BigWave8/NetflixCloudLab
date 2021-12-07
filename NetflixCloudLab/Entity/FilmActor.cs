using System;
using System.Collections.Generic;
using System.Text;

namespace Serverless.Entity
{
    public class FilmActor
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int ActorId { get; set; }
    }
}
