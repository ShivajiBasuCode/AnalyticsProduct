using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO.KPIEntity
{
    public class ManageRiskDTO: ManageAddRiskDTO
    {
        public int Id { get; set; }
    }
    public class ManageRiskDTOValidator : AbstractValidator<ManageRiskDTO>
    {
        public ManageRiskDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Risk)
                .MaximumLength(100)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Description)
                .MaximumLength(300);

            RuleFor(x => x.RiskMitigationPlan)
                .MaximumLength(500);

            RuleFor(x => x.RiskContiguencyPlan)
                .MaximumLength(500);
        }
    }

}
