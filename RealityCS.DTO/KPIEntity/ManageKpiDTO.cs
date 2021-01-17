using FluentValidation;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.SharedMethods.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO.KPIEntity
{
    public class ManageKpiDataElementDrilldownDTO
    {
        public int Id { get; set; }
        public int KpiId { get; set; }
        public int KpiDataElementId { get; set; }
        public int DrillDownOrder { get; set; }
        public int JoiningCustomerDataElementIdentifier { get; set; }//Data source
        public string CustomerDataAttribute { get; set; }
        public int NextDrilldownId { get; set; }
    }
    public class ManageKpiDataElementDrilldownDTOValidator : AbstractValidator<ManageKpiDataElementDrilldownDTO>
    {
        public ManageKpiDataElementDrilldownDTOValidator()
        {
            RuleFor(x => x.JoiningCustomerDataElementIdentifier)
                .NotEqual(0);

            RuleFor(x => x.CustomerDataAttribute)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.KpiId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.KpiDataElementId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.DrillDownOrder)
                .NotEmpty()
                .NotNull();

        }
    }
    public class ManageKPIJoiningRelationDTO
    {
        public int Id { get; set; }
        public int KpiId { get; set; }
        public int JoiningCustomerDataElementIdentifier { get; set; }//Data source
        public string JoiningAttribute { get; set; }
        public int JoiningRelationship { get; set; }
        public int JoiningCustomerDataElementIdentifierInRelation { get; set; } //Data source
        public string JoiningAttributeInRelation { get; set; }
    }
    public class ManageKPIJoiningRelationDTOValidator : AbstractValidator<ManageAddKPIJoiningRelationDTO>
    {
        public ManageKPIJoiningRelationDTOValidator()
        {
            RuleFor(x => x.JoiningCustomerDataElementIdentifier)
                .NotEqual(0);
            RuleFor(x => x.JoiningAttribute)
                .NotNull()
                .NotEmpty();
        }
    }

    public class ManageKpiDataElementDTO
    {
        public int Id { get; set; }
        public int KpiId { get; set; }
        public int ?AccessGroupId { get; set; }
        public int CustomerDataElementIdentifierOne { get; set; } //this wil be same as present in RealytcsJoiningRelation
        public string CustomerDataAttributeOne { get; set; }
        public int CustomerDataElementIdentifierTwo { get; set; }
        public string? CustomerDataAttributeTwo { get; set; }
        public bool UsedForTimeStampFilter { get; set; }
        //        public int DataElementInformation { get; set; }
        public int? BenchmarkValue { get; set; }
        public int? RedThresholdValue { get; set; }
        public int? AmberThreshholdValue { get; set; }
        public int? GreenThresholdValue { get; set; }
        public int FormulaToBeApplied { get; set; }
        public List<ManageKpiDataElementDrilldownDTO> ManageKpiDataElementDrilldown { get; set; }
    }
    public class ManageKpiDataElementDTOValidator : AbstractValidator<ManageKpiDataElementDTO>
    {
        public ManageKpiDataElementDTOValidator()
        {
            RuleFor(x => x.CustomerDataElementIdentifierOne)
                .NotEqual(0);

            RuleFor(x => x.CustomerDataAttributeOne)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.KpiId)
                .NotEmpty()
                .NotNull();
/*
            RuleFor(x => x.DataElementInformation)
                .IsInEnum();

            RuleFor(x => x.FormulaToBeApplied)
                .IsInEnum();*/
        }
    }

    public class ManageKpiDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Objective { get; set; }
        public int? IndustryId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DivisionId { get; set; }
        public int? KpiValueStreamId { get; set; }
        public int CustomerDataElementIdentifier { get; set; }

        public List<ManageKpiDataElementDTO> KpiDataElements { get; set; }
        public List<ManageKPIJoiningRelationDTO> KpiJoiningRelationship { get; set; }

    }

    public class ManageKpiDTOValidator : AbstractValidator<ManageKpiDTO>
    {
        public ManageKpiDTOValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Description)
                .MaximumLength(300);
            RuleFor(x => x.Objective)
                .MaximumLength(200);

            RuleFor(x => x.KpiDataElements)
                .Must(x => x != null && x.Count > 0 && (x.FindAll(e => e.UsedForTimeStampFilter == true).Count > 0));
        }
    }

    public class ManageKpiLiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ManageKpiLiteDTOValidator : AbstractValidator<ManageKpiLiteDTO>
    {
        public ManageKpiLiteDTOValidator()
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