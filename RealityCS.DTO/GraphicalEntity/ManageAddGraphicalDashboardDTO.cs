using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class ManageAddGraphicalDashboardDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UsedTemplateId { get; set; }
        public int ValueStreamId { get; set; }
    }
    public class ManageAddGraphicalDashboardDTOValidator : AbstractValidator<ManageAddGraphicalDashboardDTO>
    {
        public ManageAddGraphicalDashboardDTOValidator()
        {
            RuleFor(d => d.UsedTemplateId)
                .GreaterThan(0);

            RuleFor(d => d.ValueStreamId)
                .GreaterThan(0);

            RuleFor(d => d.Name)
                .MaximumLength(100)
                .NotEmpty()
                .NotNull();

            RuleFor(d => d.Description)
                .MaximumLength(300);

        }
    }
}
