using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class ManageGraphicalTemplateDTO: ManageAddGraphicalTemplateDTO
    {
        public int Id { get; set; }
    }

    public class ManageGraphicalTemplateDTOValidator : AbstractValidator<ManageGraphicalTemplateDTO>
    {
        public ManageGraphicalTemplateDTOValidator()
        {
            RuleFor(t => t.Id)
                  .GreaterThan(0);

            RuleFor(t => t.LinkedUITemplateId)
                  .GreaterThan(0);
        }
    }
}
