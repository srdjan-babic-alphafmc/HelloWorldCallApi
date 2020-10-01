using BusinessManager.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.Models.DtoModels
{
    public class ConfigurationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TotalPrice { get; set; }
        public string WhoOrdered { get; set; }
        public List<Products> Components { get; set; }
    }
}
