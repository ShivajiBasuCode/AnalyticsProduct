using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class ManageAddGraphicalTemplateDTO
    {
        public int LinkedUITemplateId { get; set; }
    }

    public class ManageAddGraphicalTemplateDTOValidator : AbstractValidator<ManageAddGraphicalTemplateDTO>
    {
        public ManageAddGraphicalTemplateDTOValidator()
        {
            RuleFor(t => t.LinkedUITemplateId)
                .GreaterThan(0);
        }
    }
}
