using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Models.Models
{
    public class Products
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Barcode is required")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "SerialNumber is required")]
        public Guid SerialNumber { get; set; }
        public int PurchasePrice { get; set; }
        public int SalePrice { get; set; }
        public bool Warranty { get; set; }
        public DateTime WarrantyExpirationDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public string Others { get; set; }
        public bool Deleted { get; set; }

    }
}
