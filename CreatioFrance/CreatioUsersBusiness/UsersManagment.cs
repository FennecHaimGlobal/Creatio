using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreatioUsersData;

namespace CreatioUsersBusiness
{
    public class UsersManagment: IUsersManagment
    {
        #region Members

        /// <summary>
        /// The _instance
        /// </summary>
        private static IUsersManagment _instance;

        private IUserDataManagment _dataManagment = UserDataManagment.GetInstance;

        /// <summary>
        /// The _locker
        /// </summary>
        private static object _locker = new object();

        #endregion

        #region Properties
        /// <summary>
        /// Gets the get instance.
        /// </summary>
        /// <value>
        /// The get instance.
        /// </value>
        public static IUsersManagment GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new UsersManagment();
                        }
                    }
                }
                return _instance;
            }
        }

        public async Task SaveUserInformation(Information informations)
        {
            await _dataManagment.SaveUserInformation(informations);
        }

        public async Task SaveAvocatsInformation(Avocat avocats)
        {
            await _dataManagment.SaveAvocatsInformation(avocats);
        }

        public async  Task SaveCommercialInformation(Commercial commercials)
        {
            await _dataManagment.SaveCommercialInformation(commercials);
        }

        public async Task AddUserToRole(string userId, string RoleName)
        {
            string roleId = await _dataManagment.GetRoleIdByRoleName(RoleName);

            await _dataManagment.AddUserToRole(userId, roleId);
        }


        #endregion
    }
}
