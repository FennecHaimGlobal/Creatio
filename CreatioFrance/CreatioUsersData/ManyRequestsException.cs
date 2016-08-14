using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatioUsersData
{
    public class ManyRequestsException : Exception
    {
        internal const string ERROR_MESSAGE = "Votre demande de rappel est en cours de traitement, il faut compter un délai d’environ 20 min pour être rappelé. Merci de votre compréhension.";

        public ManyRequestsException()
            : base(ERROR_MESSAGE)
        {

        }
    }
}
