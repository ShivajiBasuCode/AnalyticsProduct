using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Global
{
    public class ManageAddDivisionDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ManageAddDivisionDTOValidator : AbstractValidator<ManageAddDivisionDTO>
    {
        public ManageAddDivisionDTOValidator()
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
