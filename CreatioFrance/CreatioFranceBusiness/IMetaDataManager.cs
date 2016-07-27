using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreatioFranceEntities;

namespace CreatioFranceBusiness
{
    public interface IMetaDataManager
    {
        Task <MetaData> GetMetaDataByControler(string url);
    }
}
