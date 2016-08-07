using CreatioFrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatioFrance.Areas.Membres.Models
{
    public class MembersViewModel
    {
        public UsersViewModel Users { get; set; }

        public RegisterViewModel Register { get; set; }
    }
}