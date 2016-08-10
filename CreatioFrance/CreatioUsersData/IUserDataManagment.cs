using System.Threading.Tasks;

namespace CreatioUsersData
{
    public interface IUserDataManagment
    {
        Task SaveUserInformation(Information informations);
        Task SaveAvocatsInformation(Avocat avocats);
        Task SaveCommercialInformation(Commercial commercials);
        Task<string> GetRoleIdByRoleName(string roleName);
        Task AddUserToRole(string userId, string roleId);
    }
}