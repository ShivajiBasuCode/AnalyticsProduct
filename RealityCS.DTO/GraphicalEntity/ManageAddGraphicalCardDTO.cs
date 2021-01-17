using FluentValidation;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class ManageAddGraphicalCardAttributeIdDTO
    {
        public int DashboardId { get; set; }
        public int CardId { get; set; }
        public int CustomerDataSourceIdentifier { get; set; }
        public string DataAttribute { get; set; }
    }
    public class ManageAddGraphicalCardAttributeIdDTOValidator : AbstractValidator<ManageAddGraphicalCardAttributeIdDTO>
    {
        public ManageAddGraphicalCardAttributeIdDTOValidator()
        {
            RuleFor(a => a.DashboardId).GreaterThan(0);

            RuleFor(a => a.CustomerDataSourceIdentifier)
                .GreaterThan(0);

            RuleFor(a => a.DataAttribute)
                .MaximumLength(300)
                .NotNull()
                .NotEmpty();

        }
    }
    public class ManageAddGraphicalCardDTO
    {
        public int KpiId { get; set; }
        public int FK_DashboardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ReferenceAxis { get; set; }
        public int DataPlotterAxis { get; set; }
        public int CustomerDataSourceIdentifier { get; set; }
        public string? ReferenceAxisAttribute { get; set; }
        public List<ManageAddGraphicalCardAttributeIdDTO> DataPlotAxisAttributes { get; set; }
        public int SelectedGraphType { get; set; }
    }
    public class ManageAddGraphicalCardDTOValidator : AbstractValidator<ManageAddGraphicalCardDTO>
    {
        public ManageAddGraphicalCardDTOValidator()
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

            RuleFor(c => c.DataPlotterAxis)
                .NotEmpty()
                .NotNull();//.IsInEnum();

            RuleFor(c => c.SelectedGraphType)
                .NotEmpty()
                .NotNull();//.IsInEnum();

            RuleFor(c => c.CustomerDataSourceIdentifier)
                .GreaterThan(0);

            RuleFor(c => c.ReferenceAxisAttribute)
                .MaximumLength(300);
        }
    }
}
