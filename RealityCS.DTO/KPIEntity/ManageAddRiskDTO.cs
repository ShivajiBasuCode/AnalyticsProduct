using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO.KPIEntity
{
    public class ManageAddRiskDTO 
    {
        public string Risk { get; set; }
        public string Description { get; set; }
        public string RiskMitigationPlan { get; set; }
        public int KPIValueStreamForMitigationId { get; set; }
        public string RiskContiguencyPlan { get; set; }
        public decimal RiskValue { get; set; }
        public int KPIValueStreamForContiguencyId { get; set; }
        public int ?DepartmentId { get; set; }
        public int ?DivisionId { get; set; }
    }

    public class ManageAddRiskDTOValidator : AbstractValidator<ManageAddRiskDTO>
    {
        public ManageAddRiskDTOValidator()
        {
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
