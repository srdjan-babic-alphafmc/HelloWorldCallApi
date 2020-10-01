using System;
using System.Collections.Generic;

namespace BusinessManager.Models.Models
{
    public class Configuration
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TotalPrice { get; set; }
        public string WhoOrdered { get; set; }
        public List<Products> Components { get; set; }
        public bool Deleted { get; set; }
    }
}
