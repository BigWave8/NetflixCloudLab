using System;
using System.Collections.Generic;
using System.Text;

namespace Serverless.Entity
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Decimal Rating { get; set; }
    }
}
