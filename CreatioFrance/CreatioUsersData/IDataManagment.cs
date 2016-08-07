using System.Threading.Tasks;

namespace CreatioUsersData
{
    public interface IDataManagment
    {
        Task SaveUserInformation(Information informations);
        Task SaveAvocatsInformation(Avocat avocats);
        Task SaveCommercialInformation(Commercial commercials);
    }
}