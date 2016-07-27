using CreatioFrance.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreatioFrance.Areas.Administrateur.Controllers
{
    [Authorize(Roles = "CreatioAdmin")]
    public class AdministrateurBaseController: BaseController
    {
    }
}