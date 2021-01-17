using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealityCS.Web.Utility;

namespace RealityCS.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApiClient apiClient;
        private readonly IConfiguration configuration;

        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.apiClient = new ApiClient(new Uri(this.configuration["ApiBase"]));
        }

       
        public IActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Index(string username, string password,string ReturnUrl)
        {
            try
            {                
                var access_token = await this.apiClient.GetAsync<ApiResponse>("Authentication/GenarateToken"+ "?username=" + username + "&password=" + password + "");


                if (string.IsNullOrEmpty(access_token.access_token) == false)
                {
                    HttpClient _httpClient = new HttpClient();
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token.access_token);

                    var result = await _httpClient.GetAsync($"{ this.configuration["ApiBase"]}Authentication/GetClaims");

                    string data = await result.Content.ReadAsStringAsync();

                    var customclaims = JsonConvert.DeserializeObject<List<CustomClaim>>(data);

                    var claims = new List<Claim>();

                    foreach (var customclaim in customclaims)
                    {
                        claims.Add(new Claim(customclaim.Type, customclaim.Value));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        //AllowRefresh = true,
                        //ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                        //IsPersistent = true,
                    };

                    Response.Cookies.Append("access_token", access_token.access_token);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    string[] strTeamleads = new string[]
                    {
                        "Contract Recruiting Team Lead",
                        "Direct Hire Team Lead",
                        "Direct Hire Recruiter + Team Lead",
                        "Strategic Sourcing Team Lead"
                    };
                    if (string.IsNullOrEmpty(ReturnUrl) == false)
                    {
                        return new RedirectResult(ReturnUrl);
                    }

                    /*AXIOM REDIRECT
                    if (strTeamleads.Any(s=>s.Contains(claims.Find(claim => claim.Type == "AssignRoleName").Value.ToString())))
                    {
                        return new RedirectResult("~/axiom/TeamleadDashboard");
                    }
                    else{
                        return new RedirectResult("~/axiom/home");
                    }
                    */
                    return new RedirectResult("~/configuration/Usermanagement");

                }
                else
                {
                    ViewBag.loginerror = "Invalid username or password";
                }
            }
            catch(Exception ex)
            {
                ViewBag.loginerror = ex.GetBaseException().Message;
            }
            return View();
        }

        
        public async Task<IActionResult> Logout()
        {
            
            Response.Cookies.Delete("access_token");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);          

            return Redirect("~/Login");
        }


    }


    public class ApiResponse
    {
        public string access_token { get; set; }

    }

    public class CustomClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}