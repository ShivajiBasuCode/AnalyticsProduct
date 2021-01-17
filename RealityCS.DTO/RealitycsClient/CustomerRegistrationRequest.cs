using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.RealitycsClient
{
    /// <summary>
    /// Customer registration request
    /// </summary>
    public class CustomerRegistrationRequest
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="email">Email</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="passwordFormat">Password format</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="isApproved">Is approved</param>
        public CustomerRegistrationRequest(User customer, string email, string username,
            string password,
            PasswordFormat passwordFormat,
            bool isApproved = true)
        {
            Customer = customer;
            Email = email;
            Username = username;
            Password = password;
            PasswordFormat = passwordFormat;
            IsApproved = isApproved;
        }

        /// <summary>
        /// Customer
        /// </summary>
        public User Customer { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Password format
        /// </summary>
        public PasswordFormat PasswordFormat { get; set; }

     

        /// <summary>
        /// Is approved
        /// </summary>
        public bool IsApproved { get; set; }
    }
}
