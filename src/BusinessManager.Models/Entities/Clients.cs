using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Models.Models
{
    public class Clients
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "PostalCode is required")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(60, ErrorMessage = "Email can't be longer than 60 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PIB is required")]
        [StringLength(6, ErrorMessage = "PIB can't be longer than 6 characters")]
        public string PIB { get; set; }
        public string Note { get; set; }
        public bool Deleted { get; set; }
    }
}
