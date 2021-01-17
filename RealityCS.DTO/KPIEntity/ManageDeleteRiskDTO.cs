using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO.KPIEntity
{
    public class ManageDeleteRiskDTO
    {
        public int Id { get; set; }
    }
    public class ManageDeleteRiskDTOValidator : AbstractValidator<ManageDeleteRiskDTO>
    {
        public ManageDeleteRiskDTOValidator()
        {
            RuleFor(r => r.Id)
                  .GreaterThan(0);
        }
    }
}
