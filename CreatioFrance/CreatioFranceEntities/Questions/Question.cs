using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatioFranceEntities
{
    public class Question
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public bool is_free { get; set; }
        public DateTime date_created { get; set; }
        public string question { get; set; }

      
    }
}
