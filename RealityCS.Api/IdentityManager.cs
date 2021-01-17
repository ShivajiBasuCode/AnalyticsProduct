using RealityCS.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.Api
{
    public class IdentityManager : IIdentityManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<tbl_AUTH_AspNet_Roles> roleManager;
       // private readonly IConfiguration configuration;
       

        private readonly IOptions<JwtTokenOptions> jwtTokenOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="jwtTokenOptions"></param>
        public IdentityManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , RoleManager<tbl_AUTH_AspNet_Roles> roleManager,IOptions<JwtTokenOptions> jwtTokenOptions)
        {
            this.roleManager = roleManager;           
           
            this.userManager = userManager;
            this.signInManager = signInManager;


            this.jwtTokenOptions = jwtTokenOptions;


        }


        #region Authentication

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> GenarateToken(string username, string password)
        {
            
          
            string tokenString = string.Empty;
            var signInResult = await this.signInManager.PasswordSignInAsync(username, password, false, false);

           
            
            if (signInResult.Succeeded)
            {
              ApplicationUser user = await this.userManager.FindByNameAsync(username);

                var userClaims = user.UserRoles.SelectMany(s => s.Role.RoleClaims.Select(rc => new UserClaim { Type= rc.ClaimType,Value= rc.ClaimValue }))
                    .GroupBy(x => new { x.Type, x.Value}).Select(g => g.First())
                   // .Distinct()
                    .ToList();

                var userRole = user.UserRoles.Select(s => new tbl_AUTH_AspNet_Roles { Id = s.Role.Id, Name = s.Role.Name }).ToList();
                    
                tokenString = GetToken(user, userClaims,userRole);              

            }
            return tokenString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userClaims"></param>
        /// <param name="assignedRoles"></param>
        /// <returns></returns>
        private string GetToken(ApplicationUser user,List<UserClaim> userClaims,List<tbl_AUTH_AspNet_Roles> assignedRoles)
        {
            
            var utcNow = DateTime.UtcNow;


           List<Claim> claims =new List<Claim>()
            {
                        new Claim("userid", user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString()),

            };
            
            foreach(UserClaim userClaim in userClaims)
            {
                claims.Add(new Claim(userClaim.Type, userClaim.Value));
            }
                    
            

            foreach (var roleid in assignedRoles)
            {
               
                claims.Add(new Claim("AssignRole",roleid.Id.ToString()));
                claims.Add(new Claim("AssignRoleName", roleid.Name.ToString()));
            }


            //var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Tokens:Key"]));
            //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtTokenOptions.Value.SigningKey /*this.configuration["Tokens:Key"]*/));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(this.jwtTokenOptions.Value.ExpireDays));

            var token = new JwtSecurityToken(
                this.jwtTokenOptions.Value.Issuer,
                this.jwtTokenOptions.Value.Audience,
                claims,
                notBefore:DateTime.Now,
                expires: expires,
                signingCredentials: creds
            );

            


            return new JwtSecurityTokenHandler().WriteToken(token);

        }



        #endregion

        
    }
}
