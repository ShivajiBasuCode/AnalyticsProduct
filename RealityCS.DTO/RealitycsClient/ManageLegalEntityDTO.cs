using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.RealitycsClient
{
    public class ManageLegalEntityDTO: ManageAddLegalEntityDTO
    {
        public int Id { get; set; }
    }

    public class ManageLegalEntityDTOValidator : AbstractValidator<ManageLegalEntityDTO>
    {
        public ManageLegalEntityDTOValidator()
        {
            RuleFor(l => l.Name)
                .MaximumLength(256)
                .NotEmpty()
                .NotNull();

            RuleFor(l => l.PrimaryEmailId)
                .MaximumLength(320)
                .NotEmpty()
                .NotNull();

            RuleFor(l => l.LegalEntityIdentifier)
                .MaximumLength(30)
                .NotEmpty()
                .NotNull();

            RuleFor(l => l.PrimaryPhoneNumber)
                .MaximumLength(15)
                .NotEmpty()
                .NotNull();

            RuleFor(l => l.Address)
                .MaximumLength(200);

            RuleFor(l => l.CountryCodeOfOperation)
                .GreaterThan(0);

            RuleFor(l => l.IndustryId)
                .GreaterThan(0);

            RuleFor(l => l.LogoFileName)
                .MaximumLength(100)
                .NotEmpty()
                .NotNull();

            RuleFor(l => l.WebSite)
                .MaximumLength(253);

            RuleFor(l => l.CurrencyId)
                .GreaterThan(0);

        }
    }

}
