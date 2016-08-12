using CreatioFranceEntities;
using System;
using System.Threading.Tasks;

namespace CreatioMetaData
{
    public interface IMetaDataManagment
    {
        Task<Guid> SaveQuestionAsync(Question question);
    }
}