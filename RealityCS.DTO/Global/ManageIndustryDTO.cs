using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Global
{
    public class ManageIndustryDTO : ManageAddIndustryDTO
    {
        public int Id { get; set; }
        public class ManageIndustryDTOValidator : AbstractValidator<ManageIndustryDTO>
        {
            public ManageIndustryDTOValidator()
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
}
