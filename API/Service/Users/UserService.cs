using API.Data.EntityBase.Entities;
using API.Infastructures;
using API.ViewModels.IdentityResult;
using API.ViewModels.UserRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Service.Users
{
    public class UserService: IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration,
            IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
            this.roleManager = roleManager;
        }

        public async Task<IdentityCustomResult> CreateAsync(CreateUserVm request)
        {
            try
            {
                var user = new AppUser()
                {
                    UserName = request.Username,
                    Email = request.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                var result = await this.userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Basic");

                    var cart = new Cart()
                    {
                        UserId = user.Id
                    };

                    await this.unitOfWork.CartRepository.Add(cart);
                    await this.unitOfWork.SaveChanges();

                    return new SuccessResult();
                }

                return new ErrorResult(result.Errors);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IdentityCustomResult> GenerateToken(LoginVm request)
        {
            var result = await this.SignInAsync(request);

            if (result.IsSuccessed)
            {
                var user = await this.userManager.FindByEmailAsync(request.Email);
                var roles = await this.userManager.GetRolesAsync(user);

                var claims = new List<Claim>()
                {
                 new Claim(ClaimTypes.Email,user.Email),
                 new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                 new Claim(ClaimTypes.Name,user.UserName),
                 };

                foreach (var item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //rsA

                var token = new JwtSecurityToken(
                   issuer: this.configuration["Jwt:Issuer"],
                   audience: this.configuration["Jwt:Audience"],
                   claims: claims,
                    expires: DateTime.Now.AddHours(5),
                    signingCredentials: creds);

                return new SuccessResult(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return new ErrorResult("Login failed");
        }

        public async Task<IList<UserRolesViewModel>> GetUsers()
        {
            var users = await userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (AppUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.UserName = user.UserName;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return userRolesViewModel;
        }

        private async Task<List<string>> GetUserRoles(AppUser user)
        {
            return new List<string>(await userManager.GetRolesAsync(user));
        }

        public async Task<IdentityCustomResult> RoleAssignAsync(string email, string roleName)
        {
            try
            {
                var user = await this.userManager.FindByEmailAsync(email);

                if (user == null)
                    return new ErrorResult("Not found email");

                var result = await this.userManager.AddToRoleAsync(user, roleName);

                if (result.Succeeded)
                    return new SuccessResult();

                return new ErrorResult("Role Assign failed");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IdentityCustomResult> SignInAsync(LoginVm request)
        {
            try
            {
                //hash
                var user = await this.userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    return new ErrorResult("User is not found");

                var result = await this.signInManager.PasswordSignInAsync(user, request.Password, true, true);

                if (result.Succeeded)
                    return new SuccessResult();

                return new ErrorResult("Login failed");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IList<ManageUserRolesViewModel>> GetRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
                return null;

            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return (model);
        }

        public async Task<bool> UpdateRoleUser(IList<ManageUserRolesViewModel> model, string userId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return false;
                }
                var roles = await userManager.GetRolesAsync(user);
                var result = await userManager.RemoveFromRolesAsync(user, roles);
                if (!result.Succeeded)
                {
                    return false;
                }
                result = await userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
                if (!result.Succeeded)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
