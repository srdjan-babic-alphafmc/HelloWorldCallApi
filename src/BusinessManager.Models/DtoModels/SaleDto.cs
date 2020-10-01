using BusinessManager.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.Models.DtoModels
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public string Buyer { get; set; }
        public List<Products> Catalog { get; set; }
        public DateTime SaleDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
