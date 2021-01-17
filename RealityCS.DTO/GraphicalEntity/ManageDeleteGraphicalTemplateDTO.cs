using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class ManageDeleteGraphicalTemplateDTO
    {
        public int Id { get; set; }
    }

    public class ManageDeleteGraphicalTemplateDTOValidator : AbstractValidator<ManageDeleteGraphicalTemplateDTO>
    {
        public ManageDeleteGraphicalTemplateDTOValidator()
        {
            RuleFor(t => t.Id)
                   .GreaterThan(0);
        }
    }
}
