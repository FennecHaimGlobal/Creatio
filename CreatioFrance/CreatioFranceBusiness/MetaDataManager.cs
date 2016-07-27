using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreatioFranceEntities;

namespace CreatioFranceBusiness
{
    public class MetaDataManager: IMetaDataManager
    {
        #region Members

        /// <summary>
        /// The _instance
        /// </summary>
        private static IMetaDataManager _instance;

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
        public static IMetaDataManager GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new MetaDataManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public async Task<MetaData> GetMetaDataByControler(string url)
        {
            MetaData result = new MetaData();

            try
            {
                result = new MetaData()
                {
                    Id = 1,
                    Title="Ta mere",
                    Description ="La description de ta mere",
                    KeyWord = "Ta taille de sous tif de ta mere"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion
    }
}
