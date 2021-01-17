using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.RealitycsClient
{
    public class MangeClientUserDTO
    { 
        public int id { get; set;}
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int AccessGroupId { get; set; }
    }

    public class MangeClientUserDtoValidator : AbstractValidator<MangeClientUserDTO>
    {
        public MangeClientUserDtoValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(256);
            RuleFor(x => x.EmailId)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(256);
            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(256).When(x=>x.id==0);
        }
    }
}
