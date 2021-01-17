using FluentValidation;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class ManageGraphicalCardAttributeIdDTO
    {
        public int Id { get; set; }
        public int DashboardId { get; set; }
        public int CardId { get; set; }
        public int CustomerDataSourceIdentifier { get; set; }

        public string DataAttribute { get; set; }
    }
    public class ManageGraphicalCardAttributeIdDTOValidator : AbstractValidator<ManageGraphicalCardAttributeIdDTO>
    {
        public ManageGraphicalCardAttributeIdDTOValidator()
        {
            RuleFor(a => a.CardId)
                .GreaterThan(0);

            RuleFor(a => a.DashboardId)
                .GreaterThan(0);

            RuleFor(a => a.CustomerDataSourceIdentifier)
                .GreaterThan(0);

            RuleFor(a => a.DataAttribute)
                .MaximumLength(300)
                .NotNull()
                .NotEmpty();

        }
    }
    public class ManageGraphicalCardDTO
    {
        public int Id { get; set; }
        public int KpiId { get; set; }
        public int FK_DashboardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ReferenceAxis { get; set; }
        public int DataPlotterAxis { get; set; }
        public int CustomerDataSourceIdentifier { get; set; }
        public string? ReferenceAxisAttribute { get; set; }
        public List<ManageGraphicalCardAttributeIdDTO> DataPlotAxisAttributeIds { get; set; }
        public int SelectedGraphType { get; set; }
    }
    public class ManageGraphicalCardDTOValidator : AbstractValidator<ManageGraphicalCardDTO>
    {
        public ManageGraphicalCardDTOValidator()
        {
            RuleFor(c => c.KpiId)
                .GreaterThan(0);

            RuleFor(c => c.FK_DashboardId)
                .GreaterThan(0);

            RuleFor(c => c.Name)
                .MaximumLength(100)
                .NotEmpty()
                .NotNull();

            RuleFor(c => c.Description)
                .MaximumLength(300);

            RuleFor(c => c.DataPlotterAxis).IsInEnum();

            RuleFor(c => c.SelectedGraphType).IsInEnum();

            RuleFor(c => c.CustomerDataSourceIdentifier)
                .GreaterThan(0);

            RuleFor(c => c.ReferenceAxisAttribute)
                .MaximumLength(300);
        }
    }
}
