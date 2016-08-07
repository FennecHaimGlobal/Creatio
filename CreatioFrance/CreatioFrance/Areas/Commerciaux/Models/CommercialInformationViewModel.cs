using CreatioFrance.Models;
using CreatioUsersData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatioFrance.Areas.Commerciaux.Models
{
    public class CommercialInformationViewModel
    {
        public RegisterViewModel Register { get; set; }
        public UsersViewModel Users { get; set; }

        public Commercial Commercials { get; set; }
    }
}