using CreatioFrance.Models;
using CreatioUsersData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatioFrance.Areas.Avocats.Models
{
    public class AvocatInformationViewModel
    {
        public RegisterViewModel Register { get; set; }
        public UsersViewModel Users { get; set; }

        public Avocat Avocats { get; set; }
    }
}