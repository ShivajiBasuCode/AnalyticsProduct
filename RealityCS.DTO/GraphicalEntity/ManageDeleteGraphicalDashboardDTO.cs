using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.GraphicalEntity
{
    public class ManageDeleteGraphicalDashboardDTO
    {
        public int Id { get; set; }
    }
    public class ManageDeleteGraphicalDashboardDTOValidator : AbstractValidator<ManageDeleteGraphicalDashboardDTO>
    {
        public ManageDeleteGraphicalDashboardDTOValidator()
        {
            RuleFor(d => d.Id)
                 .GreaterThan(0);
        }
    }

    public class ManageSelectGraphicalDashboardDTO: ManageDeleteGraphicalDashboardDTO
    {

    }
    public class ManageSelectGraphicalDashboardDTOValidator : AbstractValidator<ManageSelectGraphicalDashboardDTO>
    {
        public ManageSelectGraphicalDashboardDTOValidator()
        {
            RuleFor(d => d.Id)
                 .GreaterThan(0);
        }
    }

}
