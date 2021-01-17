using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextModels
{
    public partial class User : RealitycsClientBase
    {
        public int FK_LegalEntityId { get; set; }
        public int FK_AccessGroupId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }

#warning Employee Id Table relation Missing 
        public int FK_EmployeeId { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is required to re-login
        /// </summary>
        public bool RequireReLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating number of failed login attempts (wrong password)
        /// </summary>
        public int FailedLoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets the date and time until which a customer cannot login (locked out)
        /// </summary>
        public DateTime? CannotLoginUntilDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the last IP address
        /// </summary>
        public string LastIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login
        /// </summary>
        public DateTime? LastLoginDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last activity
        /// </summary>
        public DateTime LastActivityDateUtc { get; set; }


        public virtual LegalEntity LegalEntity { get; set; }
        

        public virtual ICollection<RealitycsUserPassword> Passwords { get; set; }
    }
}
