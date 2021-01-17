using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.RealitycsClient
{
    public class ManageAccessGroupDTO
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }       
        public List<int> Operations { get; set; }
    }
    public class ManageAccessGroupDTOValidator : AbstractValidator<ManageAccessGroupDTO>
    {
        public ManageAccessGroupDTOValidator()
        {
            RuleFor(x => x.Name).MaximumLength(256).NotEmpty();
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(300);
            RuleFor(x => x.Operations)
                .NotEmpty();
        }
    }
}
