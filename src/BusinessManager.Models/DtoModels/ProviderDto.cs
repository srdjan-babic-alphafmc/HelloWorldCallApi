using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.Models.DtoModels
{
    public class ProviderDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PIB { get; set; }
        public string Note { get; set; }
    }
}
