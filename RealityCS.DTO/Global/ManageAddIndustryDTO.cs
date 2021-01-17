using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Global
{
    public class ManageAddIndustryDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ManageAddIndustryDTOValidator : AbstractValidator<ManageAddIndustryDTO>
    {
        public ManageAddIndustryDTOValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Description)
                .MaximumLength(300);
        }

    }

}
