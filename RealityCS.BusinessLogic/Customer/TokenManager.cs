using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealityCS.Core.Infrastructure;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Customer
{
    public class TokenManager : ITokenManager
    {
        // private readonly IConfiguration configuration;


        private readonly IOptions<JwtTokenOptions> jwtTokenOptions;
        private readonly IGenericRepository<User> userRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="jwtTokenOptions"></param>
        public TokenManager(IGenericRepository<User> userRepository, IOptions<JwtTokenOptions> jwtTokenOptions)
        {
            this.userRepository = userRepository;
            this.jwtTokenOptions = jwtTokenOptions;
        }


        #region Authentication

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> GenarateToken(string emailId, int legalEntityId)
        {

            var clientUser = RealitycsEngineContext.Current.Resolve<IClientUser>();

            string tokenString = string.Empty;


            User user = await clientUser.GetUser(emailId, legalEntityId);
            tokenString = GetToken(user);


            return tokenString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userClaims"></param>
        /// <param name="assignedRoles"></param>
        /// <returns></returns>
        private string GetToken(User user)
        {

            var utcNow = DateTime.UtcNow;


            List<Claim> claims = new List<Claim>()
            {
                        new Claim("userid", user.PK_Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.EmailId),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString()),

            };


            //var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Tokens:Key"]));
            //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtTokenOptions.Value.SigningKey /*this.configuration["Tokens:Key"]*/));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(this.jwtTokenOptions.Value.ExpireDays));

            var token = new JwtSecurityToken(
                this.jwtTokenOptions.Value.Issuer,
                this.jwtTokenOptions.Value.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: creds
            );




            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public User GetUserByToken(string token)
        {
            try
            {
                var userRepository = RealitycsEngineContext.Current.Resolve<IGenericRepository<User>>();
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                var UserId = tokenS.Claims.First(claim => claim.Type == "userid").Value;
                var emailId = tokenS.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.UniqueName).Value;
                User user = userRepository.Find(x => x.PK_Id == Convert.ToInt32(UserId) && x.EmailId == emailId);
                return user;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}
