using CreatioFranceEntities;
using CreatioMetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatioUsersBusiness
{
    public class QuestionManagment : IQuestionManagment
    {
        #region Members

        /// <summary>
        /// The _instance
        /// </summary>
        private static IQuestionManagment _instance;

        /// <summary>
        /// The _data managment
        /// </summary>
        private IMetaDataManagment _dataManagment = MetaDataManagment.GetInstance;

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
        public static IQuestionManagment GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new QuestionManagment();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Saves the question.
        /// </summary>
        /// <param name="question">The informations.</param>
        /// <returns></returns>
        public async Task SaveQuestion(Question question)
        {
            await _dataManagment.SaveQuestionAsync(question);
        }


        #endregion
    }
}
