using CreatioFranceEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatioMetaData
{
    public class MetaDataManagment : IMetaDataManagment
    {
        #region Members

        /// <summary>
        /// The _instance
        /// </summary>
        private static IMetaDataManagment _instance;

        /// <summary>
        /// The _locker
        /// </summary>
        private static object _locker = new object();

        private string _connectionString = string.Empty;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the get instance.
        /// </summary>
        /// <value>
        /// The get instance.
        /// </value>
        public static IMetaDataManagment GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new MetaDataManagment();
                        }
                    }
                }
                return _instance;
            }
        }

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    if (ConfigurationManager.ConnectionStrings["metadata-connection-provider"] != null)
                    {
                        _connectionString = ConfigurationManager.ConnectionStrings["metadata-connection-provider"].ToString();
                    }

                }

                return _connectionString;
            }
        }

        #endregion

        public async Task<Guid> SaveQuestionAsync(Question question)
        {
            Guid result = default(Guid);

            var newId = Guid.NewGuid();

            question_data quData = new question_data()
            {
                id = newId.ToString(),
                is_free = question.is_free,
                user_id = question.user_id,
                question = question.question
            };


            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities(ConnectionString))
            {
                context.question_data.Add(quData);

                await context.SaveChangesAsync();

                result = newId;
            }

                return result;
        }
    }
    public partial class CREATIO_DB_PRODEntities : DbContext
    {
        public CREATIO_DB_PRODEntities(string connectionString)
            : base(connectionString)
        {
        }
    }
}
