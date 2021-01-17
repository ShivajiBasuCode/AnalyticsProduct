using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOUser: IValidatableObject
    {
        public int Id { get; set; }

        [Display(Name ="Active")]
        public bool IsActive { get; set; } = true;       

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email cannot be blank.")]
        [EmailAddress(ErrorMessage = "Email format not valid.")]
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Password")]
        //[Required(ErrorMessage = "Password cannot be blank.")]
        public string Password { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Firstname cannot be blank.")]
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Lastname cannot be blank.")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Gendar")]
        public string Gender { get; set; } = string.Empty;

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role cannot be blank.")]
        public int Role { get; set; }       
        

        [Display(Name = "Manager")]
        public Nullable<int> RepotsTo { get; set; } = null;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Id <=0 && string.IsNullOrEmpty(Password))
            {
                yield return new ValidationResult("Password cannot be blank.");
            }
        }

    }
    /// <summary>
    /// 
    /// </summary>
    public class DTOUserSearchCriteria
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
