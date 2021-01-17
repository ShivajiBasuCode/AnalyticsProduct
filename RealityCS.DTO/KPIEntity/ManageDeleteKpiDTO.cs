using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO.KPIEntity
{

    public class ManageDeleteCascadeKpiDrilldownDataElementDTO : ManageDeleteKpiDataElementDTO
    {
        public int DrilldownElementId { get; set; }
    }
    public class ManageDeleteCascadeKpiDrilldownDataElementDTOValidator : AbstractValidator<ManageDeleteCascadeKpiDrilldownDataElementDTO>
    {
        public ManageDeleteCascadeKpiDrilldownDataElementDTOValidator()
        {
            RuleFor(d => d.Id)
                  .GreaterThan(0);

            RuleFor(d => d.DataElementId)
                  .GreaterThan(0);

            RuleFor(d => d.DrilldownElementId)
                  .GreaterThan(0);
        }
    }
    public class ManageDeleteKpiDataElementDTO : FetchKpiDTO
    {
        public int DataElementId { get; set; }
    }
    public class ManageDeleteKpiDataElementDTOValidator : AbstractValidator<ManageDeleteKpiDataElementDTO>
    {
        public ManageDeleteKpiDataElementDTOValidator()
        {
            RuleFor(d => d.Id)
                  .GreaterThan(0);

            RuleFor(d => d.DataElementId)
                  .GreaterThan(0);
        }
    }

    public class ManageDeleteKpiDTO : FetchKpiDTO
    {

    }
    public class ManageDeleteKpiDTOValidator : AbstractValidator<ManageDeleteKpiDTO>
    {
        public ManageDeleteKpiDTOValidator()
        {
            RuleFor(k => k.Id)
                  .GreaterThan(0);
        }
    }

    public class FetchKpiDTO
    {
        public int Id { get; set; }
    }

    public class FetchKpiDTOValidator : AbstractValidator<FetchKpiDTO>
    {
        public FetchKpiDTOValidator()
        {
            RuleFor(k => k.Id)
                  .GreaterThan(0);
        }
    }

    public class ManageDeleteKPIJoiningRelationDTO : FetchKpiDTO
    {
        public int KpiId { get; set; }
    }
    public class ManageDeleteKPIJoiningRelationDTOValidator : AbstractValidator<ManageDeleteKPIJoiningRelationDTO>
    {
        public ManageDeleteKPIJoiningRelationDTOValidator()
        {
            RuleFor(k => k.Id)
                .GreaterThan(0);
            RuleFor(k => k.KpiId)
                .GreaterThan(0);
        }
    }
}
