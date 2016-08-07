using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatioUsersData
{
    public class DataManagment: IDataManagment
    {
        #region Members

        /// <summary>
        /// The _instance
        /// </summary>
        private static IDataManagment _instance;

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
        public static IDataManagment GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new DataManagment();
                        }
                    }
                }
                return _instance;
            }
        }

        public async Task SaveAvocatsInformation(Avocat avocats)
        {
            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities())
            {
                context.Avocats.Add(avocats);

                await context.SaveChangesAsync();
            }
        }

        public async Task SaveCommercialInformation(Commercial commercials)
        {
            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities())
            {
                context.Commercials.Add(commercials);

                await context.SaveChangesAsync();
            }
        }




        #endregion

        public async Task SaveUserInformation(Information informations)
        {
            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities())
            {
                context.Informations.Add(informations);

                await context.SaveChangesAsync();
            }
        }
    }
}
