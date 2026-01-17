using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Mesurment.Request_Responce
{
    public class GeneralResponce
    {
        public int _status { get; set; }
        public string _message { get; set; }

        public GeneralResponce(int status, string message)
        {
            _status = status;
            _message = message;
        }
    }
}
