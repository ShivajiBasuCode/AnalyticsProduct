using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class ManageDashboardNavigationInVisualisationDTO
    {
        public int DashboardId { get; set; }
        public int ValuestreamId { get; set; }
        public string DashboardName { get; set; }
        public string DashboardDescription { get; set; }
        public string ValueStreamName { get; set; }
        public string ValueStreamDescription { get; set; }
    }
    public class ManageGraphicalDashboardInVisualisationDTOValidator : AbstractValidator<ManageDashboardNavigationInVisualisationDTO>
    {
        public ManageGraphicalDashboardInVisualisationDTOValidator()
        {

            RuleFor(d => d.ValueStreamName)
                .MaximumLength(100)
                .NotEmpty()
                .NotNull();

            RuleFor(d => d.DashboardName)
                .MaximumLength(100)
                .NotEmpty()
                .NotNull();

            RuleFor(d => d.DashboardDescription)
                .MaximumLength(300);

            RuleFor(d => d.ValueStreamDescription)
                .MaximumLength(300);

        }
    }

    public class ManageGraphicalDashboardDTO: ManageAddGraphicalDashboardDTO
    {
        public int Id { get; set; }
    }

    public class ManageGraphicalDashboardDTOValidator : AbstractValidator<ManageGraphicalDashboardDTO>
    {
        public ManageGraphicalDashboardDTOValidator()
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
