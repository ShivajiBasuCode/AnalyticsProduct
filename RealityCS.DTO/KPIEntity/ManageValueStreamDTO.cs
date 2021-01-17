using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO.KPIEntity
{
    public class ManageValueStreamDTO:ManageAddValueStreamDTO
    {
        public int Id { get; set; }
        public class ManageValueStreamDTOValidator : AbstractValidator<ManageValueStreamDTO>
        {
            public ManageValueStreamDTOValidator()
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .NotNull();

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
