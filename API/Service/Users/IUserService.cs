using API.ViewModels.IdentityResult;
using API.ViewModels.UserRoles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Service.Users
{
    public interface IUserService
    {
        Task<IdentityCustomResult> CreateAsync(CreateUserVm request);

        Task<IdentityCustomResult> SignInAsync(LoginVm request);

        Task<IdentityCustomResult> RoleAssignAsync(string email, string roleName);

        Task<IdentityCustomResult> GenerateToken(LoginVm request);

        Task<IList<UserRolesViewModel>> GetUsers();

        Task<IList<ManageUserRolesViewModel>> GetRoles(string userId);

        Task<bool> UpdateRoleUser(IList<ManageUserRolesViewModel> model, string userId);
    }
}