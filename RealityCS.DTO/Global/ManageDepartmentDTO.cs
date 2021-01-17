using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.Global
{
    public class ManageDepartmentDTO:ManageAddDepartmentDTO
    {
        public int Id { get; set; }

    }
    public class ManageDepartmentDTOValidator : AbstractValidator<ManageDepartmentDTO>
    {
        public ManageDepartmentDTOValidator()
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
