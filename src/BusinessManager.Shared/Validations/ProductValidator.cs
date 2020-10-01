using BusinessManager.Models.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BusinessManager.Shared.Validations
{
    public class ProductValidator : AbstractValidator<Products>
    {
        public ProductValidator()
        {

            RuleFor(x => x.Barcode)
            .NotNull().WithMessage("Barcode must have a value").WithErrorCode("Error")
            .NotEmpty().WithMessage("Barcode must have a value").WithErrorCode("Error");


        }
    }
}
