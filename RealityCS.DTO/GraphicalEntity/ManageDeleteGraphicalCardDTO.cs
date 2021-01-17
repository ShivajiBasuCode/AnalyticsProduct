using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class ManageDeleteGraphicalCardDTO
    {
        public int Id { get; set; }
    }
    public class ManageDeleteGraphicalCardDTOValidator : AbstractValidator<ManageDeleteGraphicalCardDTO>
    {
        public ManageDeleteGraphicalCardDTOValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0);
        }
    }
}
