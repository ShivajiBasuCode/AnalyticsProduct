using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO.KPIEntity
{
    public class ManageAddValueStreamDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ManageAddValueStreamDTOValidator : AbstractValidator<ManageAddValueStreamDTO>
    {
        public ManageAddValueStreamDTOValidator()
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
