using CreatioFrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatioFrance.Areas.Users.Models
{
    public class UsersViewModel
    {
        public CreatioFrance.Models.UsersViewModel Users { get; set; }

        public RegisterViewModel Register { get; set; }
    }
}