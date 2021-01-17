using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Global
{
    public class ManageDivisionDTO:ManageAddDivisionDTO
    {
        public int Id { get; set; }
        public class ManageDivisionDTOValidator : AbstractValidator<ManageDivisionDTO>
        {
            public ManageDivisionDTOValidator()
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
