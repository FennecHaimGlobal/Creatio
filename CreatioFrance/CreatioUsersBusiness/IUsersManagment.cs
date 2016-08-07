using System.Threading.Tasks;
using CreatioUsersData;

namespace CreatioUsersBusiness
{
    public interface IUsersManagment
    {
        Task SaveUserInformation(Information informations);
        Task SaveAvocatsInformation(Avocat avocats);
        Task SaveCommercialInformation(Commercial commercials);
    }
}